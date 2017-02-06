using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	private Text scoreText;

	// Use this for initialization
	void Start () {
		scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();		
	}
	
	// Update is called once per frame
	void Update () {
		// Shows score on HUD (ScoreText)
		scoreText.text = ScoreManager.instance.Score.ToString("n0");
	}

}