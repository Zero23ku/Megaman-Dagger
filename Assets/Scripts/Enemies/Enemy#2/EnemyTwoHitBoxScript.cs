using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoHitBoxScript : MonoBehaviour{
    public AudioClip Damage;
    private EnemyTwoController enemyTwoController;

	void Start() {
		enemyTwoController = GetComponentInParent<EnemyTwoController>();
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "MegamanBullet") {
            SoundManager.instance.RandomizeSFX(Damage);
            enemyTwoController.receiveDamage();
		}
	}
}
