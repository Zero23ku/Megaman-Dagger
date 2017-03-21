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
            if (itemScript.isInvulnerabilityItem) {
                megamanController.becomeInvulnerable();

            }
            else if (itemScript.isHealthItem) {
                megamanController.getHealth(itemScript.giveHealth);
            }
            else if (itemScript.isThreeBulletsItem) {
                megamanController.threeBullets();
            }
            else if (itemScript.isMoreBulletsItem) {
                megamanController.moreBullets();
            }
            else if (itemScript.isMoreBulletSpeedItem) {
                megamanController.moreBulletSpeed();
            }
            else if (itemScript.isMoreSpeedItem) {
                megamanController.moreSpeed();
            }
			itemScript.isUsed = true;
		}
	}
}
