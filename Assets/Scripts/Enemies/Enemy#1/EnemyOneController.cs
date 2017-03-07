using UnityEngine;
using System.Collections;


public class EnemyOneController : MonoBehaviour {


	public Transform healthItemPrefab;
	public Transform invulnerabilityItemPrefab;
	public bool getAway;
	public bool alreadyEntered;

	private enemyInformationScript enemyInformation;
	private GameObject player;
	private Transform playerTransform;
	private Transform selfTransform;
	private Rigidbody2D selfBody;

	private Vector3 playerPosition;
	private Vector3 selfPosition;
	private Vector3 distance;
	private float selfPositionY;
	private bool isLookingLeft;
	private int getAwayDirection;



	// Use this for initialization
	void Start () {
		enemyInformation = GetComponent<enemyInformationScript> ();
		player = GameObject.FindWithTag("Player");
		playerTransform = GameObject.FindWithTag ("Player").transform;
		selfTransform = GetComponent<Transform> ();
		selfBody = GetComponent<Rigidbody2D>();
		getAway = false;
		alreadyEntered = false;
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
		if(!GameManager.isPaused)
			Movement();
	}

	//Movement of enemy
	void Movement() {
		if (player)
			playerPosition = playerTransform.position;
		
		selfPosition = selfTransform.position;
		//Distance between player and enemy//
		distance = playerPosition - selfPosition;
		if (Mathf.Abs(distance.x) > 2.75f && getAway) {
			getAway = false;
		}

		if (!getAway){
			if (distance.x <= 0.0f){
				selfBody.velocity = new Vector2(-enemyInformation.speed, Mathf.Round(selfBody.velocity.y));
				if (!isLookingLeft){
					Flip();
				}

			}
			else{
				selfBody.velocity = new Vector2(enemyInformation.speed, Mathf.Round(selfBody.velocity.y));
				if (isLookingLeft){
					Flip();
				}
			}

			if (distance.y <= -0.5f){
				selfBody.velocity = new Vector2(selfBody.velocity.x, -enemyInformation.speed);
			}
			else{
				selfBody.velocity = new Vector2(selfBody.velocity.x, enemyInformation.speed);

			}
		}

		if (getAway) {
			if (Random.Range(0.0f, 1.0f) > 0.5f && !alreadyEntered) {
				getAwayDirection = -1;
			}
			else
				getAwayDirection = 1;
			alreadyEntered = true;
			selfBody.velocity = new Vector2(enemyInformation.speed * 1.7f * getAwayDirection,enemyInformation.speed * 1.3f * getAwayDirection);
		}
	}

	public void receiveDamage() {
		enemyInformation.health--;
		if (enemyInformation.health <= 0) {
			dropItem();
			Die();
		}
	}

	void Die() {
		WaveManager.timeBetweenWaves += enemyInformation.bonusTimeInFrames;
		Destroy(gameObject);
	}

	void Flip()
	{
		isLookingLeft = !isLookingLeft;
		Vector3 scale = selfTransform.localScale;
		scale.x *= -1;
		selfTransform.localScale = scale;
	}

	void dropItem() {
		Transform itemTransform;
		//if you get anything higher than 0.6 then enemy will drop something
		if (Random.Range(0.0f, 1.0f) > 0.6f ) {
			//if you get anything higher than 0.85 then enemy will drop invulnerability item
			//otherwise it will drop health item
			if (Random.Range(0.0f, 1.0f) > 0.85f){
				itemTransform = Instantiate(invulnerabilityItemPrefab) as Transform;
			}
			else {
				itemTransform = Instantiate(healthItemPrefab) as Transform;
			}

			itemTransform.position = selfPosition;
		}
	
	}



}
