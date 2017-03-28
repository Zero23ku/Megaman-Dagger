using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyThreeTriggerBoxScript : MonoBehaviour {

	private enemyDiskFloorController enemyDiskFloorController;
	private enemyInformationScript enemyInformation;
	private platformController platformController;
    public AudioClip Damage;
    

	// Use this for initialization
	void Start () {
		enemyDiskFloorController = GetComponentInParent<enemyDiskFloorController>();
		enemyInformation = GetComponentInParent<enemyInformationScript>();
	}
	

	void OnTriggerEnter2D(Collider2D otherCollider) {
		if (otherCollider.tag == "edgeBox") {
			enemyInformation.speed *= -1;
		}

		if (otherCollider.tag == "MegamanBullet") {
            SoundManager.instance.RandomizeSFX(Damage);
            enemyDiskFloorController.receiveDamage();
		}
	}

}
