using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoBulletScript : MonoBehaviour {

	public float bulletDamage;
	public float bulletSpeed = 10f;

	private float spriteWithDelta;
	private float spriteHeightDelta;
	private Transform playerTransform;
	private Vector3 playerPosition;
	private Vector3 direction;
    private Vector3 bulletForward;
    private float speedTime;

	// Use this for initialization
	void Start() {
        spriteWithDelta = GetComponentInParent<SpriteRenderer>().bounds.size.x / 2;
		spriteHeightDelta = GetComponentInParent<SpriteRenderer>().bounds.size.y / 2;
		playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerPosition = playerTransform.position;
        direction = (playerPosition - transform.position).normalized;
	}
	// Update is called once per frame
	void Update () {
        transform.position += direction * bulletSpeed * Time.deltaTime;
		DestroyBulletOutsideCamera();
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "playerHitBox")
			Destroy(gameObject);
	}

	void DestroyBulletOutsideCamera() { 
		//Destroy bullet
		if (transform.position.x + spriteWithDelta < SpriteManager.minPos.x)
		{
			Destroy(gameObject);
		}
		else if (transform.position.x + spriteWithDelta > SpriteManager.maxPos.x)
		{
			Destroy(gameObject);
		}
		else if (transform.position.y + spriteHeightDelta > SpriteManager.maxPos.y)
		{
			Destroy(gameObject);
		}
		else if (transform.position.y + spriteHeightDelta < SpriteManager.minPos.y)
		{
			Destroy(gameObject);
		}
	
	}
}
