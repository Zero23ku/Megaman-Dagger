using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MegamanDeathController : MonoBehaviour {

    public float selfSpeed = 1.7f;
    public string direction;

    private Rigidbody2D selfBody;


	// Use this for initialization
	void Start () {
        selfBody = GetComponent<Rigidbody2D>();
        if (direction == "left" || direction == "right") {
            selfBody.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
	}
	
	// Update is called once per frame
	void Update () {
        print(direction);
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Main Menu" || sceneName == "Game Over")
            Destroy(gameObject);
        Movement(direction);
        
	}


    public void Movement(string direction) {
        if (direction == "up") {
            selfBody.velocity = new Vector2(selfBody.velocity.x , selfSpeed);
        }else if (direction == "down") {
            selfBody.velocity = new Vector2(selfBody.velocity.x, -selfSpeed);
        }
        else if (direction == "left") {
            selfBody.velocity = new Vector2(-selfSpeed, selfBody.velocity.y);
        }
        else {
            selfBody.velocity = new Vector2(selfSpeed, selfBody.velocity.y);
        }
    }

}
