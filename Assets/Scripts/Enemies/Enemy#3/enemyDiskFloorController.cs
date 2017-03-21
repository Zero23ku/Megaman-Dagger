using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDiskFloorController : MonoBehaviour {

    /*public Transform healthItemPrefab;
	public Transform invulnerabilityItemPrefab;
    public Transform threeBulletsItemPrefab;
    public Transform moreBulletsItemPrefab;
    public Transform moreBulletSpeedPrefab;
    */
    public Transform[] items;

    private enemyInformationScript enemyInformation;
	private Rigidbody2D selfBody;

	// Use this for initialization
	void Start () {
		selfBody = GetComponent<Rigidbody2D>();
		enemyInformation = GetComponent<enemyInformationScript>();
    }
	
	// Update is called once per frame
	void Update () {
		Movement();
	}
		
	void Movement() {
		selfBody.velocity = new Vector2(enemyInformation.speed, selfBody.velocity.y);
	}

	public void receiveDamage() {
		enemyInformation.health--;
		if (enemyInformation.health <= 0) {
			dropItem();
			Die();
		}
	}

	void Die() {
		WaveManager.timeBetweenWaves += enemyInformation.bonusTimeInFrames;
		Destroy(gameObject);
	}
    void dropItem() {
        Transform itemTransform;
        //if you get anything higher than 0.6 then enemy will drop something
        if (Random.Range(0.0f, 1.0f) > 0.6f) {
            int item = Random.Range(0, 6);
            //Health item
            if (item == 0) {
                itemTransform = Instantiate(items[item]) as Transform;
            }
            //Invulnerability item
            else if (item == 1) {
                itemTransform = Instantiate(items[item]) as Transform;
            }
            //Three Bullets item
            else if (item == 2) {
                itemTransform = Instantiate(items[item]) as Transform;
            }
            //More bullet Speed
            else if (item == 3) {
                itemTransform = Instantiate(items[item]) as Transform;
            }
            //More Bullets item
            else if (item == 4) {
                itemTransform = Instantiate(items[item]) as Transform;
                //More Speed Item
            } else {
                itemTransform = Instantiate(items[item]) as Transform;
            }
            itemTransform.position = transform.position;
        }

    }
}
