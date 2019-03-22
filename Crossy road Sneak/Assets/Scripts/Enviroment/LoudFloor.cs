using UnityEngine;
using System.Collections;

public class LoudFloor : MonoBehaviour {

	public GameObject loudFloorDetection;
	public GameObject puddleSplash;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		
		if(other.gameObject.tag == ("Player"))
		{
			StartCoroutine(Puddles());
			loudFloorDetection.SetActive(true);
		}
	}

	IEnumerator Puddles(){
		puddleSplash.GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds(.5f);
		puddleSplash.GetComponent<Renderer>().enabled = false;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == ("Player"))
		{
			loudFloorDetection.SetActive(false);
		}
}
}