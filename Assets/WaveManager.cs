﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WaveManager : MonoBehaviour {
	public static WaveManager instance = null;

	private List<GameObject> SpawnAirList;
	private List<GameObject> SpawnLandList;

	// To refactor to DifficultManager
	private int totalDifficultLevel;
	private int currentDifficultLevel;
	private int currentRandomSpawnPoint;
	private bool currentlyAssigning;
	private int currentSpawnIndex;
	private int currentSpawnCount;
	private int currentEnemyIndex;
	private SpawnScript currentSpawnScript;
	private bool isOverallDifficultLevelCalculated;
	private int overallDifficulty;

	void Awake() {
		if (!instance) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		currentDifficultLevel = 0;
		totalDifficultLevel = 4;
		currentSpawnIndex = 0;
		currentSpawnCount = 0;
		overallDifficulty = 0;

		currentlyAssigning = false;
		isOverallDifficultLevelCalculated = false;
	}
	
	// Update is called once per frame
	void Update () {
		string currentSceneName = SceneManager.GetActiveScene().name;

		if (currentSceneName == "Scene 1") {
			// If there's no enemy on the screen
			if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !currentlyAssigning) {
				currentlyAssigning = true;

				//We get the current level spawn points
				GetSpawnPoints();

				// We restock every enemy in every spawn point
				foreach(GameObject spawnPoint in SpawnAirList) {
					spawnPoint.GetComponent<SpawnScript>().restockEnemyList();
				}

				// We calculate the overall difficult level, which is the total sum of
				// enemy's difficult attribute
				if (!isOverallDifficultLevelCalculated) {
					isOverallDifficultLevelCalculated = true;
					calculateOverallDifficultLevel(SpawnAirList);
				}

				// TODO: Switch to decide between Air and Land randomly
				// While the total difficult level quota isn't full and there's still enemies to assign
				while (currentDifficultLevel <= totalDifficultLevel && SpawnAirList.Count != 0) {
					currentSpawnCount = 0;

					while (SpawnAirList.Count != 0 && currentSpawnCount == 0) {
						// We choose a spawn point randomly
						currentSpawnIndex = Random.Range(0, SpawnAirList.Count);

						// We get the current size of the spawn point's enemy list
						currentSpawnScript = SpawnAirList[currentSpawnIndex].GetComponent<SpawnScript>();
						currentSpawnCount = currentSpawnScript.getEnemyListCount();

						// If there's no enemy to spawn, the current spawn point is deleted from the eligible spawn list
						if (currentSpawnCount == 0) {
							SpawnAirList.RemoveAt(currentSpawnIndex);
						}
					}

					// If there's any enemy still to be assign
					if (SpawnAirList.Count > 0) {
						// We choose a random enemy from the spawn point's poll
						currentEnemyIndex = Random.Range(0, currentSpawnCount);

						// We increase the current difficult level until we reach the total one
						currentDifficultLevel += currentSpawnScript.getEnemyDifficultLevel(currentEnemyIndex);

						// TODO: Add a little delay between spawns
						// We spawn an enemy randomly from the pool of remaining enemies that the spawn point has
						SpawnAirList[currentSpawnIndex].GetComponent<SpawnScript>().instantiateEnemy(currentEnemyIndex);
					}
				}

				// We increase the difficult level for the next wave by, for example, '2'
				if (totalDifficultLevel <= overallDifficulty - 2) {
					totalDifficultLevel += 2;
				}

				// We reset the current difficult level to zero for the next iteration
				currentDifficultLevel = 0;
				currentlyAssigning = false;
			}
		}
	}

	void GetSpawnPoints() {
		SpawnAirList = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnAir"));
		SpawnLandList = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnLand"));
	}

	void calculateOverallDifficultLevel(List<GameObject> SpawnAirList) {
		foreach(GameObject spawnPoint in SpawnAirList) {
			overallDifficulty += spawnPoint.GetComponent<SpawnScript>().getTotalEnemyDifficultLevel();
		}
	}
}