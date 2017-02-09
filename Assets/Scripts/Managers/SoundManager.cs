using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;
	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;

	private AudioSource sfxSource;
	private AudioSource musicSource;
	private float musicVolume;
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
		musicSource = GetComponents<AudioSource>()[0];
		sfxSource = GetComponents<AudioSource>()[1];
		musicVolume = musicSource.volume;
	}
	
	// Update is called once per frame
	void Update () {
		if (!soundPaused && GameManager.isPaused) {
			soundPaused = true;
			sfxSource.Pause();
			musicSource.Pause();
		}
		else if (soundPaused && !GameManager.isPaused) {
			soundPaused = false;
			sfxSource.UnPause();
			musicSource.UnPause();
		}
	}

	public void PlaySingle(AudioClip clip) {
		sfxSource.clip = clip;
		sfxSource.Play();
	}

	public void RandomizeSFX(AudioClip clip) {
		float randomPitch = Random.Range(lowPitchRange,highPitchRange);
		sfxSource.pitch = randomPitch;
		sfxSource.PlayOneShot(clip);
	}

	public void PlayMusic(AudioClip newClip) {
		musicSource.clip = newClip;
		musicSource.volume = musicVolume;
		musicSource.Play();
	}
}
