	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class MegamanBullet : MonoBehaviour {

		public float damage;

		private float bulletSpeed = 9.5f;
		private float spriteWidthDelta;
		private Rigidbody2D bulletBody;
		private Transform playerTransform;
		private Vector2 playerScale;
		// Use this for initialization
		void Start () {
			spriteWidthDelta = GetComponent<SpriteRenderer>().bounds.size.x / 2;
			bulletBody = GetComponent<Rigidbody2D>();
			playerTransform = GameObject.FindWithTag("Player").transform;
		}
		
		// Update is called once per frame
		void Update () {
			playerScale = playerTransform.localScale;
			BulletMovement(playerScale.x);
			//Destroy bullet if surpass right border.
			if (transform.position.x + spriteWidthDelta > SpriteManager.maxPos.x) {
				Destroy(gameObject);
			}
			
		}

		void BulletMovement(float direction) {
			if (direction > 0)
			{
				bulletBody.velocity = new Vector2(bulletSpeed, bulletBody.velocity.y);
			}
			else { 
				bulletBody.velocity = new Vector2(bulletSpeed*-1, bulletBody.velocity.y);
			}
			
		}
	}
