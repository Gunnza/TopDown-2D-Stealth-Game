using UnityEngine;
using System.Collections;

public class GUIDead : MonoBehaviour {

	public GUIStyle scoreGuiStyle;
	public GUIStyle highScoreGuiStyle;
	public GUIStyle buttonGuiStyle;

	//float highScore;
	//float score;
	//float pickUpCount;

	void Start()
	{
		//highScore = ES2.Load<float>("savefile.txt?tag=highScore");
		//score = ES2.Load<float>("savefile.txt?tag=score");
		//pickUpCount = ES2.Load<float>("savefile.txt?tag=pickUpCount");
	}

	void OnGUI () {

			//GUI.Box(new Rect(70, 100, Screen.width/2+100, Screen.height/4), "");
		///	GUI.Label(new Rect(Screen.width/2-190,Screen.height/2-50,300,30), "YOUR SCORE :" + score.ToString("F0"), scoreGuiStyle);
		//	GUI.Label(new Rect(Screen.width/2-190,Screen.height/2,300,30), "Pub Vouchers Collected:" + pickUpCount.ToString("F0") + "/ 10", scoreGuiStyle);
			///GUI.Label(new Rect(Screen.width/2-190,Screen.height/2+50,300,30), "YOUR HIGHEST SCORE :" + highScore.ToString("F0"), highScoreGuiStyle);
			
			if (GUI.Button(new Rect(20, 350, 300, 150),"Retry" , buttonGuiStyle))
			{
				Debug.Log("Main Menu");
				Application.LoadLevel("Level2");
			}
			
		//	if (GUI.Button(new Rect(30, 660, 300, 60),"Next Level", buttonGuiStyle ))
		//	{
		//		Debug.Log("Next Level on the list");
		//		//	Application.LoadLevel("Menu");
		//	}
			
			if (GUI.Button(new Rect(25,600 , 300, 150),"Back to Menu", buttonGuiStyle))
			{
				Debug.Log("Retry");
				Application.LoadLevel("Menu");
			}
	}
}