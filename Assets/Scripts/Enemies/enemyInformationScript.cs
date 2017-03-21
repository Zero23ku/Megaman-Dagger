using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyInformationScript : MonoBehaviour {
	public int health;
	public float speed;
	public int difficultLevel;
	public int bonusTimeInFrames;
	public Transform respawnPoint;
	public bool isShottingEnemy;
	public float buffAttack;

	private SpriteRenderer spriteRenderer;

	void Start() {
        transform.parent = GameObject.Find("Enemies").transform;
        buffAttack = 0;
		spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine(WaitForSpawn(60));
		StartCoroutine(WaitFramesHighlight(60));
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