using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyThreeTriggerBoxScript : MonoBehaviour {

	private enemyDiskFloorController enemyDiskFloorController;

	// Use this for initialization
	void Start () {
		enemyDiskFloorController = GetComponent<enemyDiskFloorController>();
	}
	

	void OnTriggerEnter2D(Collider2D otherCollider) {

		if (otherCollider.tag == "triggerBox"){
			
		}

		if (otherCollider.tag == "edgeBox") {
			print("CHOQUE");
			enemyDiskFloorController.enemySpeed *= -1;


		}

	}

}
