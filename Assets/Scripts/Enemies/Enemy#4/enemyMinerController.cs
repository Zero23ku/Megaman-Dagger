using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMinerController : MonoBehaviour {
	public Transform pickaxeBulletPrefab;

	private enemyInformationScript enemyInformation;
	private int framesToAttack = 150;
	private int frameCounter;
	private Transform selfTransform;
	private Animator selfAnimator;


	// Use this for initialization
	void Start () {
		frameCounter = 0;
		selfTransform = GetComponent<Transform>();
		enemyInformation = GetComponent<enemyInformationScript>();
		selfAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if(!GameManager.isPaused) {
			frameCounter++;
			if (frameCounter == framesToAttack) {
				selfAnimator.SetBool("isTimeToAttack",true);
				Attack();
				frameCounter = 0;
			}
		}
		
	}

	void Attack() {
		Transform bulletEnemyTwoTransform = Instantiate(pickaxeBulletPrefab) as Transform;
		bulletEnemyTwoTransform.position = new Vector3(selfTransform.position.x, selfTransform.position.y, selfTransform.position.y);
		//SoundManager.instance.RandomizeSFX(bulletSFX);
	}


	public void receiveDamage() {
		if (selfAnimator.GetBool("isVulnerable") == true) {
			enemyInformation.health--;
			if (enemyInformation.health <= 0) {
				Die();
			}
		}
	}

	void Die() {
		Destroy(gameObject);
	}

}
