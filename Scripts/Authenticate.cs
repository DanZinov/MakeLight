using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using TMPro;

public class Authenticate : MonoBehaviour {
	
	public TextMeshProUGUI loadingProgress;
	public int menuSceneIndex;
	public Slider loadingSlider;

	void Awake(){
		PlayGamesPlatform.Activate ();
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().EnableSavedGames ().Build ();
		PlayGamesPlatform.InitializeInstance (config);
		Social.localUser.Authenticate ((bool success) => {
			if(Social.localUser.authenticated == true && Application.internetReachability != NetworkReachability.NotReachable){
				StartCoroutine (LoadAsynchronously ());
			} else {
				loadingProgress.text = "No Internet Connection";
			}
		});
	}

	IEnumerator LoadAsynchronously(){
		if (PlayerPrefs.GetInt ("FinishedTutorial") == 1) {
			AsyncOperation operation = SceneManager.LoadSceneAsync (51);
			while (!operation.isDone) {
				float progress = Mathf.Clamp01 (operation.progress / 0.9f);
				loadingProgress.text = "Loading: " + Mathf.RoundToInt (progress * 100) + "%";
				loadingSlider.value = progress;
				yield return null;
			}
		} else {
			AsyncOperation operation = SceneManager.LoadSceneAsync (50);
			while (!operation.isDone) {
				float progress = Mathf.Clamp01 (operation.progress / 0.9f);
				loadingProgress.text = "Loading: " + Mathf.RoundToInt (progress * 100) + "%";
				loadingSlider.value = progress;
				yield return null;
			}
		}
	}

}
