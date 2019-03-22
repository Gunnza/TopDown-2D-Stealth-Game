using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

//Init variables
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform normalPlayer;
	public Transform shyGuy;
	public Transform teederToes;
	public Transform target;

	public float speed = 0.5F;
	bool finishedDragging;

	public GameObject player;
	ClickToMove clicktomove;

	public GameObject DeathObject;
	public GameObject DeathObjectPlayer;

	bool startDragging;
	float onTouchDown;

	void Start ()
	{
		clicktomove = player.GetComponent<ClickToMove>();
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			onTouchDown = Time.time;
		}

		if(Input.touchCount > 0)
		{
			if ((Time.time - onTouchDown) >= .3f)
			{
				// 3 seconds has passed with the mouse down
				Debug.Log ("Drag Started");
				startDragging = true;
				// Do something
			}
		}

		if(startDragging)
		{

			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) 
			{
				DeathObject.SetActive(false);
				DeathObjectPlayer.SetActive(false);
				finishedDragging = false;
				clicktomove.dragging = true;	
				//Get movement of the finger since last frame
				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
				Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
				Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(.5f, .38f, point.z));
				Vector3 destination = transform.position + delta;

				//Move object across XY plane
				if(transform.position == Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime))
				{

					transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
				}
				else
					transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
					

			}
		}
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {

		

			clicktomove.dragging = false;
			finishedDragging = true;
			startDragging = false;//Timer to start the drag
			onTouchDown = 0;

		}
		else
		{

			if (target && finishedDragging == true)
					{
							Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
							Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(.15f, .38f, point.z)); //(new Vector3(0.5, 0.5, point.z));

							Vector3 destination = transform.position + delta;
							transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
						if (gameObject.transform.position == destination)
						{
								DeathObject.SetActive(true);
								DeathObjectPlayer.SetActive(true);
						}
					}

		}
	}
}

	//	if (Input.GetMouseButtonDown(0))
		//{
		//	dragOrigin = Input.mousePosition;
		//	return;
	//	}
		
	//	if (!Input.GetMouseButton(0)) return;
		
		//Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		//Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0);
		
		//transform.Translate(move, Space.World);



		


	
		

