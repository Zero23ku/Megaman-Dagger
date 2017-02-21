using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyThreeHitBoxScript : MonoBehaviour {

	private enemyDiskFloorController enemyDiskFloorController;

	// Use this for initialization
	void Start () {
		enemyDiskFloorController = GetComponent<enemyDiskFloorController>();
	}
	

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "triggerBox")
		{
			enemyDiskFloorController.platform = otherCollider.GetComponentInParent<GameObject>();
			enemyDiskFloorController.leftLimit = enemyDiskFloorController.platform.GetComponentsInChildren<Collider2D>()[0];
			enemyDiskFloorController.rightLimit = enemyDiskFloorController.platform.GetComponentsInChildren<Collider2D>()[1];
			print(otherCollider.tag);
		}
		else if (otherCollider == enemyDiskFloorController.leftLimit)
		{
			enemyDiskFloorController.enemySpeed *= -1;
			print("IZQUIERDA");

		}
		else if (otherCollider == enemyDiskFloorController.rightLimit)
		{
			enemyDiskFloorController.enemySpeed *= -1;
			print("DERECHA");
		}
	}

}
