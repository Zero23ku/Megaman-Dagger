using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoController : MonoBehaviour {

	private enemyInformationScript enemyInformation;
	public int waitFramesUntilAttack = 60;
	public AudioClip bulletSFX;
	public Transform bulletEnemyTwoPrefab;

	//private GameObject player;
	//private Transform playerTransform;
	private Transform selfTransform;
	private Rigidbody2D selfBody;

	//private float spriteWitdhDelta;
	private bool goingRight;
	private int framesCounter;


	// Use this for initialization
	void Start () {
		//playerTransform = GameObject.FindWithTag("Player").transform;
		enemyInformation = GetComponent<enemyInformationScript> ();
		selfTransform = GetComponent<Transform>();
		selfBody = GetComponent<Rigidbody2D>();
		//spriteWitdhDelta = GetComponent<SpriteRenderer>().bounds.size.x / 2;

		//Know where is going the enemy for the first time.
		if (selfTransform.localScale.x > 0) {
			goingRight = false;
		} else {
			goingRight = true;
		}

		framesCounter = waitFramesUntilAttack;
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.isPaused) {
			ChangeDirectionMovement();
			Movement();
			framesCounter--;
			if (framesCounter < 0) {
				Attack();
				framesCounter = waitFramesUntilAttack;
			}
		}
	}

	void Movement() {
		if(!goingRight) {
			selfBody.velocity = new Vector2(-enemyInformation.speed,selfBody.velocity.y);
		}
		else {
			selfBody.velocity = new Vector2(enemyInformation.speed,selfBody.velocity.y);
		}
	}

	void ChangeDirectionMovement() {
		if (selfTransform.position.x < -6.24/*selfTransform.position.x + spriteWitdhDelta == SpriteManager.minPos.x + 100 && !goingRight*/) {
			goingRight = true;
		}
		else if (selfTransform.position.x > 6.24/*selfTransform.position.x + spriteWitdhDelta == SpriteManager.maxPos.x - 25 && goingRight*/) {
			goingRight = false;
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

	void Attack() {
		Transform bulletEnemyTwoTransform = Instantiate(bulletEnemyTwoPrefab) as Transform;
		bulletEnemyTwoTransform.position = new Vector3(selfTransform.position.x, selfTransform.position.y, selfTransform.position.y);
		SoundManager.instance.RandomizeSFX(bulletSFX);
	}
}