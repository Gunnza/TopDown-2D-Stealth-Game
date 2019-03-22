using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {
	public GUIStyle titleGuiStyle;
	
	public GameObject settings;
	public GameObject Sound;
	
	bool onOrOff;

	
void OnGUI(){
	if(GUI.Button (new Rect(440, 0, 45, 45), "", titleGuiStyle))
	{
			Time.timeScale = 0;
			settings.SetActive(true);		
			//Destroy(gameObject)		
	}
		
}
	public void MuteSound(){
		
		if(onOrOff)
		{
			 AudioListener.pause = true;
		 	 onOrOff = false;
			 Sound.GetComponent<UnityEngine.UI.Image>().color = Color.red;
		}
		else
		{
			 AudioListener.pause = false;
			 onOrOff = true;
			 Sound.GetComponent<UnityEngine.UI.Image>().color = Color.white;
		}
		 GameObject theMainCamera = GameObject.Find ("Main Camera");
		
	}

	public void Restart(){
		
		settings.SetActive(false);	
		Time.timeScale = 1;
		Application.LoadLevel("Endless Runner");
	}
	public void exitSettings(){
		Time.timeScale = 1;
		settings.SetActive(false);	
	}
	
	
		
}
