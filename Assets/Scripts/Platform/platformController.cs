using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : MonoBehaviour {

	private bool alreadyAssigned = false;
	private enemyDiskFloorController enemyController;
	private enemyInformationScript enemyInformation;

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "triggerBox" && !alreadyAssigned) {
			alreadyAssigned = true;
			enemyController = otherCollider.gameObject.GetComponentInParent<enemyDiskFloorController>();
			enemyInformation = otherCollider.gameObject.GetComponentInParent<enemyInformationScript>();
		}
		if (otherCollider.tag == "playerHitBox") {
			if (enemyController) {
				enemyInformation.speed *= 2;
			}
		}
	}

	void OnTriggerExit2D(Collider2D otherCollider) {
		if (otherCollider.tag == "playerHitBox") {
			if (enemyController) {
				enemyInformation.speed /= 2;
			}
		}
	}
}