using UnityEngine;  
using System.Collections;


public class EnemyOneController : MonoBehaviour {
	public Transform healthItemPrefab;
	public Transform invulnerabilityItemPrefab;
	public bool getAway;
	public bool alreadyEntered;

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
	private int getAwayDirection;
    private int framesBetweenMovement;
    
	// Use this for initialization
	void Start () {
		enemyInformation = GetComponent<enemyInformationScript> ();
		player = GameObject.FindWithTag("Player");
        if (player) {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
		selfTransform = GetComponent<Transform> ();
		selfBody = GetComponent<Rigidbody2D>();
		getAway = false;
		alreadyEntered = false;
		if (selfTransform.localScale.x > 0) {
			isLookingLeft = true;
		}
		else {
			isLookingLeft = false;
		}
        framesBetweenMovement = 20;
        StartCoroutine(Movement());
	}

	// Enemy Movement
	IEnumerator Movement() {
        while (true) {
            if (!GameManager.isPaused) {
		        if (player) {
                    if (framesBetweenMovement == 0) {
                        framesBetweenMovement = 20;
                        playerPosition = playerTransform.position;
                        playerPosition.y += 0.4f;
                    } else {
                        framesBetweenMovement--;
                    }
                }

                selfPosition = selfTransform.position;
		
                // Distance between player and enemy
		        distance = playerPosition - selfPosition;
		        if (Mathf.Abs(distance.x) > 2.75f && getAway) {
			        getAway = false;
		        }

                if(!getAway) {
                    selfBody.MovePosition(Vector2.MoveTowards(selfPosition, playerPosition, enemyInformation.speed * Time.deltaTime));
                }
            }
            yield return null;
        }
    }

    public IEnumerator moveAway() {
        while (getAway) {
            if (!GameManager.isPaused) { 
                if(Random.Range(0.0f, 1.0f) > 0.5f && !alreadyEntered) {
                    getAwayDirection = -1;
                } else {
                    getAwayDirection = 1;
                }
                selfBody.velocity = new Vector2(enemyInformation.speed * 1.7f * getAwayDirection, enemyInformation.speed * 1.7f);
                yield return null;
            }
        }
        alreadyEntered = false;
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

	void Flip()
	{
		isLookingLeft = !isLookingLeft;
		Vector3 scale = selfTransform.localScale;
		scale.x *= -1;
		selfTransform.localScale = scale;
	}

	void dropItem() {
		Transform itemTransform;
		// If you get anything higher than 0.6 then enemy will drop something
		if (Random.Range(0.0f, 1.0f) > 0.6f ) {
			// If you get anything higher than 0.85 then enemy will drop invulnerability item
			// Otherwise it will drop health item
			if (Random.Range(0.0f, 1.0f) > 0.85f) {
				itemTransform = Instantiate(invulnerabilityItemPrefab) as Transform;
			}
			else {
				itemTransform = Instantiate(healthItemPrefab) as Transform;
			}
			itemTransform.position = selfPosition;
		}
	
	}
}
