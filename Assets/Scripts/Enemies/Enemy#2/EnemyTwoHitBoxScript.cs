using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoHitBoxScript : MonoBehaviour
{
	private EnemyTwoController enemyTwoController;

	void Start() {
		enemyTwoController = GetComponentInParent<EnemyTwoController>();
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "MegamanBullet") {
			enemyTwoController.receiveDamage();
		}
	}
}
