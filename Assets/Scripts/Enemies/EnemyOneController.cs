using UnityEngine;
using System.Collections;

public class EnemyOneController : MonoBehaviour {

	public float enemySpeed = 3.5f;

	private Transform playerTransform;
	private Transform selfTransform;
	private Rigidbody2D selfBody;

	private Vector3 playerPosition;
	private Vector3 selfPosition;
	private Vector3 distance;


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
		distance = playerPosition - selfPosition;
		Debug.Log(distance);
	}

}
