using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	private GameObject player;
	private MegamanController megamanController;
	private Text scoreText;
	private Image healthBarFill;
	private int maxPlayerHealth;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text> ();		
		megamanController = player.GetComponent<MegamanController> ();

		healthBarFill = GameObject.FindGameObjectWithTag("HealthBarFill").GetComponent<Image>();
		maxPlayerHealth = megamanController.health;
	}
	
	// Update is called once per frame
	void Update () {
		// Shows score on HUD (ScoreText)
		scoreText.text = ScoreManager.instance.Score.ToString("n0");
		if (player) {
			healthBarFill.fillAmount = (float)megamanController.health / (float)maxPlayerHealth;
		} else {
			healthBarFill.fillAmount = 0f;
		}
	}

}