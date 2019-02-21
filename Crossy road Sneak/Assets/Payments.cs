using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;

public class Payments : MonoBehaviour {
	


	// Use this for initialization
	public void pay25 () {
		Analytics.Transaction("50 Coins", 0.25m, "USD", null, null);	
	}
	
	public void pay69() {
		Analytics.Transaction("150 Coins", 0.69m, "USD", null, null);	
	}
	
	public void pay125 () {
		Analytics.Transaction("300 Coins", 1.25m, "USD", null, null);	
	}
	
	public void payShyGuy () {
		Analytics.Transaction("Shy Guy", 1.99m, "USD", null, null);	
	}
	public void payTeederToes() {
		Analytics.Transaction("Teeder Toes", 2.99m, "USD", null, null);	
	}
	
	
	
}
