using UnityEngine;  
using System.Collections;


public class EnemyOneController : MonoBehaviour {
    public bool getAway;
	public bool alreadyEntered;

	private enemyInformationScript enemyInformation;
    private SpriteRenderer spriteRenderer;
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
    private bool alreadyLockedDown;

    // Use this for initialization
    void Start () {
		enemyInformation = GetComponent<enemyInformationScript> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        alreadyLockedDown = false;
        StartCoroutine(Movement());
	}

	// Enemy Movement
	IEnumerator Movement() {
        while (!enemyInformation.isDead) {
            if (!GameManager.isPaused) {
                if(!enemyInformation.isLockdown) {
                    if(player) {
                        if(framesBetweenMovement == 0) {
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
                    if(Mathf.Abs(distance.x) > 2.75f && getAway) {
                        getAway = false;
                    }

                    if(!getAway) {
                        selfBody.MovePosition(Vector2.MoveTowards(selfPosition, playerPosition, enemyInformation.speed * Time.deltaTime));
                    }
                } else {
                    if(!alreadyLockedDown) {
                        alreadyLockedDown = true;
                        StartCoroutine(Lockdown(enemyInformation.lockdownFrames));
                    }

                }
            }
            yield return null;
        }
    }

    private IEnumerator Lockdown(int frames) {
        gameObject.tag = "Default";
        spriteRenderer.color = new Color(0, 0, 255);

        while(frames > 0) {
            if(!GameManager.isPaused) {
                frames--;
                yield return null;
            }
        }

        gameObject.tag = "Enemy";
        spriteRenderer.color = new Color(255, 255, 255);
        alreadyLockedDown = false;
        enemyInformation.isLockdown = false;
    }

    public IEnumerator moveAway() {
        while (getAway) {
            if(!GameManager.isPaused) {
                if(!enemyInformation.isLockdown) {
                    if(Random.Range(0.0f, 1.0f) > 0.5f && !alreadyEntered) {
                        getAwayDirection = -1;
                    } else {
                        getAwayDirection = 1;
                    }
                    selfBody.velocity = new Vector2(enemyInformation.speed * 1.7f * getAwayDirection, enemyInformation.speed * 1.7f);
                    yield return null;
                } else {
                    if(!alreadyLockedDown) {
                        alreadyLockedDown = true;
                        StartCoroutine(Lockdown(enemyInformation.lockdownFrames));
                    }
                }
            }
        }
        alreadyEntered = false;
    }

	public void receiveDamage() {
		enemyInformation.health--;
		if (enemyInformation.health <= 0) {
			enemyInformation.Die();
		}
	}

	void Flip() {
		isLookingLeft = !isLookingLeft;
		Vector3 scale = selfTransform.localScale;
		scale.x *= -1;
		selfTransform.localScale = scale;
	}
}
