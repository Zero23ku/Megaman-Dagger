using UnityEngine;
using System.Collections;

public class MegamanController : MonoBehaviour {

	public int health;
	public float maxSpeed = 4f;
	public Vector2 jumpVector;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public float jumpVerticalSpeed = 1f;
	public Transform bulletPrefab;
	public float minJumpH;
	public float maxJumpH;
	public float timeToJumpApex;
	public int iFrames;
	public int blinkFrames;
	public AudioClip bulletClip;

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
	private bool isJumping;
	private float gravity;
	private float minJumpVel;
	private float maxJumpVel;
	private SpriteRenderer spriteRenderer;
	private bool highlightSprite;
	private bool isInvulnerable;

	// Use this for initialization
	void Start () {
		megaAnimator = GetComponent<Animator> ();
		megaBody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer>();

		//Jump
		jumpVector = new Vector2 (0.0f,15.0f);
		gravity = (2*maxJumpH) / (timeToJumpApex * timeToJumpApex);
		minJumpVel = Mathf.Sqrt(2*gravity*minJumpH);
		maxJumpVel = gravity * timeToJumpApex;
		megaBody.gravityScale = gravity / Physics2D.gravity.magnitude;

		isInvulnerable = false;
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

			if (Input.GetButtonDown("Attack")) {
				Attack();
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

	// Attack
	void Attack() {
		Transform bulletTransform = Instantiate(bulletPrefab) as Transform;
		bulletTransform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		bulletTransform.GetComponent<MegamanBullet>().SetDirection(transform.localScale.x > 0);
		SoundManager.instance.RandomizeSFX(bulletClip);
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

	public void receiveDamage() {
		if (!isInvulnerable) {
			health--;

			if (health <= 0) {
				Die();
			} else {
				TriggerIFrames(iFrames);
				TriggerBlinkFrames(blinkFrames);
			}
		}
	}

	void Die() {
		Destroy(gameObject);
	}

	private void TriggerIFrames(int frames) {
		StartCoroutine(WaitFrames(frames));
	}

	private IEnumerator WaitFrames(int frames) {
		isInvulnerable = true;
		while (frames > 0) {
			if (!GameManager.isPaused)
				frames--;
			yield return null;
		}
		isInvulnerable = false;
	}

	private void TriggerBlinkFrames(int frames) {
		StartCoroutine(WaitFramesHighlight(frames));
	}

	private IEnumerator WaitFramesHighlight(int frames) {
		while (frames > 0) {
			if (frames % 10 == 0) {
				spriteRenderer.enabled = false;
			} else if (frames % 5 == 0) {
				spriteRenderer.enabled = true;
			}
			/*
			if (highlightSprite) {
				
			} else {
				
			}*/

			if (!GameManager.isPaused)
				frames--;
			yield return null;
		}
		//highlightSprite = true;
		spriteRenderer.enabled = true;
	}
}
