using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public int Score { get; set; }

	public static ScoreManager instance = null;

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
		Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		// Score += 1;		
	}
}
