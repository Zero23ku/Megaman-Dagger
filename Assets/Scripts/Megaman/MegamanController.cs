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
	public int attackCooldown;
	public AudioClip bulletClip;
    public Transform deathParticlePrefab;

	/*
	 * GCRadius = 0.28
	 * MinJump H = 0.2
	 * MaxJump H = 2.2
	 * Time to Jump = 0.32
	 */
	private int maxHealth;
	private bool tookInvItem;
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
	private bool isKnockbacked;
	private bool onAttackCooldown;
	private float positionBulletX;
	private float positionBulletY;
    private float bulletSpeedPowerUp;

    private int maxBulletsOnScreen;
    private bool isThreeBulletsPowerUp;
    private bool isMoreBulletsPowerUp;
    private bool isMoreBulletSpeedPowerUp;
    private bool isMoreSpeedPowerUp;

    private bool tookThreeBulletsItem;
    private bool tookMoreBulletsItem;
    private bool tookMoreBulletSpeedItem;
    private bool tookMoreSpeedItem;

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
		isKnockbacked = false;
		onAttackCooldown = false;

		maxHealth = health;
		tookInvItem = false;

        maxBulletsOnScreen = 3;
        isThreeBulletsPowerUp = false;
        isMoreBulletsPowerUp = false;
        isMoreBulletSpeedPowerUp = false;
        isMoreSpeedPowerUp = false;

        tookMoreBulletsItem = false;
        tookThreeBulletsItem = false;
        tookMoreBulletSpeedItem = false;
        tookMoreSpeedItem = false;

        bulletSpeedPowerUp = 0.0f;
	}

    // Update is called once per frame
    void Update() {
		if (!GameManager.isPaused && !isKnockbacked) {
			Movement();

			if (grounded) {
				isJumping = false;
				megaAnimator.SetBool("tShootingWhileJumping", false);
				jumpVerticalSpeed = 1f;
				megaAnimator.SetBool("grounded", true);
			} else {
				megaAnimator.SetBool("grounded", false);
				jumpVerticalSpeed = 0.75f;
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
		if (!onAttackCooldown && GameObject.FindGameObjectsWithTag("MegamanBullet").Length < maxBulletsOnScreen) {
			TriggerAttackCooldown(attackCooldown);

			if (megaAnimator.GetBool("grounded")) {
				megaAnimator.SetTrigger("tShooting");
			} else {
				megaAnimator.SetBool("tShootingWhileJumping", true);
			}

			positionBulletY += 0.03f;

            //if there is SpreadBullet power up
            if (isThreeBulletsPowerUp) {
                SoundManager.instance.RandomizeSFX(bulletClip);
                SoundManager.instance.RandomizeSFX(bulletClip);
                SoundManager.instance.RandomizeSFX(bulletClip);
                Transform bulletTransform1 = Instantiate(bulletPrefab) as Transform;
                Transform bulletTransform2 = Instantiate(bulletPrefab) as Transform;
                Transform bulletTransform3 = Instantiate(bulletPrefab) as Transform;
                positionBulletX = transform.position.x;
                positionBulletY = transform.position.y;

                if (lookingRight) {
                    positionBulletX += 0.5f;
                }
                else {
                    positionBulletX -= 0.5f;
                }
                //Straighfoward bullet
                bulletTransform1.position = new Vector3(positionBulletX, positionBulletY, transform.position.z);
                bulletTransform1.GetComponent<MegamanBullet>().SetDirection(transform.localScale.x > 0);
                bulletTransform1.GetComponent<MegamanBullet>().direction = 's';
                bulletTransform1.GetComponent<MegamanBullet>().fasterBullets(bulletSpeedPowerUp);

                //Diagonal-up bullet
                bulletTransform2.position = new Vector3(positionBulletX, positionBulletY, transform.position.z);
                bulletTransform2.GetComponent<MegamanBullet>().SetDirection(transform.localScale.x > 0);
                bulletTransform2.GetComponent<MegamanBullet>().direction = 'u';
                bulletTransform2.GetComponent<MegamanBullet>().fasterBullets(bulletSpeedPowerUp);


                //Diagonal-down bullet
                bulletTransform3.position = new Vector3(positionBulletX, positionBulletY, transform.position.z);
                bulletTransform3.GetComponent<MegamanBullet>().SetDirection(transform.localScale.x > 0);
                bulletTransform3.GetComponent<MegamanBullet>().direction = 'd';
                bulletTransform3.GetComponent<MegamanBullet>().fasterBullets(bulletSpeedPowerUp);

            }

            // Nornal shooting
            else {
                SoundManager.instance.RandomizeSFX(bulletClip);
                Transform bulletTransform = Instantiate(bulletPrefab) as Transform;
                positionBulletX = transform.position.x;
                positionBulletY = transform.position.y;

                if (lookingRight) {
                    positionBulletX += 0.5f;
                }
                else {
                    positionBulletX -= 0.5f;
                }

                bulletTransform.position = new Vector3(positionBulletX, positionBulletY, transform.position.z);
                bulletTransform.GetComponent<MegamanBullet>().SetDirection(transform.localScale.x > 0);
                bulletTransform.GetComponent<MegamanBullet>().direction = 's';
                bulletTransform.GetComponent<MegamanBullet>().fasterBullets(bulletSpeedPowerUp);
            }
		}
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
			}
			else {
				TriggerIFrames(iFrames);
				TriggerFlashingFrames(iFrames / 2);

				if (lookingRight)
					megaBody.velocity = new Vector2(-3f, megaBody.velocity.y);
				else
					megaBody.velocity = new Vector2(3f, megaBody.velocity.y);
			}
		}
		else if (isInvulnerable && tookInvItem) {
			StartCoroutine(WaitFramesHighlight(100));
		}


	}
    void DeathAnimation() {
        float posInterval = 0.3f;
        //Going up particles
        Transform deathParticleUp1 = Instantiate(deathParticlePrefab) as Transform;
        Transform deathParticleUp2 = Instantiate(deathParticlePrefab) as Transform;
        Transform deathParticleUp3 = Instantiate(deathParticlePrefab) as Transform;

        deathParticleUp1.GetComponent<MegamanDeathController>().direction = "up";
        deathParticleUp2.GetComponent<MegamanDeathController>().direction = "up";
        deathParticleUp3.GetComponent<MegamanDeathController>().direction = "up";

        deathParticleUp1.position = new Vector2(transform.position.x, transform.position.y + posInterval);
        deathParticleUp2.position = new Vector2(transform.position.x, transform.position.y + 2 * posInterval);
        deathParticleUp3.position = new Vector2(transform.position.x, transform.position.y + 3 * posInterval);

        //Going Down particles
        Transform deathParticleDown1 = Instantiate(deathParticlePrefab) as Transform;
        Transform deathParticleDown2 = Instantiate(deathParticlePrefab) as Transform;
        Transform deathParticleDown3 = Instantiate(deathParticlePrefab) as Transform;

        deathParticleDown1.GetComponent<MegamanDeathController>().direction = "down";
        deathParticleDown2.GetComponent<MegamanDeathController>().direction = "down";
        deathParticleDown3.GetComponent<MegamanDeathController>().direction = "down";

        deathParticleDown1.position = new Vector2(transform.position.x, transform.position.y - posInterval);
        deathParticleDown2.position = new Vector2(transform.position.x, transform.position.y - 2 * posInterval);
        deathParticleDown3.position = new Vector2(transform.position.x, transform.position.y - 3 * posInterval);

        //Going left particles
        Transform deathParticleLeft1 = Instantiate(deathParticlePrefab) as Transform;
        Transform deathParticleLeft2 = Instantiate(deathParticlePrefab) as Transform;
        Transform deathParticleLeft3 = Instantiate(deathParticlePrefab) as Transform;

        deathParticleLeft1.GetComponent<MegamanDeathController>().direction = "left";
        deathParticleLeft2.GetComponent<MegamanDeathController>().direction = "left";
        deathParticleLeft3.GetComponent<MegamanDeathController>().direction = "left";

        deathParticleLeft1.position = new Vector2(transform.position.x - posInterval, transform.position.y);
        deathParticleLeft2.position = new Vector2(transform.position.x - 2 * posInterval, transform.position.y);
        deathParticleLeft3.position = new Vector2(transform.position.x - 3 * posInterval, transform.position.y);

        //Going right particles
        Transform deathParticleRight1 = Instantiate(deathParticlePrefab) as Transform;
        Transform deathParticleRight2 = Instantiate(deathParticlePrefab) as Transform;
        Transform deathParticleRight3 = Instantiate(deathParticlePrefab) as Transform;

        deathParticleRight1.GetComponent<MegamanDeathController>().direction = "right";
        deathParticleRight2.GetComponent<MegamanDeathController>().direction = "right";
        deathParticleRight3.GetComponent<MegamanDeathController>().direction = "right";

        deathParticleRight1.position = new Vector2(transform.position.x + posInterval, transform.position.y);
        deathParticleRight2.position = new Vector2(transform.position.x + 2 * posInterval, transform.position.y);
        deathParticleRight3.position = new Vector2(transform.position.x + 3 * posInterval, transform.position.y);
    }
	void Die() {
        DeathAnimation();
        Destroy(gameObject);
		GameManager.instance.gameOver();
	}

	private void TriggerAttackCooldown(int frames) {
		StartCoroutine(WaitAttackCooldown(frames));
	}

	private IEnumerator WaitAttackCooldown(int frames) {
		onAttackCooldown = true;
		while (frames > 0) {
			if (!GameManager.isPaused)
				frames--;
			yield return null;
		}
		onAttackCooldown = false;
	}

	private void TriggerFlashingFrames(int frames) {
		StartCoroutine(WaitFramesFlashing(frames));
	}

	private IEnumerator WaitFramesFlashing(int frames) {
		isKnockbacked = true;
		megaAnimator.SetBool("damageReceived", true);
		while (frames > 0) {
			if (!GameManager.isPaused)
				frames--;
			yield return null;
		}
		isKnockbacked = false;
		megaAnimator.SetBool("damageReceived",false);

		TriggerBlinkFrames(blinkFrames - frames);
	}


	private void TriggerIFrames(int frames) {
		StartCoroutine(WaitFramesInvulnerable(frames));
	}

	private IEnumerator WaitFramesInvulnerable(int frames) {
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

			if (!GameManager.isPaused)
				frames--;
			yield return null;
		}
		spriteRenderer.enabled = true;
	}

	public void getHealth(int newHealth) {
		if (health + newHealth <= maxHealth) {
			health += newHealth;
		}
		else {
			health = maxHealth;
		}
	}

    public void becomeInvulnerable() {
        if (!isInvulnerable) {
            isInvulnerable = true;
            StartCoroutine(getInvulnerability());
        } else {
            tookInvItem = true;
        }
    }

	public IEnumerator getInvulnerability() {
		isInvulnerable = true;
		spriteRenderer.color = new Color(255,255,0);
		yield return StartCoroutine(waitForFrames(240));
	}

	public IEnumerator waitForFrames(int frames) {
		while (frames > 0) {
			if (!GameManager.isPaused)
				frames--;
            if (frames < 100 && !GameManager.isPaused) {
				if (frames % 10 == 0)
					spriteRenderer.color = new Color(255, 255, 0);
				else if (frames % 5 == 0)
					spriteRenderer.color = new Color(255, 255, 255);
			}
            if (tookInvItem) {
                tookInvItem = false;
                frames = 240;
            }
            yield return null;
		}
		tookInvItem = false;
		isInvulnerable = false;
		spriteRenderer.color = new Color(255,255,255);
	}

    public void threeBullets() {
        if (!isThreeBulletsPowerUp) {
            isThreeBulletsPowerUp = true;
            maxBulletsOnScreen = maxBulletsOnScreen * 3;
            StartCoroutine(threeBulletsPowerUp(400));
        }
        else {
            tookThreeBulletsItem = true;
        }
    }

    public IEnumerator threeBulletsPowerUp(int frames) {

        while (frames > 0) {

            if (!GameManager.isPaused) {
                frames--;
            }

            if (tookThreeBulletsItem) {
                frames = 400;
                tookThreeBulletsItem = false;
            }
            yield return null;
        }
        tookThreeBulletsItem = false;
        isThreeBulletsPowerUp = false;
        maxBulletsOnScreen = maxBulletsOnScreen / 3;

    }

    public void moreBullets() {
        if (!isMoreBulletsPowerUp) {
            isMoreBulletsPowerUp = true;
            maxBulletsOnScreen = maxBulletsOnScreen * 2;
            StartCoroutine(moreBulletsPowerUp(400));
        }
        else {
            tookMoreBulletsItem = true;
        }
    }

    public IEnumerator moreBulletsPowerUp(int frames) {

        while (frames > 0) {
            if (!GameManager.isPaused) {
                frames--;
            }

            if (tookMoreBulletsItem) {
                frames = 400;
                tookMoreBulletsItem = false;
            }
            yield return null;
        }

        tookMoreBulletsItem = false;
        isMoreBulletsPowerUp = false;
        maxBulletsOnScreen = maxBulletsOnScreen / 2;
    }

    public void moreBulletSpeed() {
        if (!isMoreBulletSpeedPowerUp) {
            isMoreBulletSpeedPowerUp = true;
            bulletSpeedPowerUp = 4.5f;
            StartCoroutine(moreBulletSpeedPowerUp(400));
        } else {
            tookMoreBulletSpeedItem = true;
        }
    }

    public IEnumerator moreBulletSpeedPowerUp(int frames) {

        while(frames > 0) {
            if (!GameManager.isPaused) {
                frames--;
            }

            if (tookMoreBulletSpeedItem) {
                frames = 400;
                tookMoreBulletSpeedItem = false;
            }
            yield return null;
        }

        isMoreBulletSpeedPowerUp = false;
        tookMoreBulletSpeedItem = false;
        bulletSpeedPowerUp = 0.0f;
    }

    public void moreSpeed() {
        if (!isMoreSpeedPowerUp) {
            isMoreSpeedPowerUp = true;
            maxSpeed += 2f;
            StartCoroutine(moreSpeedPowerUp(400));

        } else {
            tookMoreSpeedItem = true;
        }
    }

    public IEnumerator moreSpeedPowerUp(int frames) {

        while(frames > 0) {
            if (!GameManager.isPaused) {
                frames--;
            }

            if (tookMoreSpeedItem) {
                frames = 400;
            }
            yield return null;
        }
        maxSpeed -= 2f;
        isMoreSpeedPowerUp = false;
        tookMoreSpeedItem = false;

    }
}
