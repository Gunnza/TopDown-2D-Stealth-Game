/*
using UnityEngine;
using UnityEngine.Advertisements;

public class showAds : MonoBehaviour {
	
	public static float restartCount = 0;

	
 public void ShowRewardedAd()
 {
	if (restartCount >= 2)
	{
   	 if (Advertisement.IsReady("rewardedVideoZone"))
    	 {
    	 	 var options = new ShowOptions { resultCallback = HandleShowResult };
     		 Advertisement.Show("rewardedVideoZone", options);
   		 }
	restartCount = 0;	
	}
}

  private void HandleShowResult(ShowResult result)
  {
    switch (result)
    {
      case ShowResult.Finished:
        Debug.Log("The ad was successfully shown.");
        //
        // YOUR CODE TO REWARD THE GAMER
        // Give coins etc.
        break;
      case ShowResult.Skipped:
        Debug.Log("The ad was skipped before reaching the end.");
        break;
      case ShowResult.Failed:
        Debug.LogError("The ad failed to be shown.");
        break;
		
    }
  }
}


*/