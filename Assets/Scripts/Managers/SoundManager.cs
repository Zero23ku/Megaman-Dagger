using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;
	public float lowPitchRange = 0.95f;
	public float hihPitchRange = 1.05f;

	private AudioSource sfxSource;
	private AudioSource musicSource;
	pivate float musicVolume;
	private bool isFadeReady = false;
	private bool soundPaused = false;


	void Awake() {
		if (!instance) {
			instance = this;
		}
		else if (instance != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
