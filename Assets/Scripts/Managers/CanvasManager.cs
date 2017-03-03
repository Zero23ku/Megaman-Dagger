using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	private GameObject player;
	private MegamanController megamanController;

	private Text scoreText;
	private Text bonusWaveText;
	private Text highScoreText;
	private Text waveCount;
	private Image healthBarFill;
	private int maxPlayerHealth;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		megamanController = player.GetComponent<MegamanController> ();

		maxPlayerHealth = megamanController.health;

		scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text> ();
		bonusWaveText = GameObject.FindGameObjectWithTag("BonusWave").GetComponent<Text> ();
		highScoreText = GameObject.FindGameObjectWithTag("HighScore").GetComponent<Text> ();
		waveCount = GameObject.FindGameObjectWithTag("WaveCount").GetComponent<Text> ();
		healthBarFill = GameObject.FindGameObjectWithTag("HealthBarFill").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		// Shows score on HUD (ScoreText)
		scoreText.text = ScoreManager.score.ToString("n0");
		bonusWaveText.text = ScoreManager.bonusWave.ToString("n0");
		highScoreText.text = ScoreManager.highScore.ToString("n0");
		waveCount.text = WaveManager.waveCount.ToString("n0");

		if (player) {
			healthBarFill.fillAmount = (float)megamanController.health / (float)maxPlayerHealth;
		} else {
			healthBarFill.fillAmount = 0f;
		}
	}

}