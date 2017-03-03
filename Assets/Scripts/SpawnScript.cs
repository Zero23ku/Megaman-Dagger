using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

	public List<GameObject> EnemyList;

	private List<GameObject> currentEnemyList;
	private WaveManager waveManager;

	// Use this for initialization
	void Start () {
		currentEnemyList = new List<GameObject>(EnemyList);
		waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager> ();
	}

	public int getEnemyListCount() {
		return currentEnemyList.Count;
	}

	public int getTotalEnemyDifficultLevel() {
		int totalDifficultLevel = 0;

		foreach (GameObject enemy in EnemyList) {
			totalDifficultLevel += enemy.GetComponent<enemyInformationScript>().difficultLevel;			
		}

		return totalDifficultLevel;
	}

	public int getEnemyDifficultLevel(int index) {
		return currentEnemyList[index].GetComponent<enemyInformationScript>().difficultLevel;
	}

	public void instantiateEnemy(int index, bool isBuffed) {

		if (currentEnemyList[index].tag == "enemyMiner") {
			if (checkMiner()) {
				waveManager.MinusDifficultLevel(currentEnemyList[index].GetComponent<enemyInformationScript>().difficultLevel);
				currentEnemyList.RemoveAt(index);
				return;
			}
		}

		GameObject currentEnemy = Instantiate(currentEnemyList[index]);
		enemyInformationScript enemyInformation = currentEnemy.GetComponent<enemyInformationScript>();
		currentEnemy.transform.position = GetComponent<Transform>().position;
		enemyInformation.respawnPoint = this.transform;

		if (isBuffed) {
			SpriteRenderer spriteRenderer = currentEnemy.GetComponent<SpriteRenderer>();

			if (Random.Range(0, 2) == 0) {
				enemyInformation.health = enemyInformation.health + WaveManager.enemyHealthMultiplier;
				spriteRenderer.color = new Color(0f, 255f, 0f);
			} else {
				if (enemyInformation.isShottingEnemy) {
					enemyInformation.buffAttack += (WaveManager.enemySpeedMultiplier * 2);
				} else {
					enemyInformation.speed = enemyInformation.speed * WaveManager.enemySpeedMultiplier;
				}
				spriteRenderer.color = new Color(255f, 0f, 0f);
			}

			enemyInformation.bonusTimeInFrames *= 2;
		}

		currentEnemyList.RemoveAt(index);
	}

	public void restockEnemyList() {
		currentEnemyList = new List<GameObject>(EnemyList);
	}

	private bool checkMiner() {
		List<GameObject> miners = new List<GameObject>(GameObject.FindGameObjectsWithTag("enemyMiner"));
		foreach (GameObject miner in miners) {
			if (miner.GetComponent<enemyInformationScript> ().respawnPoint == this.transform) {
				return true;
			}
		}

		return false;
	}
}