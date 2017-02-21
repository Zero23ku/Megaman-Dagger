using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderScript : MonoBehaviour {
	public GameObject gameManager;
	public GameObject scoreManager;
	public GameObject soundManager;

	// Use this for initialization
	void Awake () {
		if (!GameManager.instance) {
			Instantiate(gameManager);
			Instantiate(scoreManager);
			Instantiate(soundManager);
		}
	}
}