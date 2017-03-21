using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoController : MonoBehaviour {
    public Transform[] items;
    public int waitFramesUntilAttack = 60;
    public AudioClip bulletSFX;
    public Transform bulletEnemyTwoPrefab;

    private enemyInformationScript enemyInformation;
    private Animator enemyAnimator;
	private GameObject player;
    private Transform selfTransform;
	private Rigidbody2D selfBody;
        
    private bool goingRight;
	private int framesCounter;
    private bool isDead;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
		enemyInformation = GetComponent<enemyInformationScript> ();
        enemyAnimator = GetComponent<Animator>();
		selfTransform = GetComponent<Transform>();
		selfBody = GetComponent<Rigidbody2D>();

        isDead = false;

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
		if (!GameManager.isPaused && player && !isDead) {
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
		if (selfTransform.position.x < -6.24) {
			goingRight = true;
		}
		else if (selfTransform.position.x > 6.24) {
			goingRight = false;
		}
	}

	public void receiveDamage() {
		enemyInformation.health--;
		if (enemyInformation.health <= 0) {
			dropItem();
			Die();
		}
	}

	void Die() {
        isDead = true;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        enemyAnimator.SetTrigger("tDead");
        WaveManager.timeBetweenWaves += enemyInformation.bonusTimeInFrames;
		Destroy(gameObject, 0.3f);
	}

	void Attack() {
		GameObject enemyTwoBullet = Instantiate(bulletEnemyTwoPrefab).gameObject;
		enemyTwoBullet.GetComponent<EnemyTwoBulletScript>().bulletSpeed += enemyInformation.buffAttack;
		enemyTwoBullet.transform.position = new Vector3(selfTransform.position.x, selfTransform.position.y, selfTransform.position.z);

		SoundManager.instance.RandomizeSFX(bulletSFX);
	}
    void dropItem() {
        Transform itemTransform;
        // If you get anything higher than 0.6 then enemy will drop something
        if (Random.Range(0.0f, 1.0f) > 0.6f) {
            int item = Random.Range(0, 6);
            itemTransform = Instantiate(items[item]) as Transform;
            itemTransform.position = transform.position;
        }
    }
}