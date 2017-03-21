using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanBullet : MonoBehaviour {
	public float damage;
    public char direction;

    private float bulletSpeed = 9.5f;
    private float bulletSpeedY = 9.5f;

    private float spriteWidthDelta;
	private Rigidbody2D bulletBody;
    

	// Use this for initialization
	void Start () {
		bulletBody = GetComponent<Rigidbody2D>();
		spriteWidthDelta = GetComponent<SpriteRenderer>().bounds.size.x / 2;

}
	
	// Update is called once per frame
	void Update () {
        print("X: " + bulletSpeed + " Y:" + bulletSpeedY);
        //bulletBody.velocity = new Vector2(bulletSpeed, bulletBody.velocity.y);
        //print(bulletBody.velocity);
        if (direction == 's') {
            bulletBody.velocity = new Vector2(bulletSpeed, bulletBody.velocity.y);
            bulletBody.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else if (direction == 'u') {
            bulletBody.velocity = new Vector2(bulletSpeed, bulletSpeedY);
        }
        else if (direction == 'd') {
            bulletBody.velocity = new Vector2(bulletSpeed, -bulletSpeedY);
        }


    
        //Destroy bullet if surpass right border.
        if (bulletSpeed > 0f) {
			if (transform.position.x + spriteWidthDelta > SpriteManager.maxPos.x) {
				Destroy(gameObject);
			}
		} else if (transform.position.x + spriteWidthDelta < SpriteManager.minPos.x) {
			Destroy(gameObject);
		}

        if (transform.position.y + spriteWidthDelta > SpriteManager.maxPos.y) {
            Destroy(gameObject);
        }
        else if(transform.position.y + spriteWidthDelta < SpriteManager.minPos.y) {
            Destroy(gameObject);
        }

	}

	public void SetDirection(bool lookingRight) {
		if (!lookingRight) {
			bulletSpeed *= -1;
		}		
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "enemyHitBox")
			Destroy(gameObject);
	}

    public void fasterBullets(float adSpeed) {
        print("pase");
        if(bulletSpeed > 0)
            bulletSpeed += adSpeed;
        else
            bulletSpeed -= adSpeed;

        if(bulletSpeedY > 0)
            bulletSpeedY += adSpeed;
        else
            bulletSpeedY -= adSpeed;
    }
}