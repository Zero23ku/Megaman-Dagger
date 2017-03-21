using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColliderController : MonoBehaviour {

	private ItemScript itemScript;
	private MegamanController megamanController;

	void Start() {
		itemScript = GetComponentInParent<ItemScript>();
		megamanController = GameObject.FindWithTag("Player").GetComponent<MegamanController>();
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
			itemScript.isUsed = true;
		}
	}
}
