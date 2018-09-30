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

public class TutorialScript : MonoBehaviour {

	public Slider buttonClick;
	public Slider backgroundMusic;
	public AudioSource buttonClickSource;
	public AudioSource backgroundMusicSource;
	public TouchCamera touchCamera;
	public GameObject start;
	public GameObject itemPanel;
	public GameObject garbage;
	public GameObject time;
	public GameObject current;
	public GameObject info;
	public GameObject complete;
	void Awake(){
		buttonClick.value = 1f;
		backgroundMusic.value = 1f;
		buttonClickSource.volume = 1f;
		backgroundMusicSource.volume = 1f;
		PlayerPrefs.SetFloat ("BackgroundMusic", backgroundMusic.value);
		PlayerPrefs.SetFloat ("OtherSounds", buttonClick.value);
	}

	void Start () {
		touchCamera.isBuilding = true;
		start.gameObject.SetActive (true);
	}

	public void ShowItemPanel(){
		start.gameObject.SetActive (false);
		itemPanel.gameObject.SetActive (true);
	}

	public void ShowGarbagePanel(){
		itemPanel.gameObject.SetActive (false);
		garbage.gameObject.SetActive (true);
	}

	public void ShowTimePanel(){
		garbage.gameObject.SetActive (false);
		time.gameObject.SetActive (true);
	}

	public void ShowCurrentPanel(){
		time.gameObject.SetActive (false);
		current.gameObject.SetActive (true);
	}

	public void ShowInfoPanel(){
		current.gameObject.SetActive (false);
		info.gameObject.SetActive (true);
	}

	public void ShowCompletePanel(){
		info.gameObject.SetActive (false);
		complete.gameObject.SetActive (true);
	}

	public void FinishTutorial(){
		complete.gameObject.SetActive (false);
		PlayerPrefs.SetInt ("FinishedTutorial", 1);
		Social.ReportProgress (MakeLightGPG.achievement_finish_tutorial, 100, success => {
			VariablesToSave.coins += 1;
			PlayerPrefs.SetInt ("Coins", VariablesToSave.coins);
		});
	}

}
