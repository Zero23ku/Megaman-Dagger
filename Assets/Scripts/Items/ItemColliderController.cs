﻿using System.Collections;
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
			//print("DICKS");
			if (itemScript.giveInvulnerability) {
				
				StartCoroutine(megamanController.getInvulnerability());

			}
			else { 
				megamanController.getHealth(itemScript.giveHealth);
			}
			itemScript.isUsed = true;
		}

	}

}
