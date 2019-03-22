using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGeneratorNoGuard : MonoBehaviour {

	public List<GameObject> prefabList = new List<GameObject>();

	//public GameObject levelPrefab;
	//public GameObject levelPrefab2;

	int prefabIndex;
	
	public GameObject PrefabManager;
	public GameObject navigation;

	PrefabsManager prefabsManager;
	PolyNavAgent polyNavAgent;
	MoveBetween moveBetween;
	





	void Start()
	{
		prefabIndex = UnityEngine.Random.Range(0,2);
		prefabsManager = PrefabManager.GetComponent<PrefabsManager>();
		//prefabsManager.newLevelPos = 0f;

		//polyNavAgent = Guard.GetComponent<PolyNavAgent>();
		//moveBetween = Guard.GetComponent<MoveBetween>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
	
		if(other.gameObject.tag == ("Player"))
		{
			prefabsManager.newLevelPos += 9.7f;
			//prefabsManager.specialFloor -=10;
			ES2.Save(prefabsManager.specialFloor,  "savefile.txt?tag=metresToSpecial");

				GameObject inst = (GameObject)Instantiate(prefabList[prefabIndex], new Vector3(prefabsManager.newLevelPos, 0, 0), Quaternion.Euler(0, 0, 0));
				inst.SetActive(true);


			//prefabList[prefabIndex].SetActive(true);
			navigation.SetActive(true);
			//Guard.SetActive(true);
			//Destroy(gameObject);
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == ("Player"))
		{
			navigation.SetActive(false);
		//	polyNavAgent.enabled = false;
			//moveBetween.enabled = false;
			Destroy(gameObject);
		}
		
	}
}


