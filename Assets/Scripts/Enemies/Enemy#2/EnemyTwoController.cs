using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoController : MonoBehaviour {

	/*public Transform healthItemPrefab;
	public Transform invulnerabilityItemPrefab;
    public Transform threeBulletsItemPrefab;
    public Transform moreBulletsItemPrefab;
    public Transform moreBulletSpeedPrefab;
    */
    public Transform[] items;

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
    private GameObject player;


	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
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
		if (!GameManager.isPaused && player) {
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
			dropItem();
			Die();
		}
	}

	void Die() {
		WaveManager.timeBetweenWaves += enemyInformation.bonusTimeInFrames;
		Destroy(gameObject);
	}

	void Attack() {
		GameObject enemyTwoBullet = Instantiate(bulletEnemyTwoPrefab).gameObject;
		enemyTwoBullet.GetComponent<EnemyTwoBulletScript>().bulletSpeed += enemyInformation.buffAttack;
		enemyTwoBullet.transform.position = new Vector3(selfTransform.position.x, selfTransform.position.y, selfTransform.position.z);

		SoundManager.instance.RandomizeSFX(bulletSFX);
	}
    void dropItem() {
        Transform itemTransform;
        //if you get anything higher than 0.6 then enemy will drop something
        if (Random.Range(0.0f, 1.0f) > 0.6f) {
            int item = Random.Range(0, 6);
            //Health item
            if (item == 0) {
                itemTransform = Instantiate(items[item]) as Transform;
            }
            //Invulnerability item
            else if (item == 1) {
                itemTransform = Instantiate(items[item]) as Transform;
            }
            //Three Bullets item
            else if (item == 2) {
                itemTransform = Instantiate(items[item]) as Transform;
            }
            //More bullet Speed
            else if (item == 3) {
                itemTransform = Instantiate(items[item]) as Transform;
            }
            //More Bullets item
            else if(item == 4){
                itemTransform = Instantiate(items[item]) as Transform;
            //More Speed Item
            } else {
                itemTransform = Instantiate(items[item]) as Transform;
            }
            itemTransform.position = transform.position;
        }

    }
}