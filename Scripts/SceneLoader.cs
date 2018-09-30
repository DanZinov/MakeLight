using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

	public Slider buttonClick;
	public Slider backgroundMusic;
	public GameObject reloadPanel;
	public TouchCamera touchCamera;
	public GameObject settingsPanel;
	public GameObject infoPanel;
	public GameObject panelBackgroundSettings;
	public GameObject panelBackgroundMenu;
	public GameObject panelBackgroundReplay;
	public GameObject panelBackgroundInfo;
	public GameObject menuPanel;
	public GameObject levelSelectPanel;
	public GameObject shopPanel;
	public GameObject statsPanel;
	public GameObject newItemPanel;

	void Start(){
		if (PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "NewItem") != 1) {
			newItemPanel.gameObject.SetActive (true);
		}
	}

	public void LoadMainMenu(){
		SceneManager.LoadSceneAsync (51);
	}
	public void ExitGame(){
		Application.Quit ();
	}
	public void LoadNextScene(){
		int scene = SceneManager.GetActiveScene ().buildIndex;
		if (PlayerPrefs.GetInt ((scene).ToString () + "Completed") == 1) {
			if (SceneManager.GetActiveScene ().buildIndex == 10) {
				Social.ReportProgress (MakeLightGPG.achievement_new_item, 100, success => {
				});
			} else if (SceneManager.GetActiveScene ().buildIndex == 20) {
				Social.ReportProgress (MakeLightGPG.achievement_5_items_unlocked, 100, success => {
				});
			} else if (SceneManager.GetActiveScene ().buildIndex == 30) {
				Social.ReportProgress (MakeLightGPG.achievement_10_items_unlocked, 100, success => {
				});
			}
			SceneManager.LoadScene (scene + 1);
		}
	}
	public void LoadPreviousScene(){
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.buildIndex - 1);
	}

	public void CloseNewItem(){
		if (newItemPanel.activeSelf) {
			newItemPanel.gameObject.SetActive (false);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "NewItem", 1);
		}
	}

	public void ShowHideLevelSelect(){
		if (levelSelectPanel.activeSelf == false) {
			levelSelectPanel.gameObject.SetActive (true);
		} else {
			levelSelectPanel.gameObject.SetActive (false);
		}
	}

	public void ShowHideShop(){
		if (shopPanel.activeSelf == false) {
			shopPanel.gameObject.SetActive (true);
		} else {
			shopPanel.gameObject.SetActive (false);
		}
	}

	public void ShowHideSettings(){
		if (settingsPanel.activeSelf == false) {
			panelBackgroundSettings.gameObject.SetActive (true);
			touchCamera.isBuilding = true;
			settingsPanel.gameObject.SetActive (true);
		} else {
			touchCamera.isBuilding = false;
			panelBackgroundSettings.gameObject.SetActive (false);
			settingsPanel.gameObject.SetActive (false);
			PlayerPrefs.SetFloat ("BackgroundMusic", backgroundMusic.value);
			PlayerPrefs.SetFloat ("OtherSounds", buttonClick.value);
		}
	}

	public void ShowHideMainMenuSettings(){
		if (settingsPanel.activeSelf == false) {
			settingsPanel.gameObject.SetActive (true);
		} else {
			settingsPanel.gameObject.SetActive (false);
			PlayerPrefs.SetFloat ("BackgroundMusic", backgroundMusic.value);
			PlayerPrefs.SetFloat ("OtherSounds", buttonClick.value);
		}
	}

	public void ReloadLevel(){
		if (reloadPanel.activeSelf == false) {
			touchCamera.isBuilding = true;
			panelBackgroundReplay.gameObject.SetActive (true);
			reloadPanel.gameObject.SetActive (true);
		} else {
			touchCamera.isBuilding = false;
			panelBackgroundReplay.gameObject.SetActive (false);
			reloadPanel.gameObject.SetActive (false);
		}
	}
	public void YesReload(){
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
	}

	public void ShowHideInfo(){
		if (infoPanel.activeSelf == false) {
			panelBackgroundInfo.gameObject.SetActive (true);
			touchCamera.isBuilding = true;
			infoPanel.gameObject.SetActive (true);
		} else {
			panelBackgroundInfo.gameObject.SetActive (false);
			touchCamera.isBuilding = false;
			infoPanel.gameObject.SetActive (false);
		}
	}

	public void ShowHideMainMenuInfo(){
		if (infoPanel.activeSelf == false) {
			infoPanel.gameObject.SetActive (true);
		} else {
			infoPanel.gameObject.SetActive (false);
		}
	}

	public void ShowHideMenu(){
		if (menuPanel.activeSelf == false) {
			panelBackgroundMenu.gameObject.SetActive (true);
			touchCamera.isBuilding = true;
			menuPanel.gameObject.SetActive (true);
		} else {
			touchCamera.isBuilding = false;
			panelBackgroundMenu.gameObject.SetActive (false);
			menuPanel.gameObject.SetActive (false);
		}
	}

	public void ShowHideStatsPanel(){
		if (statsPanel.activeSelf == false) {
			statsPanel.gameObject.SetActive (true);
		} else {
			statsPanel.gameObject.SetActive (false);
		}
	}
}
