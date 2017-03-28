using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {
	public void LoadScene() {
        SceneManager.LoadScene("Scene 1");
    }

    public void LoadCredits() {
        SceneManager.LoadScene("Credits");
    }
}