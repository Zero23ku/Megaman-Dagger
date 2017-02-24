using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDiskFloorController : MonoBehaviour {

	private enemyInformationScript enemyInformation;
	private Rigidbody2D selfBody;

	// Use this for initialization
	void Start () {
		selfBody = GetComponent<Rigidbody2D>();
		enemyInformation = GetComponent<enemyInformationScript>();
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
	}
		
	void Movement() {
		selfBody.velocity = new Vector2(enemyInformation.speed, selfBody.velocity.y);
	}

	public void receiveDamage() {
		enemyInformation.health--;
		if (enemyInformation.health <= 0) {
			Die();
		}
	}

	void Die() {
		Destroy(gameObject);
	}
}
