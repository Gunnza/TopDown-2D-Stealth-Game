using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LG2Guards : MonoBehaviour {

	public List<GameObject> prefabList = new List<GameObject>();

	//public GameObject levelPrefab;
	//public GameObject levelPrefab2;

	int prefabIndex;
	
	public GameObject PrefabManager;
	public GameObject navigation;
	public GameObject Guard;
	public GameObject Guard2;
	public GameObject SpecialPrefab;
	PrefabsManager prefabsManager;
	PolyNavAgent polyNavAgent;
	MoveBetween moveBetween;
	PolyNavAgent polyNavAgent2;
	MoveBetween moveBetween2;
	
	//public float score = -10f;
	bool hitOnce = true;




	void Start()
	{

		prefabIndex = UnityEngine.Random.Range(0,10);
		prefabsManager = PrefabManager.GetComponent<PrefabsManager>();
		//prefabsManager.newLevelPos = 0f;

		polyNavAgent = Guard.GetComponent<PolyNavAgent>();
		moveBetween = Guard.GetComponent<MoveBetween>();
		polyNavAgent2 = Guard2.GetComponent<PolyNavAgent>();
		moveBetween2 = Guard2.GetComponent<MoveBetween>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
	
		if(other.gameObject.tag == ("Player"))
		{
			prefabsManager.newLevelPos += 9.7f;
			prefabsManager.specialFloor -=10;
			ES2.Save(prefabsManager.specialFloor,  "savefile.txt?tag=metresToSpecial");
			if (prefabsManager.specialFloor <= 0)
			{

				//Spawn the Special floor 
				GameObject SpecInst = (GameObject)Instantiate(SpecialPrefab, new Vector3(prefabsManager.newLevelPos, 0, 0), Quaternion.Euler(0, 0, 0));
				SpecInst.SetActive(true);
				prefabsManager.specialFloor =  Random.Range(70,150); //Put it back to normal 
			}
			else
			{
				GameObject inst = (GameObject)Instantiate(prefabList[prefabIndex], new Vector3(prefabsManager.newLevelPos, 0, 0), Quaternion.Euler(0, 0, 0));
				inst.SetActive(true);
				//Transform c  = p.GetComponentInChildren<Transform>().find("Vision-Cone");
			}


			navigation.SetActive(true);
			Guard.SetActive(true);
			Guard2.SetActive(true);
			//Destroy(gameObject);
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == ("Player"))
		{
			hitOnce = true;
			navigation.SetActive(false);
			polyNavAgent.enabled = false;
			moveBetween.enabled = false;
			polyNavAgent2.enabled = false;
			moveBetween2.enabled = false;
			Destroy(gameObject);
		}
		
	}
}


