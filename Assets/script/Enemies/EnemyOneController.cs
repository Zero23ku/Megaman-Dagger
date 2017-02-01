using UnityEngine;
using System.Collections;

public class EnemyOneController : MonoBehaviour {
	public float maxSpeed;

	private Transform targetTransform;
	private Transform selfTransform;
	private Vector3 targetHeading;
	private Vector3 targetDirection;
	private float angle;
	private Quaternion q;


	// Use this for initialization
	void Start () {
		targetTransform = GameObject.FindWithTag ("Player").transform;
		selfTransform = GetComponent<Transform> ();

	}

	void FixedUpdate (){
	
	}
	// Update is called once per frame
	void Update () {




	}



}
