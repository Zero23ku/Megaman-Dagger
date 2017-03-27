﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDiskFloorController : MonoBehaviour {
    private enemyInformationScript enemyInformation;
	private Rigidbody2D selfBody;
    private SpriteRenderer spriteRenderer;

    private bool alreadyLockedDown;

	// Use this for initialization
	void Start () {
		selfBody = GetComponent<Rigidbody2D>();
		enemyInformation = GetComponent<enemyInformationScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        alreadyLockedDown = false;
    }
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
        if (!GameManager.isPaused) {
            Movement();
=======
        if(!enemyInformation.isLockdown) {
            Movement();
        } else {
            if(!alreadyLockedDown) {
                alreadyLockedDown = true;
                StartCoroutine(Lockdown(enemyInformation.lockdownFrames));
            }
>>>>>>> refs/remotes/origin/master
        }
	}

    void Movement() {
        if(!enemyInformation.isDead) {
            selfBody.velocity = new Vector2(enemyInformation.speed, selfBody.velocity.y);
        }
	}

	public void receiveDamage() {
		enemyInformation.health--;
		if (enemyInformation.health <= 0) {
			enemyInformation.Die();
		}
	}

    private IEnumerator Lockdown(int frames) {
        spriteRenderer.color = new Color(0, 0, 255);
        selfBody.velocity = new Vector2(0f, 0f);
        Debug.Log("!");
        while(frames > 0) {
            if(!GameManager.isPaused) {
                frames--;
                yield return null;
            }
        }
        spriteRenderer.color = new Color(255, 255, 255);
        alreadyLockedDown = false;
        enemyInformation.isLockdown = false;
    }
}
