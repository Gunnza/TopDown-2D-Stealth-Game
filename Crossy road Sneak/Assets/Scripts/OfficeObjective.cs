using UnityEngine;
using System.Collections;

public class OfficeObjective : MonoBehaviour {

	float score = 1000000;
	float highScore;
	public float pickUpCount;
	public float highestPickUpCount;

	public bool finished = false;

	public GUIStyle scoreGuiStyle;
	public GUIStyle highScoreGuiStyle;
	public GUIStyle buttonGuiStyle;


	public GameObject player;

	void Start()
	{
		pickUpCount = 0;
		highScore = ES2.Load<float>("savefile.txt?tag=highScore");
		highestPickUpCount = ES2.Load<float>("savefile.txt?tag=highestPickUpCount");
	}
	
	void FixedUpdate() 
	{
		if(finished == false)
			{
				score -= Time.deltaTime * 1000;
			}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
			if(other.gameObject.tag == ("Player"))
			{
				if(score > highScore)
				{
					highScore = score;
					ES2.Save(highScore,  "savefile.txt?tag=highScore");
				}

				ES2.Save(score,  "savefile.txt?tag=score");
				Debug.Log ("Win");
				finished = true;

				ES2.Save(pickUpCount,  "savefile.txt?tag=pickUpCount");

				if (pickUpCount > highestPickUpCount)
				{//Saving how much picked up this level
				highestPickUpCount = pickUpCount;
					ES2.Save(highestPickUpCount,  "savefile.txt?tag=highestPickUpCount");
				}
					

				//Make the objective dissappear
					gameObject.GetComponent<Renderer>().enabled = false;

				//Deactive player controls
					ClickToMove clicktomove = player.GetComponent<ClickToMove>();
					clicktomove.enabled = false;
					Application.LoadLevel("Win");
				
			}

	}
}
	
	/*
	void OnGUI () {

		ClickToMove clicktomove = player.GetComponent<ClickToMove>();
		if(finished == true)
		{
			//GUI.Box(new Rect(70, 100, Screen.width/2+100, Screen.height/4), "");
			GUI.Label(new Rect(Screen.width/2-120,Screen.height/2-300,300,30), "YOUR SCORE :" + score.ToString("F0"), scoreGuiStyle);
			GUI.Label(new Rect(Screen.width/2-120,Screen.height/2-240,300,30), "Pub Vouchers Collected:" + pickUpCount.ToString("F0") + "/ 10", scoreGuiStyle);
			GUI.Label(new Rect(Screen.width/2-120,Screen.height/2-130,300,30), "YOUR HIGHEST SCORE :" + highScore.ToString("F0"), highScoreGuiStyle);

			if (GUI.Button(new Rect(20, 400, 400, 100),"Main Menu" , buttonGuiStyle))
			{
				Debug.Log("Main Menu");
				clicktomove.enabled = true;
				Application.LoadLevel("Menu");
			}

			if (GUI.Button(new Rect(30, 500, 400, 100),"Next Level", buttonGuiStyle ))
			{
				Debug.Log("Next Level on the list");
				clicktomove.enabled = true;
				//	Application.LoadLevel("Menu");
			}

			if (GUI.Button(new Rect(25, 600, 400, 100),"Re - Play", buttonGuiStyle))
			{
				Debug.Log("Retry");
				clicktomove.enabled = true;
				Application.LoadLevel("Level2");
			}
		}
		
		
	}

}
*/
