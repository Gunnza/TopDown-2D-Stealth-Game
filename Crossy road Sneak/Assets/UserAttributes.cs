using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;

public class UserAttributes : MonoBehaviour {
	
	void Start()
	{
		Gender gender = Gender.Female;
 	    Analytics.SetUserGender(gender);

  		int birthYear = 2014;
 		Analytics.SetUserBirthYear(birthYear);
	}
}
