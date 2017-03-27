using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMinerController : MonoBehaviour {
    public Transform pickaxeBulletPrefab;

    private enemyInformationScript enemyInformation;
    private SpriteRenderer spriteRenderer;
	private int framesToAttack = 150;
	private int frameCounter;
	private GameObject player;
	private Transform selfTransform;
	private Animator selfAnimator;
	private Transform playerTransform;
	private Vector3 selfScale;
    private bool alreadyLockedDown;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (player)
            playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
		frameCounter = 0;
		selfTransform = GetComponent<Transform>();
		enemyInformation = GetComponent<enemyInformationScript>();
		selfAnimator = GetComponent<Animator>();

        alreadyLockedDown = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(!GameManager.isPaused && player && !enemyInformation.isDead) {
            if(!enemyInformation.isLockdown) {

                if(player) {
                    if(playerTransform.position.x < selfTransform.position.x && selfTransform.localScale.x < 0 && selfAnimator.GetBool("isVulnerable") == false) {
                        turnDirection();
                    } else if(playerTransform.position.x > selfTransform.position.x && selfTransform.localScale.x > 0 && selfAnimator.GetBool("isVulnerable") == false) {
                        turnDirection();
                    }
                }

                frameCounter++;
                if(frameCounter == framesToAttack) {
                    selfAnimator.SetBool("isTimeToAttack", true);
                    Attack();
                    frameCounter = 0;
                }
            } else {
                if(!alreadyLockedDown) {
                    alreadyLockedDown = true;
                    StartCoroutine(Lockdown(enemyInformation.lockdownFrames));
                }
            }
		}
		
	}

    private IEnumerator Lockdown(int frames) {
        spriteRenderer.color = new Color(0, 0, 255);
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

    void Attack() {
		GameObject pickaxe = Instantiate(pickaxeBulletPrefab).gameObject;
		pickaxe.GetComponent<pickaxeController>().bulletSpeed += enemyInformation.buffAttack;
		pickaxe.transform.position = new Vector3(selfTransform.position.x, selfTransform.position.y, selfTransform.position.z);
	}


	public void receiveDamage() {
		if (selfAnimator.GetBool("isVulnerable") == true || enemyInformation.isLockdown) {
			enemyInformation.health--;
			if (enemyInformation.health <= 0) {
				
				enemyInformation.Die();
			}
		}
	}

	void turnDirection() { 
		selfScale = selfTransform.localScale;
		selfScale.x *= -1;
		selfTransform.localScale = selfScale;
	}
}
