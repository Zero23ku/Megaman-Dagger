using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDiskFloorController : MonoBehaviour {

	public int healt;
	public float enemySpeed = 3.0f;

	public Collider2D leftLimit;
	public Collider2D rightLimit;
	public Rigidbody2D selfBody;
	public GameObject platform;


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
}
