using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;
	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;

	private AudioSource sfxSource;
	private AudioSource musicSource;
	private float musicVolume;
	private bool soundPaused = false;
	private Slider volumeControl;
	private GameObject volumeObject;
	private Slider SFXControl;
	private GameObject SFXObject;
    private string currentSceneName;
    
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
 //       volumeControl = GameObject.FindWithTag("sliderVolume").GetComponent<Slider>();
        currentSceneName = SceneManager.GetActiveScene().name;
        /*if (currentSceneName == "Main Menu") {
            volumeObject = GameObject.FindGameObjectsWithTag("sliderVolume")[0];
            SFXObject = GameObject.FindGameObjectsWithTag("sliderVolume")[1];
            //SFXControl = SFXObject.GetComponent<Slider>();
            //volumeControl = volumeObject.GetComponent<Slider>();
        }*/
	}
	
	// Update is called once per frame
	void Update () {

        currentSceneName = SceneManager.GetActiveScene().name;
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
        /*
        if (currentSceneName == "Main Menu") {
 //          volumeObject = GameObject.FindGameObjectsWithTag("sliderVolume")[0];
 //           SFXObject = GameObject.FindGameObjectsWithTag("sliderVolume")[1];
            volumeControl = volumeObject.GetComponent<Slider>();
            SFXControl = SFXObject.GetComponent<Slider>();
            musicSource.volume = volumeControl.value;
            sfxSource.volume = SFXControl.value;
        }*/
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
