using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;
using UnityEngine.SceneManagement;

public class PieceConnector : MonoBehaviour {

	public GameObject[] InventorySlots;

	public TextMeshProUGUI[] InventorySlotsText;
	public TextMeshProUGUI levelText;
	public SaveToCloud saveToCloud;
	public int leaderboardScore;
	public TouchCamera touchCamera;
	private int getItemsCountFinal;
	public TextMeshProUGUI completionTime;
	public TextMeshProUGUI partsCompleted;
	public int itemCountStart;
	public TextMeshProUGUI pointsText;
	public Slider pointsSlider;
	public GameObject leftStar;
	public GameObject middleStar;
	public GameObject rightStar;
	public GameObject completionPanel;
	public GameObject On;
	public GameObject Off;
	public bool isConnected;
	public bool circuitComplete = false;
	private GameObject slotsToUse;
	public List<GameObject> slots;
	public int numberOfSlots;
	private GameObject lightWire;
	private GameObject generatorWire;
	public TextMeshProUGUI time;
	private int currentTime;

	void Update(){
		time.text = (Mathf.Floor (currentTime / 60f)).ToString() + "m " + (currentTime - (Mathf.Floor (currentTime / 60f)) * 60).ToString() + "s";
	}
	void Start(){
		if (SceneManager.GetActiveScene ().buildIndex != 50) {
			levelText.text = "Level " + SceneManager.GetActiveScene ().buildIndex.ToString ();
		}
		leaderboardScore = PlayerPrefs.GetInt ("LeaderboardScore");
		for(int i = 0; i < InventorySlots.Length; i++){
			InventorySlotsText [i].text = (InventorySlots [i].transform.childCount - 1).ToString ();
		}

		for(int i = 0; i < InventorySlots.Length; i++){
			itemCountStart += (InventorySlots [i].transform.childCount - 1);
		}

		StartCoroutine (StartTimer());
		slotsToUse = GameObject.FindGameObjectWithTag ("UsableSlots");
		lightWire = GameObject.FindGameObjectWithTag ("LightWire");
		generatorWire = GameObject.FindGameObjectWithTag ("GeneratorWire");
		slots.Add (lightWire);
		for (int k = 0; k < numberOfSlots; k++) {
			slots.Add (slotsToUse.transform.GetChild (k).gameObject);
		}
		slots.Add (generatorWire);
	}

