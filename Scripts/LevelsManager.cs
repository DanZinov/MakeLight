using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelsManager : MonoBehaviour {

	public TextMeshProUGUI levelNumberCompleted;
	public TextMeshProUGUI levelNumberNotCompleted;
	public CountLevels countLevels;
	public TextMeshProUGUI points;
	public TextMeshProUGUI time;
	public TextMeshProUGUI parts;
	public GameObject leftStar;
	public GameObject middleStar;
	public GameObject rightStar;
	public GameObject locked;
	public GameObject completed;
	public GameObject notCompleted;

	void Start(){
		string scene;
		int index;
		scene = (countLevels.levelsInContainer.IndexOf(this.transform) + 1).ToString();
		index = countLevels.levelsInContainer.IndexOf (this.transform) + 1;
		levelNumberCompleted.text = "Level " + (countLevels.levelsInContainer.IndexOf (this.transform) + 1).ToString ();
		levelNumberNotCompleted.text = "Level " + (countLevels.levelsInContainer.IndexOf (this.transform) + 1).ToString ();
		if (PlayerPrefs.GetInt (scene + "Completed") == 1 && PlayerPrefs.GetInt (scene + "Stars") == 1) {
			completed.gameObject.SetActive (true);
			notCompleted.gameObject.SetActive (false);
			leftStar.gameObject.SetActive (true);
			middleStar.gameObject.SetActive (false);
			rightStar.gameObject.SetActive (false);
			time.text = PlayerPrefs.GetString (scene + "Completion Time");
			points.text = PlayerPrefs.GetInt (scene + "Points").ToString() + " Points";
			parts.text = PlayerPrefs.GetString (scene + "Parts");

		} else if (PlayerPrefs.GetInt (scene + "Completed") == 1 && PlayerPrefs.GetInt (scene + "Stars") == 2) {
			completed.gameObject.SetActive (true);
			notCompleted.gameObject.SetActive (false);
			leftStar.gameObject.SetActive (true);
			middleStar.gameObject.SetActive (true);
			rightStar.gameObject.SetActive (false);
			time.text = PlayerPrefs.GetString (scene + "Completion Time");
			points.text = PlayerPrefs.GetInt (scene + "Points").ToString() + " Points";
			parts.text = PlayerPrefs.GetString (scene + "Parts");

		} else if (PlayerPrefs.GetInt (scene + "Completed") == 1 && PlayerPrefs.GetInt (scene + "Stars") == 3) {
			completed.gameObject.SetActive (true);
			notCompleted.gameObject.SetActive (false);
			leftStar.gameObject.SetActive (true);
			middleStar.gameObject.SetActive (true);
			rightStar.gameObject.SetActive (true);
			time.text = PlayerPrefs.GetString (scene + "Completion Time");
			points.text = PlayerPrefs.GetInt (scene + "Points").ToString() + " Points";
			parts.text = PlayerPrefs.GetString (scene + "Parts");

		} else if (PlayerPrefs.GetInt ((index - 1).ToString() + "Completed") == 1 && PlayerPrefs.GetInt ((index).ToString() + "Completed") == 0) {
			completed.gameObject.SetActive (false);
			notCompleted.gameObject.SetActive (true);
			locked.gameObject.SetActive (false);

		} else {
			completed.gameObject.SetActive (false);
			notCompleted.gameObject.SetActive (true);
		}
			
	}
	public void LoadLevel(){
		int index;
		index = countLevels.levelsInContainer.IndexOf (this.transform) + 1;
		if ((PlayerPrefs.GetInt ((index - 1).ToString() + "Completed") == 1) || index == 1) {
			SceneManager.LoadSceneAsync (index);
		}
	}
}
