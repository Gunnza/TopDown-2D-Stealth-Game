 using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//example. moving between some points at random
[RequireComponent(typeof(PolyNavAgent))]
public class MoveBetween : MonoBehaviour{

	public static MoveBetween instance;

	public GameObject yellow;

	public GameObject redEnemy;
	Animator otherAnimator;


	public bool loudFloor = false;

	public Vector2 loudFloorPos;
	public Transform tranloudFloor;


	public Vector2 Patrol1;
	public Transform TPatrol1;

	public Vector2 Patrol2;
	public Transform TPatrol2;
	//public Transform Patrol3;



	string Walk = "Walk";
	string Idle = "PlayerIdle";

	public List<Vector2> WPoints = new List<Vector2>();



	private PolyNavAgent _agent;
	public PolyNavAgent agent{
		get
		{
			if (!_agent)
				_agent = GetComponent<PolyNavAgent>();
			return _agent;			
		}
	}

	void Awake()
	{
		instance = this; 
	}

	void Start(){

		Patrol1 = TPatrol1.transform.position;
		Patrol2 = TPatrol2.transform.position;


		WPoints.Add(Patrol1);
		WPoints.Add(Patrol2);
		//WPoints.Add(Patrol3);


		yellow.GetComponent<Renderer>().enabled = false;

		loudFloor = false;

		loudFloorPos  = tranloudFloor.transform.position;

		otherAnimator = redEnemy.GetComponent<Animator> ();

		if(loudFloor == false)
		{
		if (WPoints.Count != 0)
			agent.SetDestination(WPoints[ Random.Range(0, WPoints.Count) ]);
			otherAnimator.CrossFade(Walk, 0f);
		}
	}

	//Message from agent
	void OnDestinationReached(){

		otherAnimator.CrossFade(Idle, 0f);
		yellow.GetComponent<Renderer>().enabled = false;
		StartCoroutine(GuardPatrol());


	}

	void EnemyPatrol()
	{
		agent.SetDestination(WPoints[Random.Range(0, WPoints.Count)]);
		otherAnimator.CrossFade(Walk, 0f);
	}


	//Message from agent
	void OnDestinationInvalid(){

		EnemyPatrol();
	}

	IEnumerator GuardPatrol(){

		yield return new WaitForSeconds(2);
		EnemyPatrol();
	}

	//Message from agent
	public void HeardLoudFloor(){
		
		if(loudFloor == true)
		{
			agent.SetDestination(loudFloorPos);
			yellow.GetComponent<Renderer>().enabled = true;
			loudFloor = false;
		}

	}
	void OnDrawGizmosSelected(){

		for ( int i = 0; i < WPoints.Count; i++)
			Gizmos.DrawSphere(WPoints[i], 0.05f);			
	}
}
