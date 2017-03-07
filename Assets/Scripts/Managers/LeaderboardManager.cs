using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour {
	public GameObject playerScoreEntryPrefab;

	private int maximumPlayerList = 5;
	private int currentPlayerList = 0;

	// Use this for initialization
	void Start () {

		List<ScoreEntry> playerNames = ScoreManager.instance.GetPlayerNames();
		playerNames.Sort((p1, p2) => p2.Score.CompareTo(p1.Score));

		foreach (ScoreEntry	player in playerNames) {
			currentPlayerList++;
			if (currentPlayerList > maximumPlayerList) {
				break;
			}

			GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
			go.transform.SetParent(this.transform, false);
			go.transform.FindChild("Name").GetComponent<Text> ().text = player.PlayerName;
			go.transform.FindChild("Score").GetComponent<Text> ().text = player.Score.ToString();
		}

		currentPlayerList = 0;
	}
}
