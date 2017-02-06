using UnityEngine;
using System.Collections;


public class EnemyOneController : MonoBehaviour {

	public float enemySpeed =1.5f;

	private Transform playerTransform;
	private Transform selfTransform;
	private Rigidbody2D selfBody;

	private Vector3 playerPosition;
	private Vector3 selfPosition;
	private Vector3 distance;
	private float selfPositionY;


	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindWithTag ("Player").transform;
		selfTransform = GetComponent<Transform> ();
		selfBody = GetComponent<Rigidbody2D>();

	}

	void FixedUpdate () {
	
	}
	// Update is called once per frame
	void Update () {
		Movement();
			
	


	}

	//Movement of enemy
	void Movement() {

		playerPosition = playerTransform.position;
		selfPosition = selfTransform.position;
		//Distance between player and enemy//
		distance = playerPosition - selfPosition;
		if (distance.x <= 0.0f) {
			selfBody.velocity = new Vector2(-enemySpeed,Mathf.Round(selfBody.velocity.y));

		}
		else { 
			selfBody.velocity = new Vector2(enemySpeed, Mathf.Round(selfBody.velocity.y));

		}

		if (distance.y <= -0.5f) {	
			selfBody.velocity = new Vector2(selfBody.velocity.x, -enemySpeed);

		}
		else { 			
			selfBody.velocity = new Vector2(selfBody.velocity.x, enemySpeed);

		}



		Debug.Log(selfTransform.position);
	}

}
