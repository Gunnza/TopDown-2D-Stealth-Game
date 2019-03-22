using UnityEngine;
using System.Collections;

public class IntroScene : MonoBehaviour {

	public GameObject thePlayer1;
	public GameObject thePlayer2;
	public GameObject thePlayer3;
	public GameObject thePlayer4;
	public GameObject thePlayer5;

	GameObject instancePlayer1;
	GameObject instancePlayer2;
	GameObject instancePlayer3;
	GameObject instancePlayer4;
	GameObject instancePlayer5;

	public GUIStyle buttonGuiStyle;

	bool scene1 = true;
	bool scene2;
	bool scene3;
	bool scene4;
	bool scene5;
	bool scene6;






	void OnGUI()
	{
		if(scene1 == true)
		{
			if (GUI.Button(new Rect(250, 600, 200, 150),"Next" , buttonGuiStyle))
			{
				instancePlayer1 = (GameObject)Instantiate(thePlayer1);
				scene1 = false;
				scene2 = true;
			}
		}
		if(scene2 == true)
		{
			if (GUI.Button(new Rect(250, 600, 200, 150),"Next" , buttonGuiStyle))
			{
				instancePlayer2  = (GameObject)Instantiate(thePlayer2);
				Destroy(instancePlayer1 );
				scene2 = false;
				scene3 = true;
			}
		}
		if(scene3 == true)
		{
			if (GUI.Button(new Rect(250, 600, 200, 150),"Next" , buttonGuiStyle))
			{
				instancePlayer3  = (GameObject)Instantiate(thePlayer3);
				Destroy(instancePlayer2);
				scene3 = false;
				scene4 = true;
		    }
		}
		if(scene4 == true)
		{
			if (GUI.Button(new Rect(250, 600, 200, 150),"Next" , buttonGuiStyle))
			{
				instancePlayer4  = (GameObject)Instantiate(thePlayer4);
				Destroy(instancePlayer3);
				scene4 = false;
				scene5 = true;
			}
		}
		if(scene5 == true)
		{
			if (GUI.Button(new Rect(250, 600, 200, 150),"Next" , buttonGuiStyle))
			{
				instancePlayer5  = (GameObject)Instantiate(thePlayer5);
				Destroy(instancePlayer4);
				scene5 = false;
				scene6 = true;
			}
		}
		if(scene6 == true)
		{
			if (GUI.Button(new Rect(250, 600, 200, 150),"Next" , buttonGuiStyle))
			{
				Destroy(instancePlayer5);

			  Application.LoadLevel("Level2");
				
			}
		}
	}
}



	
		
		










			/*

			if (Input.GetMouseButtonDown(0))
			{
				Destroy(instancePlayer1 );
				GameObject instancePlayer2  = (GameObject)Instantiate(thePlayer2);
				if (Input.GetMouseButtonDown(0))
				{
					Destroy(instancePlayer2 );
					GameObject instancePlayer3  = (GameObject)Instantiate(thePlayer3);
					if (Input.GetMouseButtonDown(0))
					{
						Destroy(instancePlayer3 );
						GameObject instancePlayer4  = (GameObject)Instantiate(thePlayer4);
						if (Input.GetMouseButtonDown(0))
						{
							Destroy(instancePlayer4 );
							GameObject instancePlayer5  = (GameObject)Instantiate(thePlayer5);
							if (Input.GetMouseButtonDown(0))
							{
								Debug.Log ("StartGame");
							}
						}
					}
				}

			}

		}
	}

}

*/
	
