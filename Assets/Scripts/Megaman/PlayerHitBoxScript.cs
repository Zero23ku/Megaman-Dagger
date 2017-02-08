using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBoxScript : MonoBehaviour {

	private MegamanController megamanController;

	// Use this for initialization
	void Start () {
		megamanController = GetComponentInParent<MegamanController>();
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "enemyHitBox")
			megamanController.receiveDamage();
	}
}
