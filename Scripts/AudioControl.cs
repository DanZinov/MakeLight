using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioControl : MonoBehaviour {
	public Slider buttonClick;
	public Slider backgroundMusic;
	public AudioSource buttonClickMusic;
	public AudioSource backgroundSource;
	public AudioSource star1;
	public AudioSource star2;
	public AudioSource star3;
	public AudioSource completion;


	public AudioSource buttonClickDrop;
	public AudioClip clickDrop;

	void Start(){
		backgroundMusic.value = PlayerPrefs.GetFloat ("BackgroundMusic");
		buttonClick.value = PlayerPrefs.GetFloat ("OtherSounds");
	}

	void Update () {
		buttonClickMusic.volume = buttonClick.value;
		backgroundSource.volume = backgroundMusic.value;
		if (SceneManager.GetActiveScene ().buildIndex != 51) {
			completion.volume = buttonClick.value;
			star1.volume = buttonClick.value;
			star2.volume = buttonClick.value;
			star3.volume = buttonClick.value;
		}
	}

	public void OnDropSound(){
		buttonClickDrop.PlayOneShot (clickDrop);
	}

}
