using UnityEngine;
using System.Collections;

public class LoudFloorHearing : MonoBehaviour {
	
	
	void OnTriggerEnter2D(Collider2D other)
	{
		
		if(other.gameObject.tag == ("EnemySight"))
		{
			MoveBetween movebetween = other.gameObject.GetComponent<MoveBetween>();
			movebetween.loudFloor = true;
			movebetween.HeardLoudFloor();
		}
	}
}

	