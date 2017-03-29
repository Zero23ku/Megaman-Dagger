using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public static bool isPaused;
    public static string playerName = "";

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
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		string currentSceneName = SceneManager.GetActiveScene().name;    
 
        if (currentSceneName == "Scene 1" && Input.GetButtonDown("Pause")) {
				if (isPaused) {
					Unpause();
				} else {
					Pause();
				}
			}

        if (currentSceneName == "Game Over" || currentSceneName == "Credits") {
            if(Input.GetButtonDown("Jump") || Input.GetButtonDown("Attack")) {
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
            ScoreManager.instance.SetScore("Player");
        else
            ScoreManager.instance.SetScore(playerName);
        StartCoroutine(waitForFrames(100));
		
	}

    private IEnumerator waitForFrames(int frames) {
        while (frames > 0) {
            if (!GameManager.isPaused)
                frames--;
            yield return null;
        }
        SceneManager.LoadScene("Game Over");
    }
}
