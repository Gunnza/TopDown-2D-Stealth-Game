using UnityEngine;
using System.Collections;

public class ShowTitle : MonoBehaviour {
	public GUIStyle titleGuiStyle;
	public GUIStyle buyMoreGuiStyle;
	bool showTitle = false;
	bool showTitleButton = true;
	AudioSource audio;
	public AudioClip Fist;
	
	public GameObject buyMoreCoins;
	public GameObject selectCharacters;

	// Use this for initialization
	void Start () {

		audio = GetComponent<AudioSource>();
		StartCoroutine(title());
	}
	
	IEnumerator title()
	{
		yield return new WaitForSeconds(1.5f);
		audio.PlayOneShot(Fist, 0.7F);
		showTitle = true;
	}

	void OnGUI()
	{
		if(showTitle)
		{

			Time.timeScale = 0;
			
			if(GUI.Button (new Rect(100, 90, 200, 100), "", buyMoreGuiStyle))
			{
				Time.timeScale = 0;
				buyMoreCoins.SetActive(true);		
					
				showTitle = false;
			}
					
			/*

			if(GUI.Button (new Rect(50, 100, 45, 45), "", titleGuiStyle))
			{
				Time.timeScale = 0;
				selectCharacters.SetActive(true);		
						
				showTitleButton = false;
			}
			*/	
				
			if(showTitleButton)
			{
				if(GUI.Button (new Rect(20, 200, 450, 450), "", titleGuiStyle))
				{
					Time.timeScale = 1;

					Destroy(gameObject);
				}
			}
		}
	}
	
	public void exitCoins(){
		
		Time.timeScale = 1;
		buyMoreCoins.SetActive(false);
		showTitleButton = true;
		showTitle = true;
	}
	
	public void exitCharacters(){
		
		Time.timeScale = 1;
		selectCharacters.SetActive(false);	
		showTitleButton = true;
		showTitle = true;
	}
}
