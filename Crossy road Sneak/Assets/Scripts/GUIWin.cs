using UnityEngine;
using System.Collections;

public class GUIWin : MonoBehaviour {

	public GUIStyle scoreGuiStyle;
	public GUIStyle highScoreGuiStyle;
	public GUIStyle buttonGuiStyle;

	float highScore;
	float score;
	float pickUpCount;

	void Start()
	{
		highScore = ES2.Load<float>("savefile.txt?tag=highScore");
		score = ES2.Load<float>("savefile.txt?tag=score");
		pickUpCount = ES2.Load<float>("savefile.txt?tag=pickUpCount");
	}

	void OnGUI () {

			//GUI.Box(new Rect(70, 100, Screen.width/2+100, Screen.height/4), "");
			GUI.Label(new Rect(Screen.width/2-190,Screen.height/2-350,300,30), "YOUR SCORE :" + score.ToString("F0"), scoreGuiStyle);
			GUI.Label(new Rect(Screen.width/2-190,Screen.height/2-250,300,30), "Pub Vouchers Collected:" + pickUpCount.ToString("F0") + "/ 10", scoreGuiStyle);
			GUI.Label(new Rect(Screen.width/2-190,Screen.height/2-200,300,30), "YOUR HIGHEST SCORE :" + highScore.ToString("F0"), highScoreGuiStyle);
			
			if (GUI.Button(new Rect(20, 350, 300, 150),"Next Level" , buttonGuiStyle))
			{
				Debug.Log("Next Level");
				//Application.LoadLevel("Level2");
			}
			
			if (GUI.Button(new Rect(30, 500, 300, 150),"Back to Menu", buttonGuiStyle ))
			{
				Debug.Log("Next Level on the list");
				Application.LoadLevel("Menu");
			}
			
			if (GUI.Button(new Rect(25,650 , 300, 150),"Replay Level", buttonGuiStyle))
			{
				Debug.Log("Retry");
				Application.LoadLevel("Level2");
				
			}
	}
}