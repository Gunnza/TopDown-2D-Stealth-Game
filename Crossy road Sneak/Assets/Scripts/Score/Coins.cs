using UnityEngine;
using System.Collections;

public class Coins: MonoBehaviour {

	GameObject updateScoreObject;
	public GameObject plus10;
	UpdateScore updateScore;
	int speed = 2;
	bool entered = false;

	AudioSource audio;
	public AudioClip coinSound;
	
	// Use this for initialization
	void Awake () {
		audio = GetComponent<AudioSource>();
		updateScoreObject = GameObject.Find ("UpdateScoreCollider");
		plus10.GetComponent<Renderer>().enabled = false;
		updateScore = updateScoreObject.GetComponent<UpdateScore>();
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == ("Player"))
		{
			if(entered == false)
			{
				entered = true;
				audio.PlayOneShot(coinSound, 0.7F);
				UpdateScore.coins += 5;
				plus10.GetComponent<Renderer>().enabled = true;
				GetComponent<Renderer>().enabled = false;
				StartCoroutine(wait1sec());
			}
		}
	}
	IEnumerator wait1sec()
	{
		yield return new WaitForSeconds(.5f);
		Destroy(gameObject);
	}
}
