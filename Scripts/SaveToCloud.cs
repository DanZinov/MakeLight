using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using TMPro;

public class SaveToCloud : MonoBehaviour {

	public GameObject googleConnected;
	public GameObject googleDisconnected;


	void Start(){
		if (Social.localUser.authenticated) {
			googleConnected.gameObject.SetActive (true);
			googleDisconnected.gameObject.SetActive (false);
		} else {
			googleConnected.gameObject.SetActive (false);
			googleDisconnected.gameObject.SetActive (true);
		}

	}
		

	public void UnlockAchievement(string id){
		Social.ReportProgress (id, 100, success => {
		});
	}

	public void ShowAchievementsUI(){
		Social.ShowAchievementsUI ();
	}

	public void AddScoreToLeaderboard(string leaderboardId, long score){
		Social.ReportScore (score, leaderboardId, success => {
		});
	}

	public void ShowLeaderboardUI ()
	{
		Social.ShowLeaderboardUI ();
	}



}
