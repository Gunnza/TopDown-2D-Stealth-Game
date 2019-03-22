using UnityEngine;
using System.Collections;

public class LoadScore : MonoBehaviour {

	float myInt;

	// Use this for initialization
	void Start () {
	myInt = ES2.Load<float>("savefile.txt?tag=score");
	}

	void OnGUI () {
		
		GUI.Label(new Rect(Screen.width/2-150,Screen.height/2-90,300,30), "YOUR SCORE :" + myInt.ToString());
	}
	

}
