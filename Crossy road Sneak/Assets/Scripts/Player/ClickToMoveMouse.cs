using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Click to move the player
[RequireComponent(typeof(PolyNavAgent))]
public class ClickToMoveMouse: MonoBehaviour{

	public static ClickToMove instance;

 //boolean variables 
	public bool dragging = false; //finger to move camera 
	public bool choking = false; //is the player choking an enemy
	public bool caught = false; //is the player caught

//init audio variables
	AudioSource audio;
	public AudioClip footsteps;

//init player variable
	public GameObject playerObject;

//init  animator variable 
	Animator otherAnimator;

//Init variables of the marker
	float x;
	float y;
	public Vector2 newPosition;
	public Transform Marker;
	Renderer isMarkerOn;

//init varibale for the pathfinding agent
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
		audio = GetComponent<AudioSource>(); //getting the audio scource selected in the editor
		otherAnimator = playerObject.GetComponent<Animator> (); //getting the animator component of the player object
		isMarkerOn = Marker.GetComponent<Renderer>(); //acccessing the renderer component of the marker gameoject to change the marker location
	}

	void Update() 
	{

		if(caught)
		{
			agent.Stop(); //stop the pathfinding
			otherAnimator.CrossFade("PlayerIdle", 0f);//play the player idle animation
		}

		if(!caught)
		{
			if(choking)
			{
				otherAnimator.CrossFade("KnockOut", 0f); //when choking is true play the knockout animation
			}
		
			else
			{
				//Mobile Input
				//if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) 

				//Mouse Input
				if(Input.GetMouseButtonDown(0)){

						if(dragging == false) //if the player is not dragging the camera
						{
						 if (choking == false)
							{
								//setting the new position of the x marker
								isMarkerOn.enabled = true;
								x = Input.mousePosition.x;
								y = Input.mousePosition.y;
								Marker.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x,y,10.0f));
								
								//moving the player gameobject 
								agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition)); //using the pathfinding agent
								audio.PlayOneShot(footsteps, 0.7F); //walking sound
								otherAnimator.CrossFade("Walk", 0f);//play the walking animation 
							}
						else
							{
								otherAnimator.CrossFade("KnockOut", 0f); //choking animation 
							}

						}
				}
			}
		}
	}

	//On reaching the destination stop the player
	void OnDestinationReached(){
		otherAnimator.CrossFade("PlayerIdle", 0f); //player is idle so change animation to idle
		audio.Stop(); //stop footstep audio
		isMarkerOn.enabled = false; //turn off the marker 
	}

	//if the destination is invalid stop the player
	void OnDestinationInvalid(){
		otherAnimator.CrossFade("PlayerIdle", 0f); 
		audio.Stop();
		isMarkerOn.enabled = false;
	}

}
