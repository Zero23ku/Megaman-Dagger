using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBoxScript : MonoBehaviour {
	private EnemyOneController enemyOneController;
    private enemyInformationScript enemyInformation;
    public AudioClip Damage;

    void Start () {
        enemyOneController = GetComponentInParent<EnemyOneController>();
        enemyInformation = GetComponentInParent<enemyInformationScript>();
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "MegamanBullet") {
            SoundManager.instance.RandomizeSFX(Damage);
            enemyOneController.receiveDamage();
		}
		if (otherCollider.tag == "playerHitBox" && !enemyOneController.alreadyEntered && !enemyInformation.isLockdown) {
            enemyOneController.alreadyEntered = true;
            enemyOneController.getAway = true;
            StartCoroutine(enemyOneController.moveAway());
        }
	}
}
