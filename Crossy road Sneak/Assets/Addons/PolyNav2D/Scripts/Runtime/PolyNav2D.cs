using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//[RequireComponent(typeof(PolygonCollider2D))]
[AddComponentMenu("Navigation/Poly|Nav2D")]
///Singleton. Main class for map generation and navigation
public class PolyNav2D : MonoBehaviour {

	///If true map will recalculate whenever an obstacle changes position, rotation or scale.
	public bool generateOnUpdate = true;
	///The list of obstacles for the navigation
	public List<PolyNavObstacle> navObstacles = new List<PolyNavObstacle>();
	///The radious from the edges to offset the agents.
	public float inflateRadius = 0.1f;

	private bool generateFlag;
	private PolyMap map;
	private List<PathNode> nodes = new List<PathNode>();

	private Collider2D _masterCollider;
	private Collider2D masterCollider{
		get
		{
			if (_masterCollider == null)
				_masterCollider = GetComponent<Collider2D>();
			return _masterCollider;
		}
	}


	///The current instance of PolyNav2D
	private static PolyNav2D _current;
	public static PolyNav2D current{
		get
		{
			if (_current == null || !Application.isPlaying)
				_current = FindObjectOfType(typeof(PolyNav2D)) as PolyNav2D;
			
			return _current;
		}
	}

	public int nodesCount{
		get {return nodes.Count;}
	}


	void Reset(){

		gameObject.name = "@PolyNav2D";
		gameObject.AddComponent<PolygonCollider2D>();
	}

	void Awake(){

		_current = this;
		masterCollider.enabled = false;
		GenerateMap();
	}

	///Adds a PolyNavObstacle to the map.
	public void AddObstacle( PolyNavObstacle navObstacle ){

		if (!navObstacles.Contains(navObstacle)){
			navObstacles.Add(navObstacle);
			generateFlag = true;
		}
	}

	///Removes a PolyNavObstacle to the map.
	public void RemoveObstacle ( PolyNavObstacle navObstacle ){
		
		navObstacles.Remove(navObstacle);
		generateFlag = true;
	}

	void LateUpdate(){

		if (generateFlag == true){
			generateFlag = false;
			GenerateMap();
		}
	}

	///Find a path 'from' and 'to', providing a callback for when path is ready containing the path.
	public void FindPath(Vector2 start, Vector2 end, System.Action<List<Vector2>> callback){

		if (CheckLOS(start, end)){
			callback( new List<Vector2>{start, end});
			return;
		}

		//make a temp list with all existing nodes cloned
		var tempNodes= new List<PathNode>();
		foreach(var node in nodes)
			tempNodes.Add(node.Clone());

		var originalStart = start;
		if (!PointIsValid(start)){
			start = GetCloserEdgePoint(start);
		}

		//create start & end as nodes
		var startNode = new PathNode(start);
		var endNode = new PathNode(end);

		//add start and end nodes
		tempNodes.Add(startNode);
		tempNodes.Add(endNode);
		
		//link only those with the rest
		LinkNode(startNode, tempNodes);
		LinkNode(endNode, tempNodes);


		StartCoroutine( AStar.CalculatePath(startNode, endNode, tempNodes, delegate(List<Vector2> path){
			path.Insert(0, originalStart);
			callback(path);
		}));
	}

	///Generate the map
	public void GenerateMap (){

		CreatePolyMap();
		CreateNodes();
		LinkNodes(nodes);
	}

	//helper function
	Vector2[] TransformPoints ( Vector2[] points, Transform t ){
		for (int i = 0; i < points.Length; i++)
			points[i] = t.TransformPoint(points[i]);
		return points;
	}

