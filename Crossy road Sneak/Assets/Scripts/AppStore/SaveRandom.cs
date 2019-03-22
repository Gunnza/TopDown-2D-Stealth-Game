using UnityEngine;
using System.Collections;

public class SaveRandom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		/* Save the value 123 to a key named myInt */
		ES2.Save("123",  "savefile.txt");

	}
	
	// Update is called once per frame
	void Update () {

}

}
