using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public static bool isPaused;

	private WaveManager waveManager;
    private InputField playerNameInput;
    private string playerName = "";
    private bool isInputReferenced = false;

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
            if (!isInputReferenced) {
                isInputReferenced = true;
                playerNameInput = GameObject.Find("PlayerNameInput").GetComponent<InputField>();
                playerNameInput.text = playerName;
            }

            if (Input.GetButtonDown("Jump")) {
                waveManager.ResetScene();
                ScoreManager.ResetScene();
                playerName = playerNameInput.gameObject.GetComponentsInChildren<Text>()[1].text;
                isInputReferenced = false;
                SceneManager.LoadScene("Scene 1");
            }
            playerNameInput.ActivateInputField();            
		} else if (currentSceneName == "Scene 1") {
			if (Input.GetButtonDown("Pause")) {
				if (isPaused) {
					Unpause();
				} else {
					Pause();
				}
			}
		} else { // Game Over
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
        if (playerName == "")
            playerName = "Player";
        ScoreManager.instance.SetScore(playerName);
		SceneManager.LoadScene("Game Over");
	}
}
