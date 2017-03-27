using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTutorialGoal : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}


    private void OnTriggerEnter2D(Collider2D otherCollider) {
        if(otherCollider.tag == "playerHitBox") {
            WaveManager.firstTutorial = false;
            Destroy(gameObject);
        }
    }

}
