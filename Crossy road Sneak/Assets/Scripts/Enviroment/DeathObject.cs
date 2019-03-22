using UnityEngine;
using System.Collections;

public class DeathObject : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject)
		{
			Destroy(other.gameObject);
		}
	}
}
