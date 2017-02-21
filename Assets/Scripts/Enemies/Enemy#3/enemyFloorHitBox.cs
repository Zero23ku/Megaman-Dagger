using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFloorHitBox : MonoBehaviour {
	private enemyDiskFloorController enemyDiskFloorController;
	// Use this for initialization
	void Start () {	
		enemyDiskFloorController = GetComponentInParent<enemyDiskFloorController>();
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider){

		if (otherCollider.tag == "MegamanBullet") {
			enemyDiskFloorController.receiveDamage();
		}

	}
}
