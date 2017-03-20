using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDiskFloorController : MonoBehaviour {

	public Transform healthItemPrefab;
	public Transform invulnerabilityItemPrefab;

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
	void dropItem()
	{
		Transform itemTransform;
		//if you get anything higher than 0.6 then enemy will drop something
		if (Random.Range(0.0f, 1.0f) > 0.6f)
		{
			//if you get anything higher than 0.85 then enemy will drop invulnerability item
			//otherwise it will drop health item
			if (Random.Range(0.0f, 1.0f) > 0.85f)
			{
				itemTransform = Instantiate(invulnerabilityItemPrefab) as Transform;
			}
			else
			{
				itemTransform = Instantiate(healthItemPrefab) as Transform;
			}

			itemTransform.position = transform.position;
		}

	}
}
