using UnityEngine;
using System.Collections;

public class patrolOn : MonoBehaviour {

	MoveBetween myMoveBetween;
	PolyNavAgent myPolyNav;

	
	
	void Start ()
	{
		myMoveBetween = GetComponent<MoveBetween>();
		myPolyNav = GetComponent<PolyNavAgent>();
	}


	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == ("inRange"))
		{
			//myMoveBetween = other.gameObject.GetComponent<MoveBetween>();
			//myPolyNav = other.gameObject.GetComponent<PolyNavAgent>();
			myMoveBetween.enabled = true;
			myPolyNav.enabled = true;

		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == ("inRange"))
		{
			//myMoveBetween = other.gameObject.GetComponent<MoveBetween>();
			//myPolyNav = other.gameObject.GetComponent<PolyNavAgent>();
			myMoveBetween.enabled = false;
			myPolyNav.enabled = false;
		}
	}
}

