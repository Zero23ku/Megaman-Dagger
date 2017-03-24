using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoBulletScript : MonoBehaviour {

	public float bulletDamage;
	public float bulletSpeed = 10f;

	private float spriteWithDelta;
	private float spriteHeightDelta;
    private Rigidbody2D selfBody2D;
    private GameObject player;
	private Transform playerTransform;
	private Vector3 playerPosition;
	private Vector3 direction;
    private Vector3 bulletForward;
    private float speedTime;

	// Use this for initialization
	void Start() {
        selfBody2D = GetComponent<Rigidbody2D>();
        spriteWithDelta = GetComponentInParent<SpriteRenderer>().bounds.size.x / 2;
		spriteHeightDelta = GetComponentInParent<SpriteRenderer>().bounds.size.y / 2;
        player = GameObject.FindWithTag("Player");
        if(player) {
            playerPosition = player.GetComponent<Transform>().position;
            direction = (playerPosition - transform.position);
        }
        selfBody2D.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
    }
	// Update is called once per frame
	void Update () {
        //transform.position += direction * bulletSpeed * Time.deltaTime;
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
