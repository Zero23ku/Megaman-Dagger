using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMinerHitBoxScript : MonoBehaviour {

	private enemyMinerController enemyMinerController;

	// Use this for initialization
	void Start () {
		enemyMinerController = GetComponentInParent<enemyMinerController>();
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "MegamanBullet") {
			enemyMinerController.receiveDamage();
		}
	}
}
