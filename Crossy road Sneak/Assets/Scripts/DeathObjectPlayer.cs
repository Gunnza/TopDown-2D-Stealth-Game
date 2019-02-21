using UnityEngine;
using System.Collections;

public class DeathObjectPlayer : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.tag == ("Player"))
		{
			EnemySight.detected = true;
		}
	}
}
