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

		targetHeading = targetTransform.position - selfTransform.position;
		angle = Mathf.Atan2 (targetHeading.y,targetHeading.x) * Mathf.Rad2Deg;
		q = Quaternion.AngleAxis (angle, Vector3.forward);
		selfTransform.rotation = Quaternion.Slerp (selfTransform.rotation,q,Time.deltaTime*maxSpeed);

	
	}



}
