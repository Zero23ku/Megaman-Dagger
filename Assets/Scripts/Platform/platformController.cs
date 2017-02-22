using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : MonoBehaviour {

	private bool alreadyAssigned = false;
	private enemyDiskFloorController enemyController;

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "triggerBox" && !alreadyAssigned) {
			alreadyAssigned = true;
			enemyController = otherCollider.gameObject.GetComponentInParent<enemyDiskFloorController>();
		}
		if (otherCollider.tag == "playerHitBox") {
			if (enemyController) {
				enemyController.enemySpeed *= 2;
			}
		}
	}

	void OnTriggerExit2D(Collider2D otherCollider) {
		if (otherCollider.tag == "playerHitBox") {
			if (enemyController) {
				enemyController.enemySpeed /= 2;
			}
		}
	}
}