using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VariablesToSave : MonoBehaviour {

	public SaveToCloud saveToCloud;
	public SceneLoader sceneLoader;
	public PieceConnector pieceConnector;
	public TextMeshProUGUI coinsText;
	public static int coins {set; get;}

	void Update(){
		coins = PlayerPrefs.GetInt("Coins");
		coinsText.text = coins.ToString ();
	}

	public void SkipLevel(){
		if (coins >= 10) {
			int scene;
			PlayerPrefs.SetInt ("Coins", coins - 10);
			scene = SceneManager.GetActiveScene ().buildIndex;
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Stars", 3);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed", 1);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Points", 10000);
			pieceConnector.leaderboardScore += 10000;
			PlayerPrefs.SetInt ("LeaderboardScore", pieceConnector.leaderboardScore);
			saveToCloud.AddScoreToLeaderboard (MakeLightGPG.leaderboard_leaderboard, (long)pieceConnector.leaderboardScore);
			sceneLoader.LoadNextScene ();
		}
	}
}
