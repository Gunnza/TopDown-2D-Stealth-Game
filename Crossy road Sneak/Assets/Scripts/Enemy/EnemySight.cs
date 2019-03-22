using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class EnemySight : MonoBehaviour {
	
	public Transform sightStart,player;
	public GameObject arrow;



	public GUIStyle buttonGuiStyle;
	public GUIStyle scoreGuiStyle;
	public GUIStyle boxGuiStyle;


	public GameObject PrefabManager;
	PrefabsManager prefabsManager;

	public bool spotted = false;
	public bool behindCover = false;
	public static bool detected = false;

	public float secsToNext=4.0f;
	bool GameOver;
	bool button1;
	bool button2;

	public AudioClip heartBeat;
	public AudioClip alert;
	AudioSource audio;

	float score;
	float highScore;
	float workersKo;
	float highWorkersKo;
	float Dosser;

	bool reset = false;
	bool isCoroutineStarted = false;

	GameObject mainCamera;
	AudioSource mainSound;

	ClickToMove clickToMove;
//	MoveBetween moveBetween;
//	GameObject theEnemy;
	GameObject thePlayer;

	// Use this for initialization
//	void Start () {
		//gameoverText.SetActive(false);
	//}
	void Awake()
	{
		//theEnemy = GameObject.Find("Enemy 1");
		thePlayer = GameObject.Find ("NormalPlayer");
		//moveBetween = theEnemy.GetComponent<MoveBetween>();
		clickToMove = thePlayer.GetComponent<ClickToMove>();

		mainCamera = GameObject.Find ("Main Camera");
		mainSound = mainCamera.GetComponent<AudioSource>();
		prefabsManager = PrefabManager.GetComponent<PrefabsManager>();

		audio = GetComponent<AudioSource>();

		GameOver = false;
		button1 = false;
		button2 = false;
		detected = false;
		Time.timeScale = 1;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == ("Player"))
		{
			Debug.Log ("Spotted");
			spotted = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
		{
			if(other.gameObject.tag ==("Player"))
			spotted = false;
			detected = false;
		}


	void FixedUpdate()
	{
	//	Raycasting();
	//	Behaviours();

		arrow.SetActive(false);

		if (spotted == true)
		{
			if( Physics2D.Linecast (sightStart.position,player.position, 1 << LayerMask.NameToLayer("Cover")))
			{
				//You are behind cover 
				behindCover = true;
				//Debug.DrawLine (sightStart.position, player.position, Color.red);
				detected = false;
			}
			else 
			{

				if(!audio.isPlaying)
					audio.PlayOneShot(alert, 0.7F);

				//You have been detected---------------------------------------------
				behindCover = false;
				//Debug.DrawLine (sightStart.position, player.position, Color.cyan);
				gameObject.GetComponent<SpriteRenderer>().color = Color.red;//RED CONE
				if(!isCoroutineStarted)
				{
					StartCoroutine("wait1sec");
				}
				//--------------------------------------------------------------------
			}
		}
	}

	IEnumerator wait1sec()
	{
		isCoroutineStarted = true;
		clickToMove.caught = true;
		yield return new WaitForSeconds(2f);
		detected = true;
	}
	
	void OnGUI () {


		if(detected == true)
		{
			//if(!audio.isPlaying)
				//audio.PlayOneShot(heartBeat, 0.7F);
			mainSound.enabled = false;
			Time.timeScale = 0;
			highScore = ES2.Load<float>("savefile.txt?tag=highScore");
			score = ES2.Load<float>("savefile.txt?tag=Score");
			workersKo = ES2.Load<float>("savefile.txt?tag=workersKo");
			highWorkersKo = ES2.Load<float>("savefile.txt?tag=highWorkersKo");
			Dosser = ES2.Load<float>("savefile.txt?tag=Dosser");

			GUI.Box(new Rect(70,Screen.height/2-340,320,350),  " ", boxGuiStyle);


				GUI.Label(new Rect(Screen.width/2-120,Screen.height/2-300,300,30), "Game Over ", scoreGuiStyle);

				GUI.Label(new Rect(Screen.width/2-120,Screen.height/2-250,300,30), "Metres Traveled: " + score, scoreGuiStyle);

				GUI.Label(new Rect(Screen.width/2-120,Screen.height/2-220,300,30), "Highest : " + highScore, scoreGuiStyle);

				GUI.Label(new Rect(Screen.width/2-120,Screen.height/2-150,300,30), "Employees Ko'd " + workersKo, scoreGuiStyle);
			
				GUI.Label(new Rect(Screen.width/2-120,Screen.height/2-120,300,30), "Highest : " + highWorkersKo, scoreGuiStyle);

				GUI.Label(new Rect(Screen.width/2-120,Screen.height/2-60,300,30), "Total Lazy's Escaped: " + Dosser, scoreGuiStyle);
				


				if (GUI.Button(new Rect(25, 420, 400, 100),"Bribe " + prefabsManager.saveCoins + " to continue", buttonGuiStyle))
				{
					if(UpdateScore.coins >= prefabsManager.saveCoins)
					{
						UpdateScore.coins -= prefabsManager.saveCoins;
						prefabsManager.saveCoins += 25;
						detected = false;
						Time.timeScale = 1;
						mainSound.enabled = true;
						clickToMove.caught = false;
						Destroy(gameObject);
					}
				}

			
				if (GUI.Button(new Rect(25, 530, 400, 100),"Restart", buttonGuiStyle))
				{
					prefabsManager.saveCoins = 25;
					detected = false;
					Time.timeScale = 1;
					prefabsManager.newLevelPos = 0f;
					mainSound.enabled = true;
					clickToMove.caught = false;
					//showAds.restartCount +=1;
				
				
				
				// Reference the Collections Generic namespace
 

  				//int totalPotions = 5;
 				// int totalCoins = 100;
			    // string weaponID = "Weapon_102";
  Analytics.CustomEvent("gameOver", new Dictionary<string, object>
  {
    //{ "potions", totalPotions },
   // { "coins", totalCoins },
   // { "activeWeapon", weaponID }
	 { "Meters Traveled", score }
		
  });
				
				
				
				
					Application.LoadLevel("Endless Runner");
				}

				if (GUI.Button(new Rect(360, 700, 70, 50),"Reset", buttonGuiStyle))
				{
				reset = true;
				}

				if(reset == true)
				{
					GUI.Label(new Rect(Screen.width/2-120,Screen.height/2+200,300,30), "Are you sure you want to reset all scores? : ", scoreGuiStyle);

					if (GUI.Button(new Rect(200, 700, 50, 50),"Yes", buttonGuiStyle))
					{
						//erase
						highScore =0;
						ES2.Save(highScore,  "savefile.txt?tag=highScore");
						score = 0;
						ES2.Save(score,  "savefile.txt?tag=Score");
						workersKo = 0; 
						ES2.Save(workersKo,  "savefile.txt?tag=workersKo");
						highWorkersKo = 0;
						ES2.Save(highWorkersKo,  "savefile.txt?tag=highWorkersKo");
						Dosser = 0; 
						ES2.Save(Dosser,  "savefile.txt?tag=Dosser");

						reset= false;
					}
					if (GUI.Button(new Rect(300, 700, 50, 50),"No", buttonGuiStyle))
					{
						reset= false;
					}
				}

				//if (GUI.Button(new Rect(20, 300, 400, 100),"Main Menu" , buttonGuiStyle))
				//{
					//detected = false;
					//Time.timeScale = 1;
				//	Application.LoadLevel("Menu");
			//	}

		}
		//Application.LoadLevel("Dead");
	}
}



	/* 
	void Raycasting()
	{

		if( Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Cover")))
		{
			behindCover = true;
		}


		//	Stopping the enemy from seeing through walls with if statments here
		if (Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player")) && behindCover == false)
		{
			spotted = true; 
		}

	}

	void Behaviours()
	{
		if(spotted == true)
		{
			arrow.SetActive(true);
		}
		else
		{
			arrow.SetActive(false);
		}
	}
*/









