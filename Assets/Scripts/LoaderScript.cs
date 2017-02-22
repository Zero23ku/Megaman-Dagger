using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderScript : MonoBehaviour {
	public GameObject gameManager;
	public GameObject scoreManager;
	public GameObject soundManager;
	public GameObject waveManager;

	// Use this for initialization
	void Awake () {
		if (!GameManager.instance) {
			Instantiate(gameManager);
			Instantiate(scoreManager);
			Instantiate(soundManager);
			Instantiate(waveManager);
		}
	}
}