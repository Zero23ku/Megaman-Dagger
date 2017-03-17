using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickaxeController : MonoBehaviour {
	public int bulletDamage;
	public float bulletSpeed = 10f;

	private float spriteWithDelta;
	private float spriteHeightDelta;
	private GameObject player;
	private Transform selfTransform;
	private Vector3 playerPosition;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		spriteWithDelta = GetComponent<SpriteRenderer>().bounds.size.x / 2;
		spriteHeightDelta = GetComponent<SpriteRenderer>().bounds.size.y / 2;
		selfTransform = GetComponent<Transform>();
		player = GameObject.FindWithTag("Player");
		if (player) {
			playerPosition = player.GetComponent<Transform>().position;
			direction = (playerPosition - selfTransform.position).normalized;
		}
	}
	
	// Update is called once per frame
	void Update () {
		selfTransform.position += direction * bulletSpeed * Time.deltaTime;

		DestroyBulletOutsideCamera();
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "playerHitBox")
			Destroy(gameObject);
		if (otherCollider.tag == "MegamanBullet")
			Destroy(gameObject);
	}

	void DestroyBulletOutsideCamera() {
		//Destroy bullet-
		if (selfTransform.position.x + spriteWithDelta < SpriteManager.minPos.x) {
			Destroy(gameObject);
		}
		else if (selfTransform.position.x + spriteWithDelta > SpriteManager.maxPos.x) {
			Destroy(gameObject);
		}
		else if (selfTransform.position.y + spriteHeightDelta > SpriteManager.maxPos.y) {
			Destroy(gameObject);
		}
		else if (selfTransform.position.y + spriteHeightDelta < SpriteManager.minPos.y) {
			Destroy(gameObject);
		}
	}
}
