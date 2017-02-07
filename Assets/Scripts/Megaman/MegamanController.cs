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
	private Animator megaAnimator;                            
	private Rigidbody2D megaBody;
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
		megaAnimator = GetComponent<Animator> ();
		jumpVector = new Vector2 (0.0f,15.0f);
		megaBody = GetComponent<Rigidbody2D> ();

		//Jump
		gravity = (2*maxJumpH) / (timeToJumpApex * timeToJumpApex);
		minJumpVel = Mathf.Sqrt(2*gravity*minJumpH);
		maxJumpVel = gravity * timeToJumpApex;
		megaBody.gravityScale = gravity / Physics2D.gravity.magnitude;
	}

    // Update is called once per frame
    void Update() {
		if (!GameManager.isPaused) {
			Movement();

			if (grounded) {
				isJumping = false;
				jumpVerticalSpeed = 1f;
				megaAnimator.SetBool("grounded", true);
			} else {
				megaAnimator.SetBool("grounded", false);
				jumpVerticalSpeed = 0.45f;
			}

			megaAnimator.SetFloat("vSpeed", megaBody.velocity.y);

			if (Input.GetButtonDown("Jump")) {
				megaAnimator.SetBool("grounded", false);
				Jump();
			}

			if (Input.GetButtonUp("Jump") && (!grounded || (grounded && isJumping))) {
				if (megaBody.velocity.y > minJumpVel) {
					megaBody.velocity = transform.up * minJumpVel;
				}
			}
		}
    }

	// Use it for physic things
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}

    void Movement() {
        move = Input.GetAxis("Horizontal");
        megaAnimator.SetFloat("fSpeed", Mathf.Abs(move));
		megaBody.velocity = new Vector2(move * maxSpeed*jumpVerticalSpeed, megaBody.velocity.y);

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
		Vector2 currentVel = megaBody.velocity;
		if (!isJumping && grounded) {
			isJumping = true;
			currentVel.y = maxJumpVel;
			megaBody.velocity = currentVel;
		}
	}
}
