using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoController : MonoBehaviour {
    public int waitFramesUntilAttack = 60;
    public AudioClip bulletSFX;
    public Transform bulletEnemyTwoPrefab;

    private enemyInformationScript enemyInformation;
    private SpriteRenderer spriteRenderer;
	private GameObject player;
    private Transform selfTransform;
	private Rigidbody2D selfBody;
        
    private bool goingRight;
	private int framesCounter;
    private bool alreadyLockedDown;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
		enemyInformation = GetComponent<enemyInformationScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
		selfTransform = GetComponent<Transform>();
		selfBody = GetComponent<Rigidbody2D>();

		//Know where is going the enemy for the first time.
		if (selfTransform.localScale.x > 0) {
			goingRight = false;
		} else {
			goingRight = true;
		}

		framesCounter = waitFramesUntilAttack;
        alreadyLockedDown = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.isPaused && player && !enemyInformation.isDead) {
            if (!enemyInformation.isLockdown) { 
			    ChangeDirectionMovement(); 
			    Movement();
			    framesCounter--;
			    if (framesCounter < 0) {
				    Attack();
				    framesCounter = waitFramesUntilAttack;
			    }
            } else {
                if (!alreadyLockedDown) {
                    alreadyLockedDown = true;
                    StartCoroutine(Lockdown(enemyInformation.lockdownFrames));
                }
            }
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
			enemyInformation.Die();
		}
	}

	void Attack() {
		GameObject enemyTwoBullet = Instantiate(bulletEnemyTwoPrefab).gameObject;
		enemyTwoBullet.GetComponent<EnemyTwoBulletScript>().bulletSpeed += enemyInformation.buffAttack;
		enemyTwoBullet.transform.position = new Vector3(selfTransform.position.x, selfTransform.position.y, selfTransform.position.z);

		SoundManager.instance.RandomizeSFX(bulletSFX);
	}
}