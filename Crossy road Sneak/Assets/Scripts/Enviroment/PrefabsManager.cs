using UnityEngine;
using System.Collections;

public class PrefabsManager : MonoBehaviour {

	public float newLevelPos = 0;
	public float specialFloor ;
	public float saveCoins = 25;

	void Awake ()
	{
		saveCoins = 25;
		specialFloor = Random.Range(30,100);
		//specialFloor = Random.Range(10,20);
	}


}
