using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyThreeTriggerBoxScript : MonoBehaviour {

	private enemyDiskFloorController enemyDiskFloorController;
	private platformController platformController;

	// Use this for initialization
	void Start () {
		enemyDiskFloorController = GetComponentInParent<enemyDiskFloorController>();
	}
	

	void OnTriggerEnter2D(Collider2D otherCollider) {

		if (otherCollider.tag == "edgeBox") {
			enemyDiskFloorController.enemySpeed *= -1;
		}

		if (otherCollider.tag == "MegamanBullet") {
			enemyDiskFloorController.receiveDamage();
		}

	}

}
