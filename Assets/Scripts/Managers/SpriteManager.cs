using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

	public static Vector2 minPos;
	public static Vector2 maxPos;

	// Use this for initialization
	void Start () {

		minPos = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		maxPos = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		
	}

	
	// Update is called once per frame
	void Update () {

		minPos = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		maxPos = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

	}
}
