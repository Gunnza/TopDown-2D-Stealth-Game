using UnityEngine;
using System.Collections;

public class CollectItems : MonoBehaviour {
	public string message="GetGold";
	public int qty=5;
	
	public void OnTriggerEnter(Collider collider) {
		if ("Player".Equals(collider.gameObject.tag)){
			collider.gameObject.SendMessage(message,qty);
			gameObject.SetActive(false);
		}
	}
}
