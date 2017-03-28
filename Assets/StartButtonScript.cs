using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class StartButtonScript : MonoBehaviour {

    private WaveManager waveManager;
    private InputField playerNameInput;
    private string playerName;

    // Use this for initialization
    void Start () {
        waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
    }

    public void StartGame() {
        playerNameInput = GameObject.Find("PlayerNameInput").GetComponent<InputField>();
        playerName = playerNameInput.gameObject.GetComponentsInChildren<Text>()[1].text;
        if(GameManager.playerName != playerName && playerName != "")
            GameManager.playerName = playerName;
        waveManager.ResetScene();
        ScoreManager.ResetScene();
        WaveManager.currentSet = 0;
        WaveManager.setCount = 0;
        WaveManager.firstWaveSpawned = false;
        SceneManager.LoadScene("Scene 1");
    }
}
