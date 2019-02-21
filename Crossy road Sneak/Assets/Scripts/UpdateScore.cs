using UnityEngine;
using System.Collections;

public class UpdateScore : MonoBehaviour {

	public float score = 0f;
	float highScore;
	float newpos;
	public float guardsKo;
	public float highGuardsKo;
	public static float coins;

	//Text Score
	public GameObject topMetersObj;
	public GameObject MetersObj;
	public GameObject Guards;
	public GameObject topGuards;
	public GameObject metresToNextDosser;
	public GameObject Coins;

	GUIText guardsTxt;
	GUIText topGuardsTxt;
	GUIText metresTxt;
	GUIText topMetersTxt;
	GUIText CoinsTxt;

	GUIText metresToNextDosserTxt;

	float workersKo;
	float highWorkersKo;

	float metresToSpecial;


	
	
	void Awake()
		
	{

		metresToSpecial = ES2.Load<float>("savefile.txt?tag=metresToSpecial");
		highScore = ES2.Load<float>("savefile.txt?tag=highScore");
		ES2.Save(score,  "savefile.txt?tag=Score");
		guardsTxt = Guards.GetComponent<GUIText>();
		topGuardsTxt = topGuards.GetComponent<GUIText>();
		metresTxt = MetersObj.GetComponent<GUIText>();
		topMetersTxt = topMetersObj.GetComponent<GUIText>();
		metresToNextDosserTxt = metresToNextDosser.GetComponent<GUIText>();
		CoinsTxt = Coins.GetComponent<GUIText>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == ("Player"))
		{
			score+= 10;
			ES2.Save(score,  "savefile.txt?tag=Score");
			if(score > highScore)
			{
				highScore = score;
				ES2.Save(highScore,  "savefile.txt?tag=highScore");
			}

			metresToSpecial = ES2.Load<float>("savefile.txt?tag=metresToSpecial");
			newpos += 14;
			transform.position = new Vector3(newpos, 0, 0);	
		}
	}

	void Update()
	{
		
		topMetersTxt.text = "Top " + highScore.ToString() + " M" ;
		metresTxt.text = score.ToString() + " Meters";
		guardsTxt.text = "KO's: " + guardsKo.ToString();
		topGuardsTxt.text = "Top: " + highGuardsKo.ToString();
		
		if(metresToSpecial <= 0)
			metresToSpecial = 0;
		metresToNextDosserTxt.text = metresToSpecial.ToString() + "M";
		
		CoinsTxt.text = coins.ToString();
	}

}