	//takes all colliders points and convert them to usable stuff
	void CreatePolyMap (){

		//create a polygon object for each obstacle
		var obstaclePolys = new List<Polygon>();
		foreach(PolyNavObstacle obstacle in navObstacles){
			var transformedPoints = TransformPoints(obstacle.points, obstacle.transform).ToList();
			var inflatedPoints = InflatePolygon(transformedPoints, Mathf.Max(0.01f, inflateRadius + obstacle.extraOffset) );
			obstaclePolys.Add(new Polygon(inflatedPoints));
		}

		var masterPolys = new List<Polygon>();
		if (masterCollider is PolygonCollider2D){

			var polyCollider = masterCollider as PolygonCollider2D;
			//invert the main polygon points so that we save checking for inward/outward later (for Inflate)
			var reversed = new List<Vector2>();
			
			for (int i = 0; i < polyCollider.pathCount; ++i){

				for (int p = 0; p < polyCollider.GetPath(i).Count(); ++p)
					reversed.Add( polyCollider.GetPath(i)[p] );
				
				reversed.Reverse();

				var transformed = TransformPoints(reversed.ToArray(), polyCollider.transform).ToList();
				var inflated = InflatePolygon(transformed, Mathf.Max(0.01f, inflateRadius) );
			
				masterPolys.Add(new Polygon(inflated));
				reversed.Clear();
			}

		} else if (masterCollider is BoxCollider2D){
			var box = masterCollider as BoxCollider2D;
			var tl = box.offset + new Vector2(-box.size.x, box.size.y)/2;
			var tr = box.offset + new Vector2(box.size.x, box.size.y)/2;
			var br = box.offset + new Vector2(box.size.x, -box.size.y)/2;
			var bl = box.offset + new Vector2(-box.size.x, -box.size.y)/2;
			var transformed = TransformPoints(new Vector2[]{tl, bl, br, tr}, masterCollider.transform).ToList();
			var inflated = InflatePolygon(transformed, Mathf.Max(0.01f, inflateRadius));
			masterPolys.Add(new Polygon(inflated) );
		}

		//create the main polygon map (based on inverted) also containing the obstacle polygons
		map = new PolyMap(masterPolys, obstaclePolys);

		//
		//The colliders are never used again after this point. They are simply a drawing method.
		//
	}

	//Create Nodes at convex points (since master poly is inverted, it will be concave for it) if they are valid
	void CreateNodes (){

		nodes.Clear();

		foreach(Polygon poly in map.allPolygons){

			//Inflate even more for nodes, by a marginal value to allow CheckLOS between them
			List<Vector2> inflatedPoints = InflatePolygon(poly.points, 0.05f);
			for (int i = 0; i < inflatedPoints.Count; i++){

				//if point is concave dont create a node
				if (PointIsConcave(inflatedPoints, i))
					continue;

				//if point is not in valid area dont create a node
				if (!PointIsValid(inflatedPoints[i]))
					continue;

				nodes.Add(new PathNode(inflatedPoints[i]));
			}
		}
	}

	//link the nodes provided
	void LinkNodes(List<PathNode> nodeList){

		for (int a = 0; a < nodeList.Count; a++){

			nodeList[a].links.Clear();

			for (int b = 0; b < nodeList.Count; b++){
				
				if (nodeList[a] == nodeList[b])
					continue;

				if (b > a)
					continue;

				if (CheckLOS(nodeList[a].pos, nodeList[b].pos)){
					
					nodeList[a].links.Add(b);
					nodeList[b].links.Add(a);
				}
			}
		}
	}
	
	//link a single node with the rest. 'node' must already exist in 'toNodes'
	//used to link temporary start and end nodes. A bit repeating code, I know :/
	void LinkNode(PathNode node, List<PathNode> toNodes){

		for (int i = 0; i < toNodes.Count; i++){

			if (node == toNodes[i])
				continue;

			if (CheckLOS(node.pos, toNodes[i].pos)){
				
				node.links.Add(i);
				toNodes[i].links.Add(toNodes.IndexOf(node));
			}			
		}
	}


	///Determine if 2 points see each other.
	public bool CheckLOS ( Vector2 posA, Vector2 posB ){

		if ( (posA - posB).magnitude < Mathf.Epsilon )
			return true;
		
		foreach(Polygon poly in map.allPolygons){
			for (int i = 0; i < poly.points.Count; i++){
				if (SegmentsCross(posA, posB, poly.points[i], poly.points[(i + 1) % poly.points.Count]))
					return false;
			}
		}
		return true;
	}

	///determine if a point is within a valid (walkable) area.
	public bool PointIsValid ( Vector2 point ){

		foreach(Polygon poly in map.allPolygons){
			bool  isMaster = map.allPolygons.IndexOf(poly) < map.masterPolygons.Count;
			if (isMaster? !PointInsidePolygon(poly.points, point) : PointInsidePolygon(poly.points, point))
				return false;
		}
		return true;
	}

	///Kind of scales a polygon based on it's vertices average normal.
	public static List<Vector2> InflatePolygon(List<Vector2> points, float dist){

		var inflatedPoints= new List<Vector2>();

		for (int i = 0; i < points.Count; i++){

			Vector2 ab = (points[(i + 1) % points.Count] - points[i]).normalized;
			Vector2 ac = (points[i == 0? points.Count - 1 : i - 1] - points[i]).normalized;
			Vector2 mid = (ab + ac).normalized;
			
			mid *= (!PointIsConcave(points, i)? -dist : dist);
			inflatedPoints.Add(points[i] + mid);
		}

		return inflatedPoints;
	}

