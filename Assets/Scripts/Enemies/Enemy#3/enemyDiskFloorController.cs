using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDiskFloorController : MonoBehaviour {

	public int health = 3;
	public float enemySpeed = 3.0f;

	private Rigidbody2D selfBody;

	// Use this for initialization
	void Start () {
		selfBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
	}
		
	void Movement() {
		selfBody.velocity = new Vector2(enemySpeed,selfBody.velocity.y);
	}

	public void receiveDamage() {
		health--;
		if (health <= 0) {
			Die();
		}
	}

	void Die() {
		Destroy(gameObject);
	}
}
