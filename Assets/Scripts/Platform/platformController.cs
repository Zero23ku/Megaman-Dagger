using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : MonoBehaviour {

	// Use this for initialization

	void Start () {

	}

	void OnTriggerEnter2D(Collider2D otherCollider) {

		//print(otherCollider.tag);		if (otherCollider.tag == "playerHitBox") {
			GameObject.FindWithTag("Enemy").GetComponent<enemyDiskFloorController>().enemySpeed *= 2;
		}
	}

	void OnTriggerExit2D(Collider2D otherCollider) {		
		if (otherCollider.tag == "playerHitBox") {
			GameObject.FindWithTag("Enemy").GetComponent<enemyDiskFloorController>().enemySpeed /= 2;
		}
	}
	

}
