using UnityEngine;
using System.Collections;

public class ZeroController : MonoBehaviour {

	public float maxS = 4f;
	bool right = true;
	Animator a;


	// Use this for initialization
	void Start () {
        a = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update(){
        Movement();

    }


	void FixedUpdate () {
        
	}

    void Flip()
    {
        right = !right;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Movement()
    {
        float move = Input.GetAxis("Horizontal");
        a.SetFloat("speed", Mathf.Abs(move));
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxS, GetComponent<Rigidbody2D>().velocity.y);

        //Left & Right movement//
        if (move > 0 && !right)
        {
            Flip();
        }
        else
        {
            if (move < 0 && right)
            {
                Flip();
            }
        }

        //Jump



    }
}
