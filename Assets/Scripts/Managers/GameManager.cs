﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public static bool isPaused;

	private WaveManager waveManager;

	void Awake() {
		if (!instance) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		string currentSceneName = SceneManager.GetActiveScene().name;

		// Main Menu logic
		if (currentSceneName == "Main Menu") {
			if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Attack")) {
				waveManager.ResetScene();
				ScoreManager.ResetScene();
				SceneManager.LoadScene("Scene 1");
			}
		} else if (currentSceneName == "Scene 1") {
			if (Input.GetButtonDown("Pause")) {
				if (isPaused) {
					Unpause();
				} else {
					Pause();
				}
			}
		} else {
			if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Attack")) {
				SceneManager.LoadScene("Main Menu");
			}			
		}
	}

	public void Pause() {
		Time.timeScale = 0;
		isPaused = true;
	}

	public void Unpause() {
		Time.timeScale = 1;
		isPaused = false;
	}

	public void gameOver() {
		ScoreManager.instance.SetScore("Sh1ken");
		SceneManager.LoadScene("Game Over");
	}
}
