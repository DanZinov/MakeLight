using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountLevels : MonoBehaviour {

	public List<Transform> levelsInContainer;

	// Use this for initialization
	void Awake () {
		foreach (Transform child in transform) {
			levelsInContainer.Add (child);
		}
	}

}
