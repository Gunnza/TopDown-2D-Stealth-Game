using UnityEngine;
using System.Collections;

public class Dosser : MonoBehaviour {
	
	public GameObject redCoin;
	public GameObject blue;

	public GUIStyle DosserGuiStyle;

	AudioSource audio;
	AudioSource blueAudio;
	public AudioClip unlock;
	public AudioClip notUnlock;


	float dosserRescued;
	bool showTitle = false;

	public GameObject PrefabManager;
	PrefabsManager prefabsManager;

	void Awake()
	{
		audio = GetComponent<AudioSource>();
		blueAudio = blue.GetComponent<AudioSource>();
		prefabsManager = PrefabManager.GetComponent<PrefabsManager>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == ("Player")) 
		{
			if (UpdateScore.coins < 50)
			{
				blueAudio.PlayOneShot(notUnlock, 0.7F);
			}
			else
			{
				UpdateScore.coins -= 50;
				audio.PlayOneShot(unlock, 0.7F);
				dosserRescued +=1;
				ES2.Save(dosserRescued,  "savefile.txt?tag=Dosser");
				prefabsManager.specialFloor = Random.Range(30,100);
				Destroy(redCoin);
				StartCoroutine(title());
			}
		}
	}

	IEnumerator title()
	{
		yield return new WaitForSeconds(1.5f);
		//audio.PlayOneShot(Fist, 0.7F);
		showTitle = true;
	}

	void OnGUI()
	{
		if(showTitle)
		{
			
			Time.timeScale = 0;
			
			if(GUI.Button (new Rect(40, 50, 400, 500), "", DosserGuiStyle))
			{
				Time.timeScale = 1;
				
				Destroy(gameObject);
			}
		}
	}
}
