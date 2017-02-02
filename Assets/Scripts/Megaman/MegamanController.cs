using UnityEngine;
using System.Collections;

public class MegamanController : MonoBehaviour {

	public float maxSpeed = 4f;
	public Vector2 jumpVector;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public float jumpVerticalSpeed = 1f;
	/*
	 * GCRadius = 0.28
	 * MinJump H = 0.2
	 * MaxJump H = 2.2
	 * Time to Jump = 0.32
	 */

	private bool grounded;
	private bool lookingRight = true;
	private Animator zeroAnimator;                            
	private Rigidbody2D zeroBody;
	private float move;

	//Jump//
	private bool isJumping;
	public float minJumpH;
	public float maxJumpH;
	public float timeToJumpApex;

	private float gravity;
	private float minJumpVel;
	private float maxJumpVel;

	// Use this for initialization
	void Start () {
		zeroAnimator = GetComponent<Animator> ();
		jumpVector = new Vector2 (0.0f,15.0f);
		zeroBody = GetComponent<Rigidbody2D> ();

		//Jump
		gravity = (2*maxJumpH) / (timeToJumpApex * timeToJumpApex);
		minJumpVel = Mathf.Sqrt(2*gravity*minJumpH);
		maxJumpVel = gravity * timeToJumpApex;
		zeroBody.gravityScale = gravity / Physics2D.gravity.magnitude;
	}

    // Update is called once per frame
    void Update() {
		Movement();

		if (grounded) {
			isJumping = false;
			jumpVerticalSpeed = 1f;
			zeroAnimator.SetBool ("grounded", true);
		} else {
			zeroAnimator.SetBool ("grounded",false);
			jumpVerticalSpeed = 0.45f;
		}

		zeroAnimator.SetFloat ("vSpeed",zeroBody.velocity.y);

		if(Input.GetButtonDown("Jump")) {
			zeroAnimator.SetBool ("grounded",false);
			Jump ();
		}

		if (Input.GetButtonUp ("Jump") && (!grounded || (grounded && isJumping))) {
			if (zeroBody.velocity.y > minJumpVel) {
				zeroBody.velocity = transform.up * minJumpVel;
			}
		}
    }

	// Use it for physic things
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}

    void Movement() {
        move = Input.GetAxis("Horizontal");
        zeroAnimator.SetFloat("fSpeed", Mathf.Abs(move));
		zeroBody.velocity = new Vector2(move * maxSpeed*jumpVerticalSpeed, zeroBody.velocity.y);

        // Left & Right movement
        if (move > 0 && !lookingRight) {
            Flip();
        } else {
            if (move < 0 && lookingRight) {
                Flip();
            }
        }
    }

	void Flip() {
		lookingRight = !lookingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	//Jump 
	void Jump() { 
		Vector2 currentVel = zeroBody.velocity;
		if (!isJumping && grounded) {
			isJumping = true;
			currentVel.y = maxJumpVel;
			zeroBody.velocity = currentVel;
		}
	}
}