	public void CheckLight (){
		int i = ((slots.Count - 2) / 3) + 1;
		isConnected = true;
		while (isConnected) {
			Debug.Log (i);
			if (slots [i].transform.childCount > 0) {
				if (slots [i].transform.GetComponentInChildren<PieceDescriptionEnd> ().top && slots [i - ((slots.Count - 2) / 3)].transform.childCount > 0 && slots [i - ((slots.Count - 2) / 3)].transform.GetComponentInChildren<PieceDescriptionStart> ().bottom) {
					i -= ((slots.Count - 2) / 3);
					getItemsCountFinal += 1;
				} else if (slots [i].transform.GetComponentInChildren<PieceDescriptionEnd> ().bottom && slots [i + ((slots.Count - 2) / 3)].transform.childCount > 0 && slots [i + ((slots.Count - 2) / 3)].transform.GetComponentInChildren<PieceDescriptionStart> ().top) {
					i += ((slots.Count - 2) / 3);
					getItemsCountFinal += 1;
				} else if (slots [i].transform.GetComponentInChildren<PieceDescriptionEnd> ().right && slots [i + 1].transform.childCount > 0 && slots [i + 1].transform.GetComponentInChildren<PieceDescriptionStart> ().left) {
					i++;
					getItemsCountFinal += 1;
				} else if (slots [i].transform.GetComponentInChildren<PieceDescriptionEnd> ().left && slots [i - 1].transform.childCount > 0 && slots [i - 1].transform.GetComponentInChildren<PieceDescriptionStart> ().right) {
					i--;
					getItemsCountFinal += 1;
				} else if (slots [i].transform.GetComponentInChildren<PieceDescriptionEnd> ().right2 && slots [i + 1].transform.childCount > 0 && slots [i + 1].transform.GetComponentInChildren<PieceDescriptionStart> ().left2) {
					i++;
					getItemsCountFinal += 1;
				} else if (slots [i].transform.GetComponentInChildren<PieceDescriptionEnd> ().left2 && slots [i - 1].transform.childCount > 0 && slots [i - 1].transform.GetComponentInChildren<PieceDescriptionStart> ().right2) {
					i--;
					getItemsCountFinal += 1;
				} else if (slots [i].transform.GetComponentInChildren<PieceDescriptionEnd> ().top2 && slots [i - ((slots.Count - 2) / 3)].transform.childCount > 0 && slots [i - ((slots.Count - 2) / 3)].transform.GetComponentInChildren<PieceDescriptionStart> ().bottom2) {
					i -= ((slots.Count - 2) / 3);
					getItemsCountFinal += 1;
				} else if (slots [i].transform.GetComponentInChildren<PieceDescriptionEnd> ().bottom2 && slots [i + ((slots.Count - 2) / 3)].transform.childCount > 0 && slots [i + ((slots.Count - 2) / 3)].transform.GetComponentInChildren<PieceDescriptionStart> ().top2) {
					i += ((slots.Count - 2) / 3);
					getItemsCountFinal += 1;
				}
				else if (i == (((slots.Count - 2) / 3) * 2) && slots [i].transform.GetComponentInChildren<PieceDescriptionEnd> ().right) {
					getItemsCountFinal += 1;
					On.gameObject.SetActive (true);
					Off.gameObject.SetActive (false);
					CompletionScreen ();
					circuitComplete = true;
					isConnected = false;
				} else {
					isConnected = false;
					On.gameObject.SetActive (false);
					Off.gameObject.SetActive (true);
					circuitComplete = false;
					getItemsCountFinal = 0;
				}
			} else {
				isConnected = false;
				On.gameObject.SetActive (false);
				Off.gameObject.SetActive (true);
				circuitComplete = false;
				getItemsCountFinal = 0;
			}
		}

	}

