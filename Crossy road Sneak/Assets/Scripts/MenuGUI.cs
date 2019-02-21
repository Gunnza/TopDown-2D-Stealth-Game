using UnityEngine;
using System.Collections;

public class MenuGUI : MonoBehaviour {

	public float xpos = -150;
	public float ypos = 300;
	public float width = 500;
	public float height = 150;

	public GUIStyle menuButtonGuiStyle;
	public GUIStyle levelOnenGuiStyle;
	public GUIStyle highScoreGuiStyle;
	public GUIStyle headingGuiStyle;

	float highScore;
	float highestPickUpCount;

	bool levels = false;

	// Use this for initialization
	void Start () {
		highScore = ES2.Load<float>("savefile.txt?tag=highScore");

		highestPickUpCount = ES2.Load<float>("savefile.txt?tag=highestPickUpCount");

	}

	void OnGUI()
	{

		if (levels == false)
		{

			GUI.Label(new Rect(0, 10, 500, 150), "Office Escape",  headingGuiStyle);
			if(GUI.Button (new Rect(xpos, ypos, width, height), "Play", menuButtonGuiStyle))
			{
				levels = true;
				//Application.LoadLevel("Scene1");
			}
		
			if (GUI.Button (new Rect(-150, 500, 500, 150), "Exit", menuButtonGuiStyle))
			{
				Application.Quit();
			}
		}

		else
		{

			GUI.Label(new Rect(Screen.width/2-160,Screen.height/2+280,300,30), "HIGH SCORE:" + highScore.ToString("F0"), highScoreGuiStyle);
			GUI.Label(new Rect(Screen.width/2-160,Screen.height/2+330,300,30), "Pub Vouchers:" + highestPickUpCount.ToString("F0") + "/ 10", highScoreGuiStyle);
			if(GUI.Button (new Rect(40, 50, 400, 500), "Level 1", levelOnenGuiStyle))
			{
				Application.LoadLevel("Intro");
			}

		}
		
	}
	
}

