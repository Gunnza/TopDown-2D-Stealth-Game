using UnityEngine;
using System.Collections;

public class PlayerCollideWithObject : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == ("UpWall"))
		{
			SwipeDetector.upAvailable = true;
		}
	}
}
