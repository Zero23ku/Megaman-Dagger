using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBoxScript : MonoBehaviour {
	private EnemyOneController enemyOneController;

	void Start () {
		enemyOneController = GetComponentInParent<EnemyOneController>();		
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "MegamanBullet") {
			enemyOneController.receiveDamage();
		}
		if (otherCollider.tag == "playerHitBox") {
			enemyOneController.getAway = true;
			enemyOneController.alreadyEntered = false;
		}
	}
}