	///Check if or not a point is concave to the polygon points provided
	public static bool PointIsConcave(List<Vector2> points, int point){

		Vector2 current = points[point];
		Vector2 next = points[(point + 1) % points.Count];
		Vector2 previous =  points[point == 0? points.Count - 1 : point - 1];

		Vector2 left = new Vector2(current.x - previous.x, current.y - previous.y);
		Vector2 right = new Vector2(next.x - current.x, next.y - current.y);

		float cross = (left.x * right.y) - (left.y * right.x);

		return cross > 0;
	}

	///Check intersection of two segments, each defined by two vectors.
	public static bool SegmentsCross ( Vector2 a ,   Vector2 b ,   Vector2 c ,   Vector2 d  ){

		 float denominator = ((b.x - a.x) * (d.y - c.y)) - ((b.y - a.y) * (d.x - c.x));

		if (denominator == 0)
			return false;

	    float numerator1 = ((a.y - c.y) * (d.x - c.x)) - ((a.x - c.x) * (d.y - c.y));
	    float numerator2 = ((a.y - c.y) * (b.x - a.x)) - ((a.x - c.x) * (b.y - a.y));

	    if (numerator1 == 0 || numerator2 == 0)
	    	return false;

	    float r = numerator1 / denominator;
	    float s = numerator2 / denominator;

	    return (r > 0 && r < 1) && (s > 0 && s < 1);
	}

	///Is a point inside a polygon?
	public static bool PointInsidePolygon(List<Vector2> polyPoints, Vector2 point){

		float xMin = 0;
		foreach(Vector2 p in polyPoints)
			xMin = Mathf.Min(xMin, p.x);

		Vector2 origin = new Vector2(xMin - 0.1f, point.y);
		int intersections = 0;

		for (int i = 0; i < polyPoints.Count; i++){

			Vector2 pA = polyPoints[i];
			Vector2 pB = polyPoints[(i + 1) % polyPoints.Count];

			if (SegmentsCross(origin, point, pA, pB))
				intersections ++;
		}

		return (intersections & 1) == 1;
	}

	///Finds the closer edge point to the navigation valid area
	public Vector2 GetCloserEdgePoint ( Vector2 point ){

		var possiblePoints= new List<Vector2>();
		var closerVertex = Vector2.zero;
		var closerVertexDist = Mathf.Infinity;

		foreach(var poly in map.allPolygons){

			//marginal inflate
			List<Vector2> inflatedPoints = InflatePolygon(poly.points, 0.01f);

			for (int i = 0; i < inflatedPoints.Count; i++){

				Vector2 a = inflatedPoints[i];
				Vector2 b = inflatedPoints[(i + 1) % inflatedPoints.Count];

				Vector2 originalA = poly.points[i];
				Vector2 originalB = poly.points[(i + 1) % poly.points.Count];
				
				Vector2 proj = (Vector2)Vector3.Project( (point - a), (b - a) ) + a;

				if (SegmentsCross(point, proj, originalA, originalB) && PointIsValid(proj))
					possiblePoints.Add(proj);

				float dist = (point - inflatedPoints[i]).magnitude;
				if ( dist < closerVertexDist && PointIsValid(inflatedPoints[i])){
					closerVertexDist = dist;
					closerVertex = inflatedPoints[i];
				}
			}
		}

		possiblePoints.Add(closerVertex);
		//possiblePoints = possiblePoints.OrderBy(vector => (point - vector).magnitude).ToList();
		//return possiblePoints[0];

		var closerDist = Mathf.Infinity;
		var index = 0;
		for (int i = 0; i < possiblePoints.Count; i++){
			var dist = (point - possiblePoints[i]).magnitude;
			if (dist < closerDist){
				closerDist = dist;
				index = i;
			}
		}
		Debug.DrawLine(point, possiblePoints[index]);
		return possiblePoints[index];
	}


/*	//Legacy
	public static List<Vector2> InflatePath(List<Vector2> points, float dist){

		List<Vector2> inflatedPath= new List<Vector2>();

		for (int i = 0; i < points.Count; i++){
			Vector2 ab = (points[(i + 1) % points.Count] - points[i]).normalized;
			Vector2 ac = (points[i == 0? points.Count - 1 : i - 1] - points[i]).normalized;
			Vector2 mid = (ab + ac).normalized * -dist;
			inflatedPath.Add(points[i] + mid);
		}

		//retain the first and last points at original positions
		inflatedPath[0] = points[0];
		inflatedPath[points.Count-1] = points[points.Count-1];
		return inflatedPath;
	}
*/

