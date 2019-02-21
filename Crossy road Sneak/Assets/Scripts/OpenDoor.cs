using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenDoor : MonoBehaviour {

	public GameObject doorBlocker;
	public GameObject door;
	
	public void openDoor()
	{

		door.GetComponent<Renderer>().enabled = false;
		doorBlocker.SetActive(false);
	}
}
