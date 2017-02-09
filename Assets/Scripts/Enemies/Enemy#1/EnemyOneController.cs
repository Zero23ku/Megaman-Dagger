using UnityEngine;
using System.Collections;


public class EnemyOneController : MonoBehaviour {

	public float enemySpeed = 1.5f;
	public int health;

	private GameObject player;
	private Transform playerTransform;
	private Transform selfTransform;
	private Rigidbody2D selfBody;

	private Vector3 playerPosition;
	private Vector3 selfPosition;
	private Vector3 distance;
	private float selfPositionY;
	private bool isLookingLeft;


	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		playerTransform = GameObject.FindWithTag ("Player").transform;
		selfTransform = GetComponent<Transform> ();
		selfBody = GetComponent<Rigidbody2D>();
		if (selfTransform.localScale.x > 0) {
			isLookingLeft = true;
		}
		else {
			isLookingLeft = false;
		}
	}

	void FixedUpdate () {
	
	}
	// Update is called once per frame
	void Update () {
		Movement();
	}

	//Movement of enemy
	void Movement() {
		if (player)
			playerPosition = playerTransform.position;
		
		selfPosition = selfTransform.position;
		//Distance between player and enemy//
		distance = playerPosition - selfPosition;
		if (distance.x <= 0.0f) {
			selfBody.velocity = new Vector2(-enemySpeed,Mathf.Round(selfBody.velocity.y));
			if (!isLookingLeft) {
				Flip();
			}

		}
		else { 
			selfBody.velocity = new Vector2(enemySpeed, Mathf.Round(selfBody.velocity.y));
			if (isLookingLeft) {
				Flip();
			}
		}

		if (distance.y <= -0.5f) {	
			selfBody.velocity = new Vector2(selfBody.velocity.x, -enemySpeed);
		}
		else { 			
			selfBody.velocity = new Vector2(selfBody.velocity.x, enemySpeed);

		}
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

	void Flip()
	{
		isLookingLeft = !isLookingLeft;
		Vector3 scale = selfTransform.localScale;
		scale.x *= -1;
		selfTransform.localScale = scale;
	}

}
