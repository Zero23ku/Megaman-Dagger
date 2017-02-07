	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class MegamanBullet : MonoBehaviour {

		public float damage;

		private float bulletSpeed = 9.5f;
		private float spriteWidthDelta;
		private Rigidbody2D bulletBody;
		// Use this for initialization
		void Start () {
			spriteWidthDelta = GetComponent<SpriteRenderer>().bounds.size.x / 2;
			bulletBody = GetComponent<Rigidbody2D>();
		}
		
		// Update is called once per frame
		void Update () {
			BulletMovement();
			//Destroy bullet if surpass right border.
			if (transform.position.x + spriteWidthDelta > SpriteManager.maxPos.x) {
				Destroy(gameObject);
			}
		}

		void BulletMovement() {
			bulletBody.velocity = new Vector2(bulletSpeed,bulletBody.velocity.y);
			print(bulletBody.velocity);
		}
	}
