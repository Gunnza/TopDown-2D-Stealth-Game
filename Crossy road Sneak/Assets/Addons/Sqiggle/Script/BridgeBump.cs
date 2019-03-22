using UnityEngine;
using System.Collections;

public class BridgeBump : MonoBehaviour {
	
	public GameObject bridge;
	
	private bool bumpon=false;
	
	/**
	 * We must add ourselves as a listener to the Parley instance in the scene.
	 * 
	 * One very important trick it to see that the Parley scripts executiong order is before ANY
	 * script taht wants to acces Parley from its Start method.
	 * 
	 * See Edit/Project Settings/Script Execution Order from inside Unity to mark this order.
	 */
	void Start () {
		
		// Get the sigleton instance of PArley and call Add us a Listener. Paramter one is our gameObject, 2 is the QuestEvents name and the last
		// is the message to send to this Object. (The message method can be in a different script on this GameObject)
		Parley.GetInstance().AddTriggerListener(gameObject,"LearntAboutKnocking","ActivateBridgeBump");
	}
	
	void Update () {
	}
	
	// This is the message method that will be called.
	public void ActivateBridgeBump(){
		bumpon=true;
	}
	
	public void OnTriggerEnter(Collider collider) {
		if (bumpon && "Player".Equals(collider.gameObject.tag)){
			bridge.GetComponent<Rigidbody>().useGravity=true;
			//RotateBridge();
			Parley.GetInstance().TriggerQuestEvent("BridgeKnocked");
			gameObject.SetActive(false);
		}
	}
}
