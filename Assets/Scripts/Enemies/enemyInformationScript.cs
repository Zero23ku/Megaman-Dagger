using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyInformationScript : MonoBehaviour {
    public Transform[] items;
    public int health;
	public float speed;
	public int difficultLevel;
	public int bonusTimeInFrames;
	public Transform respawnPoint;
	public bool isShottingEnemy;
	public float buffAttack;
    public bool isDead;
    public bool isLockdown;
    public int boxColliderPosition;
<<<<<<< HEAD
    public bool enemyTutorial;
=======
    public int lockdownFrames;
>>>>>>> refs/remotes/origin/master

    private SpriteRenderer spriteRenderer;
    private Animator enemyAnimator;

	void Start() {
        isDead = false;
        isLockdown = false;
        transform.parent = GameObject.Find("Enemies").transform;
        enemyAnimator = GetComponent<Animator>();
        buffAttack = 0;
		spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine(WaitForSpawn(60));
		StartCoroutine(WaitFramesHighlight(60));
	}

    public void Die() {
        isDead = true;
        if(!enemyTutorial)
            dropItem();

        GetComponentsInChildren<BoxCollider2D>() [boxColliderPosition].enabled = false;
        enemyAnimator.SetTrigger("tDead");
        WaveManager.timeBetweenWaves += bonusTimeInFrames;
        Destroy(gameObject, 0.3f);
    }
    
    private void dropItem() {
        Transform itemTransform;
        //if you get anything higher than 0.6 then enemy will drop something
        if(Random.Range(0.0f, 1.0f) > 0.6f) {
            int item = Random.Range(0, 8);
            itemTransform = Instantiate(items[item]) as Transform;
            itemTransform.position = transform.position;
        }

    }
    private IEnumerator WaitForSpawn(int frames) {
		List<BoxCollider2D> boxColliders = new List<BoxCollider2D>(GetComponentsInChildren<BoxCollider2D>());
		foreach (BoxCollider2D boxCollider in boxColliders) {
			if (boxCollider.tag == "enemyHitBox")
				boxCollider.enabled = false;
		}

		while (frames > 0) {
			if (!GameManager.isPaused)
				frames--;
			yield return null;
		}

		foreach (BoxCollider2D boxCollider in boxColliders) {
			if (boxCollider.tag == "enemyHitBox")
				boxCollider.enabled = true;
		}
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
}