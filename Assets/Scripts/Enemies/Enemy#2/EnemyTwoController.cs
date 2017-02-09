using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoController : MonoBehaviour {

	public float enemySpeed = 2.5f;
	public int health;

	//private GameObject player;
	private Transform playerTransform;
	private Transform selfTransform;
	private Rigidbody2D selfBody;

	private float spriteWitdhDelta;
	private bool goingRight;

	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindWithTag("Player").transform;
		selfTransform = GetComponent<Transform>();
		selfBody = GetComponent<Rigidbody2D>();
		spriteWitdhDelta = GetComponent<SpriteRenderer>().bounds.size.x / 2;

		//Know where is going the enemy for the first time.
		if (selfTransform.localScale.x > 0) {
			goingRight = false;
		}

		else {
			goingRight = true;
		}


		
	}
	
	// Update is called once per frame
	void Update () {
		ChangeDirectionMovement();
		Movement();
		print(selfTransform.position.x);
	}

	void Movement() {
		if(!goingRight) {
			selfBody.velocity = new Vector2(-enemySpeed,selfBody.velocity.y);
		}
		else {
			selfBody.velocity = new Vector2(enemySpeed,selfBody.velocity.y);
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
		health--;
		if (health <= 0)
		{
			Die();
		}
	}

	void Die() {
		Destroy(gameObject);
	}
}
