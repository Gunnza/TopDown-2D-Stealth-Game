using UnityEngine;
using System.Collections;

public class CollectedPubVoucher : MonoBehaviour {

	public GUIText collected;
	float theTime = 1;
	float speed = .1f;
	// Use this for initialization

	void Start()

	{
		GameObject objectiveScriptObject = GameObject.Find("Objective");
		//Get Script
		OfficeObjective officeObjective = objectiveScriptObject.GetComponent<OfficeObjective>();

		collected.text = ("Pub Vouchers ") + officeObjective.pickUpCount +  ("/10");


	}
	
	void Update()
	{
		transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);

		theTime -= Time.deltaTime;

		if (theTime <=0)

			Destroy(gameObject);
	}
}
	



