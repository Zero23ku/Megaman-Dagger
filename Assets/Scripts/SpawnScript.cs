using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

	public List<GameObject> EnemyList;

	private List<GameObject> currentEnemyList;

	// Use this for initialization
	void Start () {
		currentEnemyList = new List<GameObject>(EnemyList);
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
		GameObject currentEnemy = Instantiate(currentEnemyList[index]);
		currentEnemy.transform.position = GetComponent<Transform>().position;

		if (isBuffed) {
			enemyInformationScript enemyInformation = currentEnemy.GetComponent<enemyInformationScript>();
			SpriteRenderer spriteRenderer = currentEnemy.GetComponent<SpriteRenderer>();

			if (Random.Range(0, 2) == 0) {
				enemyInformation.health = enemyInformation.health + WaveManager.enemyHealthMultiplier;
				spriteRenderer.color = new Color(0f, 255f, 0f);
			} else {
				//print(WaveManager.enemySpeedMultiplier);
				enemyInformation.speed = enemyInformation.speed * WaveManager.enemySpeedMultiplier;
				//print(WaveManager.enemySpeedMultiplier);
				spriteRenderer.color = new Color(255f, 0f, 0f);
			}
		}

		currentEnemyList.RemoveAt(index);
	}

	public void restockEnemyList() {
		currentEnemyList = new List<GameObject>(EnemyList);
	}
}
