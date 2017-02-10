using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoBulletScript : MonoBehaviour {

	public float bulletDamage;

	private float bulletSpeed = 9.5f;
	private float spriteWithDelta;
	//private Rigidbody2D bulletBody;
	private Transform playerTransform;
	private Transform selfTransform;
	private Vector3 playerPosition;


	// Use this for initialization
	void Start() {
		//bulletBody = GetComponent<Rigidbody2D>();
		spriteWithDelta = GetComponent<SpriteRenderer>().bounds.size.x / 2;
		playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
		playerPosition = playerTransform.position;
		selfTransform = GetComponent<Transform>();

	
	}
	// Update is called once per frame
	void Update () {
		selfTransform.position = Vector3.MoveTowards(selfTransform.position,playerPosition,bulletSpeed*Time.deltaTime);
	}
}
