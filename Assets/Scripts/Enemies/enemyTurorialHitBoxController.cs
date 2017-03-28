using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTurorialHitBoxController : MonoBehaviour {

    private enemyTutorialController enemyTutorialController;
    private enemyInformationScript enemyInformation;
	// Use this for initialization
	void Start () {
        enemyTutorialController = GetComponentInParent<enemyTutorialController>();
        enemyInformation = GetComponentInParent<enemyInformationScript>();
	}

    private void OnTriggerEnter2D(Collider2D otherCollider) {
        if (otherCollider.tag == "edgeBox") {
            enemyInformation.speed *= -1;
        }

        if (otherCollider.tag == "MegamanBullet") {
            enemyTutorialController.receiveDamage();
        }
    }


}
