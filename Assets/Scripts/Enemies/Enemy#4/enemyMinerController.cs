using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMinerController : MonoBehaviour {
	public Transform pickaxeBulletPrefab;

	private int framesToAttack = 150;
	private int frameCounter;
	private Transform selfTransform;


	// Use this for initialization
	void Start () {
		frameCounter = 0;
		selfTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

		if(!GameManager.isPaused) {
			frameCounter++;
			if (frameCounter == framesToAttack) {
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

	/*
	public void receiveDamage() -{
		enemyInformation.health--;
		if (enemyInformation.health <= 0) {
			Die();
		}
	}*/

	void Die() {
		Destroy(gameObject);
	}

}
