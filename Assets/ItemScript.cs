using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

	public bool giveInvulnerability;
	public int giveHealth;

	public bool isUsed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (isUsed) {
			Destroy(gameObject);
		}

	}
}
