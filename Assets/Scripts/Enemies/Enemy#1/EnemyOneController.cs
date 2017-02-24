﻿using UnityEngine;
using System.Collections;


public class EnemyOneController : MonoBehaviour {

	private enemyInformationScript enemyInformation;
	private GameObject player;
	private Transform playerTransform;
	private Transform selfTransform;
	private Rigidbody2D selfBody;

	private Vector3 playerPosition;
	private Vector3 selfPosition;
	private Vector3 distance;
	private float selfPositionY;
	private bool isLookingLeft;


	// Use this for initialization
	void Start () {
		enemyInformation = GetComponent<enemyInformationScript> ();
		player = GameObject.FindWithTag("Player");
		playerTransform = GameObject.FindWithTag ("Player").transform;
		selfTransform = GetComponent<Transform> ();
		selfBody = GetComponent<Rigidbody2D>();
		if (selfTransform.localScale.x > 0) {
			isLookingLeft = true;
		}
		else {
			isLookingLeft = false;
		}
	}

	void FixedUpdate () {
	
	}
	// Update is called once per frame
	void Update () {
		if(!GameManager.isPaused)
			Movement();
	}

	//Movement of enemy
	void Movement() {
		if (player)
			playerPosition = playerTransform.position;
		
		selfPosition = selfTransform.position;
		//Distance between player and enemy//
		distance = playerPosition - selfPosition;
		if (distance.x <= 0.0f) {
			selfBody.velocity = new Vector2(-enemyInformation.speed,Mathf.Round(selfBody.velocity.y));
			if (!isLookingLeft) {
				Flip();
			}

		}
		else { 
			selfBody.velocity = new Vector2(enemyInformation.speed, Mathf.Round(selfBody.velocity.y));
			if (isLookingLeft) {
				Flip();
			}
		}

		if (distance.y <= -0.5f) {	
			selfBody.velocity = new Vector2(selfBody.velocity.x, -enemyInformation.speed);
		}
		else { 			
			selfBody.velocity = new Vector2(selfBody.velocity.x, enemyInformation.speed);

		}
	}

	public void receiveDamage() {
		enemyInformation.health--;
		if (enemyInformation.health <= 0) {
			Die();
		}
	}

	void Die() {
		Destroy(gameObject);
	}

	void Flip()
	{
		isLookingLeft = !isLookingLeft;
		Vector3 scale = selfTransform.localScale;
		scale.x *= -1;
		selfTransform.localScale = scale;
	}

}
