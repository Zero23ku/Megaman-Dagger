using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTutorialController : MonoBehaviour {

    public bool buffedHealth;
    public bool buffedSpeed;

    private Rigidbody2D selfBody2D;
    private enemyInformationScript enemyInformation;
    private SpriteRenderer selfSpriteRenderer;

    private int waitUntilMove;

	// Use this for initialization
	void Start () {
        selfBody2D = GetComponent<Rigidbody2D>();
        enemyInformation = GetComponent<enemyInformationScript>();
        selfSpriteRenderer = GetComponent<SpriteRenderer>();
        if (buffedHealth) {
            selfSpriteRenderer.color = new Color(0f, 255f, 0f);
            enemyInformation.health *= 2;
        }
        if (buffedSpeed) {
            selfSpriteRenderer.color = new Color(255f, 0f, 0f);
            enemyInformation.speed += 3.0f;
        }
        waitUntilMove = 120;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.isPaused) {
            if (waitUntilMove > 0) {
                waitUntilMove--;
            } else {
                Movement();
            }
        }
		
	}

    void Movement() {
        if (!enemyInformation.isDead) {
            selfBody2D.velocity = new Vector2(enemyInformation.speed, selfBody2D.velocity.y);
        }
    }

    public void receiveDamage() {
        enemyInformation.health--;
        if (enemyInformation.health <= 0) {
            enemyInformation.Die();
        }
    }
}
