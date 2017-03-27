using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColliderController : MonoBehaviour {

	private ItemScript itemScript;
	private MegamanController megamanController;
    private GameObject megamanObject;

	void Start() {
		itemScript = GetComponentInParent<ItemScript>();
        megamanObject = GameObject.FindWithTag("Player");

        if (megamanObject) {
            megamanController = megamanObject.GetComponent<MegamanController>();
        }
        //megamanController = GameObject.FindWithTag("Player").GetComponent<MegamanController>();
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "playerHitBox") {
            if(itemScript.isInvulnerabilityItem) {
                megamanController.becomeInvulnerable();
            } else if(itemScript.isHealthItem) {
                megamanController.getHealth(itemScript.giveHealth);
            } else if(itemScript.isThreeBulletsItem) {
                megamanController.threeBullets();
            } else if(itemScript.isMoreBulletsItem) {
                megamanController.moreBullets();
            } else if(itemScript.isMoreBulletSpeedItem) {
                megamanController.moreBulletSpeed();
            } else if(itemScript.isMoreSpeedItem) {
                megamanController.moreSpeed();
            } else if(itemScript.isClearAll) {
                foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
                    enemy.GetComponent<enemyInformationScript>().Die();
                }
                foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("enemyMiner")) {
                    enemy.GetComponent<enemyInformationScript>().Die();
                }
            } else if(itemScript.isLockdown) {
                foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
                    enemy.GetComponent<enemyInformationScript>().isLockdown = true;
                }
                foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("enemyMiner")) {
                    enemy.GetComponent<enemyInformationScript>().isLockdown = true;
                }
            }
			itemScript.isUsed = true;
		}
	}
}
