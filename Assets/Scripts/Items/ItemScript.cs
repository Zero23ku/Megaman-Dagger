﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

	public bool isInvulnerabilityItem;
    public bool isHealthItem;
    public bool isThreeBulletsItem;
    public bool isMoreBulletsItem;
    public bool isMoreBulletSpeedItem;
    public bool isMoreSpeedItem;
    public bool isClearAll;
    public bool isLockdown;
    public AudioClip itemPickUp;

    public int giveHealth;
    public bool isUsed;
	private bool timePassed;
	private SpriteRenderer selfRenderer;

	// Use this for initialization
	void Start () {
		timePassed = false;
		isUsed = false;
		StartCoroutine(waitForFrames(300));
		selfRenderer= GetComponent<SpriteRenderer>();
        /*print(gameObject.tag);
        gameObject.tag = "SpawnAir";
        print(gameObject.tag);*/
	}
	
	// Update is called once per frame
	void Update () {
		if (isUsed || timePassed) {
            if (isUsed) {
                SoundManager.instance.RandomizeSFX(itemPickUp);
                //print("pase");
            }
            Destroy(gameObject);
		}
	}

	private IEnumerator waitForFrames(int frames) {
		while (frames > 0) {
			if (!GameManager.isPaused)
				frames--;
			yield return null;

			if (frames < 120 && !GameManager.isPaused) {
				if (frames % 10 == 0) {
					selfRenderer.enabled = false;
				}
				else if (frames % 5 == 0) {
					selfRenderer.enabled = true;
				}
			}
		}
		timePassed = true;
	}
}
