using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LoadOperations: MonoBehaviour {
	
	public Image myImage;
	// Use this for initialization
	public bool UseAsync;
	private AsyncOperation async = null;
	public int LevelToLoad;
	
	public float FadeoutTime;
	public float fadeSpeed = 1.5f; 
	private bool fadeout;
	private bool fadein;    
	
	
	public void FadeOut(){
		fadein= false;
		fadeout = true;
		Debug.Log("Fading Out");
	}
	
	public void FadeIn(){
		fadeout = false;
		fadein = true;
		Debug.Log("Fading In");
	}
	
	void Update(){
		
		if(async != null){
			Debug.Log(async.progress);
			//When the Async is finished, the level is done loading, fade in the screen
			if(async.progress >= 1.0){
				async = null;
				FadeIn();
			}
		}
		
		//Fade Out the screen to black
		if(fadeout){
			myImage.color = Color.Lerp(myImage.color, Color.black, fadeSpeed * Time.deltaTime);
			
			//Once the Black image is visible enough, Start loading the next level
			if(myImage.color.a >= 0.999){
				StartCoroutine("LoadALevel");
				fadeout = false;
			}
		}
		
		if(fadein){
			myImage.color = Color.Lerp(myImage.color, new Color(0,0,0,0), fadeSpeed * Time.deltaTime);
			
			if(myImage.color.a <= 0.01){
				fadein = false;
			}
		}
	}
	
	public void LoadLevel(int index){
		if(UseAsync){
			LevelToLoad= index;
		}else{
			Application.LoadLevel(index);
		}
	}
	
	public IEnumerator LoadALevel() {
		async = Application.LoadLevelAsync(LevelToLoad);
		yield return async;
	}


}