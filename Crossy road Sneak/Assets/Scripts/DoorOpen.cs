using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

	public AudioClip openDoor;
	AudioSource audio;

	void Awake()
	{
		audio = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == ("Player") || other.gameObject.tag == ("Enemy"))
		{
			audio.PlayOneShot(openDoor, 0.7F);
			gameObject.GetComponent<Renderer>().enabled = false;

		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == ("Player") || other.gameObject.tag == ("Enemy"))
		{
			audio.PlayOneShot(openDoor, 0.7F);
			gameObject.GetComponent<Renderer>().enabled = true;
			
		}
	}



}
