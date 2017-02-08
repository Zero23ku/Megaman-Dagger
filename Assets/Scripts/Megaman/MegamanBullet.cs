﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanBullet : MonoBehaviour {
	public float damage;

	private float bulletSpeed = 9.5f;
	private float spriteWidthDelta;
	private Rigidbody2D bulletBody;

	// Use this for initialization
	void Start () {
		bulletBody = GetComponent<Rigidbody2D>();
		spriteWidthDelta = GetComponent<SpriteRenderer>().bounds.size.x / 2;
	}
	
	// Update is called once per frame
	void Update () {
		bulletBody.velocity = new Vector2(bulletSpeed, bulletBody.velocity.y);

		//Destroy bullet if surpass right border.
		if (bulletSpeed > 0f) {
			if (transform.position.x + spriteWidthDelta > SpriteManager.maxPos.x) {
				Destroy(gameObject);
			}
		} else if (transform.position.x + spriteWidthDelta < SpriteManager.minPos.x) {
			Destroy(gameObject);
		}

	}

	public void SetDirection(bool lookingRight) {
		if (!lookingRight) {
			bulletSpeed *= -1;
		}		
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "enemyHitBox")
			Destroy(gameObject);
	}
}