using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEntry : ScriptableObject {
	public string PlayerName { get; set; }
	public int Score { get; set; }

	public void Init(string newName, int newScore) {
		PlayerName = newName;
		Score = newScore;
	}

	public static ScoreEntry CreateInstance(string newName, int newScore) {
		var data = ScriptableObject.CreateInstance<ScoreEntry>();
		data.Init(newName, newScore);
		return data;
	}
}