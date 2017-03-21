using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDiskFloorController : MonoBehaviour {

    public Transform[] items;

    private enemyInformationScript enemyInformation;
	private Rigidbody2D selfBody;
    private Animator enemyAnimator;
    private bool isDead;

	// Use this for initialization
	void Start () {
		selfBody = GetComponent<Rigidbody2D>();
		enemyInformation = GetComponent<enemyInformationScript>();
        enemyAnimator = GetComponent<Animator>();

        isDead = false;
    }
	
	// Update is called once per frame
	void Update () {
		Movement();
	}

    void Movement() {
        if(!isDead) {
            selfBody.velocity = new Vector2(enemyInformation.speed, selfBody.velocity.y);
        }
	}

	public void receiveDamage() {
		enemyInformation.health--;
		if (enemyInformation.health <= 0) {
			dropItem();
			Die();
		}
	}

	void Die() {
        isDead = true;
        GetComponentsInChildren<BoxCollider2D>()[2].enabled = false;
        enemyAnimator.SetTrigger("tDead");
        WaveManager.timeBetweenWaves += enemyInformation.bonusTimeInFrames;
		Destroy(gameObject, 0.3f);
	}
    void dropItem() {
        Transform itemTransform;
        //if you get anything higher than 0.6 then enemy will drop something
        if (Random.Range(0.0f, 1.0f) > 0.6f) {
            int item = Random.Range(0, 6);
            itemTransform = Instantiate(items[item]) as Transform;
            itemTransform.position = transform.position;
        }

    }
}
