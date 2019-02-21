using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class ClickToMoveMouse: MonoBehaviour{

	public static ClickToMove instance;

	public bool dragging = false;
	public bool choking = false;
	public bool caught = false;

	AudioSource audio;
	public AudioClip footsteps;

	public GameObject playerObject;
	Animator otherAnimator;


	float x;
	float y;
	public Vector2 newPosition;
	public Transform Marker;

	Renderer isMarkerOn;

	private PolyNavAgent _agent;
	public PolyNavAgent agent{
		get
		{
			if (!_agent)
				_agent = GetComponent<PolyNavAgent>();
			return _agent;			
		}
	}



	void Start() 
	{
		audio = GetComponent<AudioSource>();
		otherAnimator = playerObject.GetComponent<Animator> ();
		isMarkerOn = Marker.GetComponent<Renderer>();
	}

	void Update() 
	{

		if(caught)
		{
			agent.Stop();
			otherAnimator.CrossFade("PlayerIdle", 0f);
		}

		if(!caught)
		{
			if(choking)
			{
				otherAnimator.CrossFade("KnockOut", 0f);
			}
		
			else
			{
				//if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) 
				if(Input.GetMouseButtonDown(0)){

						if(dragging == false)
						{
						 if (choking == false)
							{
								isMarkerOn.enabled = true;
								x = Input.mousePosition.x;
								y = Input.mousePosition.y;


								Marker.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x,y,10.0f));
								//newPosition = Marker.transform.position;
		

								agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
							Debug.Log ("Camera move?");
								audio.PlayOneShot(footsteps, 0.7F);
								otherAnimator.CrossFade("Walk", 0f);
							}
						else
							{
								otherAnimator.CrossFade("KnockOut", 0f);
							}

						}
				}
			}
		}
	}




	//Message from Agent
	void OnDestinationReached(){
		otherAnimator.CrossFade("PlayerIdle", 0f);
		audio.Stop();
		isMarkerOn.enabled = false;
		//do something here...
	}

	//Message from Agent
	void OnDestinationInvalid(){
		otherAnimator.CrossFade("PlayerIdle", 0f);
		audio.Stop();
		isMarkerOn.enabled = false;
		//do something here...
	}



}