	public void CountItems(){
		for(int i = 0; i < InventorySlots.Length; i++){
			InventorySlotsText [i].text = (InventorySlots [i].transform.childCount - 1).ToString ();
		}
	}
	private void ZeroStars(){
		int points;
		leftStar.gameObject.SetActive (false);
		middleStar.gameObject.SetActive (false);
		rightStar.gameObject.SetActive (false);
		points = Mathf.RoundToInt (1000 + ((10000 / currentTime) * 2));
		pointsText.text = points.ToString() + " Points";
		pointsSlider.value = ((float)points / 10000f);
		if (PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed") == 0) {
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Stars", 0);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed", 0);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Points", points);
			leaderboardScore += points;
			PlayerPrefs.SetInt ("LeaderboardScore", leaderboardScore);
			saveToCloud.AddScoreToLeaderboard (MakeLightGPG.leaderboard_leaderboard, (long)leaderboardScore);
		}
	}

	private void OneStar(){
		int points;
		StartCoroutine(OneStarSequence ());
		points = Mathf.RoundToInt (3000 + ((10000 / currentTime) * 2));
		pointsText.text = points.ToString() + " Points";
		pointsSlider.value = ((float)points / 10000f);
		if ((PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed") == 0) || ((PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed") == 1) && (PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Stars") <= 1) || (PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Points") <= points))) {
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Stars", 1);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed", 1);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Points", points);
			PlayerPrefs.SetInt ("leaderboardScore", leaderboardScore + points);
			leaderboardScore += points;
			PlayerPrefs.SetInt ("LeaderboardScore", leaderboardScore);
			saveToCloud.AddScoreToLeaderboard (MakeLightGPG.leaderboard_leaderboard, (long)leaderboardScore);
		}
	}

	private void TwoStars(){
		int points;
		StartCoroutine(TwoStarSequence ());
		points = Mathf.RoundToInt (7000 + ((10000 / currentTime) * 2));
		pointsText.text = points.ToString() + " Points";
		pointsSlider.value = ((float)points / 10000f);
		if ((PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed")) == 0 || ((PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed") == 1) && (PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Stars") <= 2) || (PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Points") <= points))) {
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Stars", 2);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed", 1);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Points", points);
			leaderboardScore += points;
			PlayerPrefs.SetInt ("LeaderboardScore", leaderboardScore);
			saveToCloud.AddScoreToLeaderboard (MakeLightGPG.leaderboard_leaderboard, (long)leaderboardScore);
		}
	}

	private void ThreeStars(){
		int points;
		StartCoroutine(ThreeStarSequence ());
		points = Mathf.RoundToInt (10000 - ((10000 / currentTime) / 3));
		pointsText.text = points.ToString() + " Points";
		pointsSlider.value = ((float)points / 10000f);
		if ((PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed") == 0) || ((PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed") == 1) && (PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Stars") <= 3) || (PlayerPrefs.GetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Points") <= points))) {
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Stars", 3);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Completed", 1);
			PlayerPrefs.SetInt (SceneManager.GetActiveScene ().buildIndex.ToString () + "Points", points);
			leaderboardScore += points;
			PlayerPrefs.SetInt ("LeaderboardScore", leaderboardScore);
			saveToCloud.AddScoreToLeaderboard (MakeLightGPG.leaderboard_leaderboard, (long)leaderboardScore);
		}
	}
	public void CompletionScreen(){
		VariablesToSave.coins += 1;
		PlayerPrefs.SetInt ("Coins", VariablesToSave.coins);
		if (SceneManager.GetActiveScene ().buildIndex == 10) {
			Social.ReportProgress (MakeLightGPG.achievement_10_levels_completed, 100, success => {
			});
		} else if (SceneManager.GetActiveScene ().buildIndex == 49) {
			Social.ReportProgress (MakeLightGPG.achievement_50_levels_completed, 100, success => {
			});
		} else if (SceneManager.GetActiveScene ().buildIndex == 50) {
			PlayerPrefs.SetInt ("FinishedTutorial", 1);
		}
		completionTime.text = (Mathf.Floor (currentTime / 60f)).ToString() + " min " + (currentTime - (Mathf.Floor (currentTime / 60f)) * 60).ToString() + " sec";
		PlayerPrefs.SetString (SceneManager.GetActiveScene ().buildIndex.ToString() + "Completion Time", completionTime.text);
		partsCompleted.text = getItemsCountFinal.ToString () + "/" + itemCountStart.ToString () + " - " + (Mathf.RoundToInt(((float)getItemsCountFinal / (float)itemCountStart) * 100)).ToString() + "%";
		PlayerPrefs.SetString (SceneManager.GetActiveScene ().buildIndex.ToString () + "Parts", partsCompleted.text);
		completionPanel.gameObject.SetActive (true);
		if (currentTime <= itemCountStart * 4 && (float)getItemsCountFinal / (float)itemCountStart == 1f) {
			ThreeStars ();
		} else if (currentTime <= itemCountStart * 4 && (float)getItemsCountFinal / (float)itemCountStart < 1f && (float)getItemsCountFinal / (float)itemCountStart >= 0.80f) {
			TwoStars ();
		} else if ((currentTime <= itemCountStart * 8 && currentTime > itemCountStart * 4)) {
			TwoStars ();
		} else if ((currentTime <= itemCountStart * 12 && currentTime > itemCountStart * 8)) {
			OneStar ();
		} else if ((currentTime <= itemCountStart * 16 && currentTime > itemCountStart * 12) && (float)getItemsCountFinal / (float)itemCountStart != 1f) {
			ZeroStars ();
		} else if (currentTime > itemCountStart * 16) {
			ZeroStars ();
		}
	}

	IEnumerator StartTimer(){
		while (circuitComplete == false) {
			yield return new WaitForSeconds (1.0f);
			currentTime += 1;
		}
	}
	IEnumerator OneStarSequence(){
		yield return new WaitForSeconds (0.5f);
		leftStar.gameObject.SetActive (true);
		middleStar.gameObject.SetActive (false);
		rightStar.gameObject.SetActive (false);
	}
	IEnumerator TwoStarSequence(){
		yield return new WaitForSeconds (0.5f);
		leftStar.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		middleStar.gameObject.SetActive (true);
		rightStar.gameObject.SetActive (false);
	}
	IEnumerator ThreeStarSequence(){
		yield return new WaitForSeconds (0.5f);
		leftStar.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		middleStar.gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		rightStar.gameObject.SetActive (true);
	}
}
