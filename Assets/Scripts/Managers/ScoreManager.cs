using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public static int score;
	public static int bonusWave;
	public static int highScore;

	public static ScoreManager instance = null;

	private WaveManager waveManager;

	void Awake() {
		if (!instance) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start () {
		score = 0;
	}

	void Update () {
		bonusWave = WaveManager.timeBetweenWaves;
		if (score > highScore)
			highScore = score;
	}

	public static void AssignBonus() {
		score += WaveManager.timeBetweenWaves;
	}

	public static void ResetScene() {
		score = 0;
		bonusWave = 0;
	}
}