	void OnDrawGizmos (){

		if (!Application.isPlaying)
			CreatePolyMap();

		//the original drawn polygons
		if (masterCollider is PolygonCollider2D){
			var polyCollider = masterCollider as PolygonCollider2D;
			for ( int i = 0; i < polyCollider.pathCount; ++i ) {
	            for ( int p = 0; p < polyCollider.GetPath( i ).Count(); ++p )
	                DebugDrawPolygon( TransformPoints( polyCollider.GetPath( i ), polyCollider.transform ).ToList(), Color.green );
	        }
        
        } else if (masterCollider is BoxCollider2D){
        	var box = masterCollider as BoxCollider2D;
			var tl = box.offset + new Vector2(-box.size.x, box.size.y)/2;
			var tr = box.offset + new Vector2(box.size.x, box.size.y)/2;
			var br = box.offset + new Vector2(box.size.x, -box.size.y)/2;
			var bl = box.offset + new Vector2(-box.size.x, -box.size.y)/2;
        	DebugDrawPolygon(TransformPoints(new Vector2[]{tl, tr, br, bl}, masterCollider.transform).ToList(), Color.green);
        }

		foreach(PolyNavObstacle o in navObstacles)
			DebugDrawPolygon(TransformPoints(o.points, o.transform).ToList(), new Color(1, 0.7f, 0.7f));
        //


		//the inflated actualy used polygons
        foreach (Polygon pathPoly in map.masterPolygons)
        	DebugDrawPolygon(pathPoly.points, new Color(1f,1f,1f,0.2f));

		foreach(Polygon poly in map.obstaclePolygons)
			DebugDrawPolygon(poly.points, new Color(1, 0.7f, 0.7f, 0.1f));
		//
	}

	//helper debug function
	void DebugDrawPolygon(List<Vector2> points, Color color){
		for (int i = 0; i < points.Count; i++)
			Debug.DrawLine(points[i], points[(i + 1) % points.Count], color);
	}

#if UNITY_EDITOR
	[UnityEditor.MenuItem("GameObject/Create Other/PolyNav2D")]
	public static void CreatePolyNav2D (){
		if (FindObjectOfType(typeof(PolyNav2D)) == null){
			var newNav = new GameObject("@PolyNav2D").AddComponent<PolyNav2D>();
			UnityEditor.Selection.activeObject = newNav;
		}
	}
#endif

}


//defines a polygon
class Polygon{

	public List<Vector2> points = new List<Vector2>();

	public Polygon(List<Vector2> points){
		this.points = points;
	}
}

//defines the main navigation polygon and its sub obstacle polygons
class PolyMap{

	public List<Polygon> masterPolygons = new List<Polygon>();
	public List<Polygon> obstaclePolygons = new List<Polygon>();

	public List<Polygon> allPolygons{
		get
		{
			var polyList = new List<Polygon>();
			polyList.AddRange(masterPolygons);
			polyList.AddRange(obstaclePolygons);
			return polyList;
		}
	}

	public PolyMap(List<Polygon> masterPolys, List<Polygon> obstaclePolys){
		masterPolygons = masterPolys;
		obstaclePolygons = obstaclePolys;
	}
}

//defines a node for A*
class PathNode : System.IComparable{

	public Vector2 pos;
	public List<int> links= new List<int>();
	public float cost = 1;
	public float estimatedCost;
	public PathNode parent = null;

	public PathNode ( Vector2 pos  ){
		this.pos = pos;
	}

	public int CompareTo ( System.Object obj ){
		var other = obj as PathNode;
		if (this.estimatedCost < other.estimatedCost){
			return -1;
		} else if (this.estimatedCost > other.estimatedCost){
			return 1;
		} else {
			return 0;
		}
	}

	public PathNode Clone (){

		var newNode = new PathNode(pos);
		newNode.cost = this.cost;
		newNode.estimatedCost = this.estimatedCost;
		
		if (this.parent != null)
			newNode.parent = this.parent.Clone();

		foreach(int linkIndex in this.links)
			newNode.links.Add(linkIndex);

		return newNode;
	}
}
