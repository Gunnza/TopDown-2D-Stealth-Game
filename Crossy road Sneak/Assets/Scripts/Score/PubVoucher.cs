using UnityEngine;
using System.Collections;

public class PubVoucher: MonoBehaviour {

	Rigidbody2D voucher;
	public GameObject myPrefab;	
	public GameObject pickUpSFX;

	


	void Start() {
		voucher = GetComponent<Rigidbody2D>(); //.rotation = Quaternion.identity;
	}

	void FixedUpdate() {
		voucher.MoveRotation(voucher.rotation + 100 * Time.fixedDeltaTime);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
	
		if(other.gameObject.tag == ("Player"))
		{


			GameObject go = (GameObject)Instantiate(myPrefab);
			GameObject theSound = (GameObject)Instantiate(pickUpSFX);


			//Get Object
			GameObject objectiveScriptObject = GameObject.Find("Objective");
			//Get Script
			OfficeObjective officeObjective = objectiveScriptObject.GetComponent<OfficeObjective>();
			//Do Action
			officeObjective.pickUpCount += 1; //Plus 1 to the pickup count

			//gameObject.GetComponent<Renderer>().enabled = false;

			Destroy(gameObject);
			//enemy myscript = gameObject.GetComponentInChildren<enemy>(); enemy.health = 90;
		}


	}
}