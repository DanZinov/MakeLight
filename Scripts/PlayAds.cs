using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class PlayAds : MonoBehaviour {
	public SceneLoader sceneLoader;
	public PieceConnector pieceConnector;
	public static PlayAds Instance {set;get;}
	public SaveToCloud saveToCloud;
	void Awake(){
		Advertisement.Initialize ("2747871");
	}

	public void ShowUnityUnrewardedAd(){
		Advertisement.Show ("video", new ShowOptions (){ resultCallback = HandleUnrewardedResult});
	}
	public void ShowUnityRewardedAd(){
		Advertisement.Show ("rewardedVideo", new ShowOptions(){resultCallback = HandleRewardedResult});
	}

	private void HandleRewardedResult (ShowResult result){
		switch (result) {
		case ShowResult.Finished:
			int scene;
			VariablesToSave.coins += 1;
			PlayerPrefs.SetInt ("Coins", VariablesToSave.coins);
			scene = SceneManager.GetActiveScene ().buildIndex;
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Stars", 3);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed", 1);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Points", 10000);
			pieceConnector.leaderboardScore += 10000;
			PlayerPrefs.SetInt ("LeaderboardScore", pieceConnector.leaderboardScore);
			saveToCloud.AddScoreToLeaderboard (MakeLightGPG.leaderboard_leaderboard, (long)pieceConnector.leaderboardScore);
			sceneLoader.LoadNextScene ();
			break;
		case ShowResult.Skipped:
			break;
		case ShowResult.Failed:
			break;
		}
	}
	private void HandleUnrewardedResult(ShowResult result){
		switch (result) {
		case ShowResult.Finished:
			break;
		case ShowResult.Skipped:
			break;
		case ShowResult.Failed:
			break;
		}
	}
	
}
