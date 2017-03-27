using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WaveManager : MonoBehaviour {
    public static WaveManager instance = null;
    public static int enemyHealthMultiplier;
    public static float enemySpeedMultiplier;
    public static int waveCount;
    public static int setCount;
    public static int timeBetweenWaves;
    public static int currentSet;
    public static bool firstWaveSpawned;

    public static bool isTutorialActivated = true;
    public static bool firstTutorial;
    public static bool secondTutorial;
    public static int enemiesCount = 3;

    public GameObject[] SpawnPoints;
    public GameObject[] Platforms;
    public GameObject[] TutorialLevels;

    public bool DEBUGMODE;


    private List<GameObject> SpawnAirList;
    private List<GameObject> SpawnLandList;
    private List<GameObject> currentSpawnLandList;

    private GameObject[] enemiesTutorial;

    private bool firstTutorialspawned;
    private bool secondTutorialspawned;

    private GameObject player;
    private SpawnScript currentSpawnScript;
    private int originalTimeBetweenWaves;
    private int totalDifficultLevel;
    private int currentDifficultLevel;
    private int currentRandomSpawnPoint;
    private bool currentlyAssigning;
    private int currentSpawnIndex;
    private int currentSpawnCount;
    private int currentEnemyIndex;
    private bool isOverallDifficultLevelCalculated;
    private int overallDifficulty;
    private bool isWaveSpawnable;
    private int totalBuffedEnemiesCount;
    private int currentBuffedEnemiesCount;
    private bool isBuffed;
    private int originalSpawnRate;
    private int spawnRate;
    private bool setChanged;

    private List<int> sets;

    void Awake() {
        if(!instance) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start() {
        currentSet = 0;
        sets = new List<int>(new int[] {0, 1, 2, 3, 4});
        currentDifficultLevel = 0;
        totalDifficultLevel = 4;
        currentSpawnIndex = 0;
        currentSpawnCount = 0;
        overallDifficulty = 0;
        enemyHealthMultiplier = 2;
        enemySpeedMultiplier = 1.2f;
        waveCount = 0;
        totalBuffedEnemiesCount = 1;
        currentBuffedEnemiesCount = 0;

        originalSpawnRate = 20;
        spawnRate = originalSpawnRate;

        timeBetweenWaves = 300;
        originalTimeBetweenWaves = timeBetweenWaves;

        isBuffed = false;
        currentlyAssigning = false;
        isOverallDifficultLevelCalculated = false;
        isWaveSpawnable = true;

        setChanged = false;

        if(DEBUGMODE) {
            spawnNewSet(0);
        }
        setCount = 0;
        firstWaveSpawned = true;
        if (isTutorialActivated) {
            firstTutorial = true;
            secondTutorial = true;
            firstTutorialspawned = false;
            secondTutorialspawned = false;
        } else {
            spawnNewSet(0);
        }
    }

    // Update is called once per frame
    void Update() {
        string currentSceneName = SceneManager.GetActiveScene().name;
        player = GameObject.FindWithTag("Player");
        if(currentSceneName == "Scene 1" && player) {
            //print(firstTutorial);
            if (isTutorialActivated) {
               // print("tutorial 1: " + firstTutorial + " tutorial 2: " + secondTutorial);
                if (firstTutorial) {
                    if (!firstTutorialspawned) {
                        SpawnNewTutorialSet(0);
                        firstTutorialspawned = true;
                    }
                } else if(secondTutorial) {
                    if (!secondTutorialspawned) {
                        //print("pase");
                        StartCoroutine(ChangeTutorialSet());
                        //enemiesTutorial = GameObject.FindGameObjectsWithTag("Enemy");
                        secondTutorialspawned = true;
                    }
                    if (enemiesCount <= 0) {
                        secondTutorial = false;
                    }
                }
                if(!firstTutorial && !secondTutorial) {
                    isTutorialActivated = false;
                }
            } else {
                if (!firstWaveSpawned) {
                    spawnNewSet(0);
                    firstWaveSpawned = true;
                }
                // If there's no enemy on the screen
                if (isWaveSpawnable && !currentlyAssigning) {
                    currentlyAssigning = true;
                    isWaveSpawnable = false;
                    StartCoroutine(AssignNewWave());
                }
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

    private IEnumerator AssignNewWave() {
        waveCount++;
        setCount++;

        if(setCount > 2 && (GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("enemyMiner").Length == 0)) {
            setCount = 0;
            StartCoroutine(ChangeSet());
            yield return new WaitUntil(() => setChanged);
            setChanged = false;
        }
        timeBetweenWaves = originalTimeBetweenWaves;

        currentBuffedEnemiesCount = 0;
        spawnRate = originalSpawnRate;

        if((waveCount % 3) == 0) {
            totalBuffedEnemiesCount += 2;
            enemyHealthMultiplier += 1;
            enemySpeedMultiplier += 0.4f;
        }

        //We get the current level spawn points
        GetSpawnPoints();

        // We restock every enemy in every spawn point
        foreach(GameObject spawnPoint in SpawnAirList) {
            spawnPoint.GetComponent<SpawnScript>().restockEnemyList();
        }

        foreach(GameObject spawnPoint in SpawnLandList) {
            spawnPoint.GetComponent<SpawnScript>().restockEnemyList();
        }

        // We calculate the overall difficult level, which is the total sum of
        // enemy's difficult attribute
        if(!isOverallDifficultLevelCalculated) {
            isOverallDifficultLevelCalculated = true;
            calculateOverallDifficultLevel(SpawnAirList);
        }

        // While the total difficult level quota isn't full and there's still enemies to assign
        while(currentDifficultLevel <= totalDifficultLevel && SpawnAirList.Count != 0) {
            currentSpawnCount = 0;

            // We choose randomly between air and land
            if(Random.Range(0, 2) == 0) {
                currentSpawnLandList = SpawnAirList;
            } else {
                currentSpawnLandList = SpawnLandList;
            }

            while(currentSpawnLandList.Count != 0 && currentSpawnCount == 0) {
                // We choose a spawn point randomly
                currentSpawnIndex = Random.Range(0, currentSpawnLandList.Count);

                // We get the current size of the spawn point's enemy list
                currentSpawnScript = currentSpawnLandList[currentSpawnIndex].GetComponent<SpawnScript>();
                currentSpawnCount = currentSpawnScript.getEnemyListCount();

                // If there's no enemy to spawn, the current spawn point is deleted from the eligible spawn list
                if(currentSpawnCount == 0) {
                    currentSpawnLandList.RemoveAt(currentSpawnIndex);
                }
            }

            // If there's any enemy still to be assign
            if(currentSpawnLandList.Count > 0) {
                // We choose a random enemy from the spawn point's poll
                currentEnemyIndex = Random.Range(0, currentSpawnCount);

                // We increase the current difficult level until we reach the total one
                currentDifficultLevel += currentSpawnScript.getEnemyDifficultLevel(currentEnemyIndex);

                if(currentBuffedEnemiesCount < totalBuffedEnemiesCount) {
                    isBuffed = true;
                    currentBuffedEnemiesCount++;
                } else {
                    isBuffed = false;
                }

                // We spawn an enemy randomly from the pool of remaining enemies that the spawn point has
                yield return StartCoroutine(WaitForNextEnemy(20));

                if(!(currentSpawnLandList[currentSpawnIndex] == null))
                    currentSpawnLandList[currentSpawnIndex].GetComponent<SpawnScript>().instantiateEnemy(currentEnemyIndex, isBuffed);
                else
                    break;

                spawnRate += spawnRate;
            }
        }

        // We increase the difficult level for the next wave by, for example, '2'
        if(totalDifficultLevel <= overallDifficulty - 2) {
            totalDifficultLevel += 2;
        }

        // We reset the current difficult level to zero for the next iteration
        currentDifficultLevel = 0;
        currentlyAssigning = false;
        StartCoroutine(WaitForNextWave());
    }

    private IEnumerator WaitForNextEnemy(int frames) {
        while(frames > 0) {
            frames--;
            yield return null;
        }
    }

    private IEnumerator WaitForNextWave() {
        isWaveSpawnable = false;
        while(timeBetweenWaves > 0) {
            if(!GameManager.isPaused)
                timeBetweenWaves--;
            if(GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("enemyMiner").Length == 0) {
                if(GameObject.FindWithTag("Player")) {
                    ScoreManager.AssignBonus();
                }
                break;
            }
            yield return null;
        }
        isWaveSpawnable = true;
    }

    private void spawnNewSet(int index) {
        GameObject spawnPoint = Instantiate(SpawnPoints[index]);
        spawnPoint.transform.parent = GameObject.Find("Spawn points").transform;
        spawnPoint.transform.localPosition = new Vector3(spawnPoint.transform.localPosition.x, spawnPoint.transform.localPosition.y, 0f);

        GameObject platforms = Instantiate(Platforms[index]);
        platforms.transform.parent = GameObject.Find("Platforms").transform;
        platforms.transform.localPosition = new Vector3(platforms.transform.position.x, platforms.transform.position.y, 0f);
    }

    private void SpawnNewTutorialSet(int index) {
        GameObject tutorialLevel = Instantiate(TutorialLevels[index]);
        tutorialLevel.transform.parent = GameObject.Find("Platforms").transform;
        tutorialLevel.transform.localPosition = new Vector3(tutorialLevel.transform.position.x, tutorialLevel.transform.position.y, 0f);
    }

    private IEnumerator ChangeTutorialSet() {
        float transitionFrames = 70f;
        float framesToTransition = 0f;
        float tAlpha = 0f;
        SpriteRenderer BGFO = GameObject.FindGameObjectWithTag("BGFO").GetComponent<SpriteRenderer>();
        Color BGFOColor = BGFO.color;
        //print(BGFO + " " + BGFOColor);
        // Fade Out
        while (framesToTransition < transitionFrames) {
            if (!GameManager.isPaused && player) {
                framesToTransition += 1f;
                tAlpha = framesToTransition / transitionFrames;
                BGFOColor.a = Mathf.Lerp(0.0f, 1.0f, tAlpha);
                BGFO.color = BGFOColor;
                yield return null;
            } else {
                break;
            }
        }

        

        // We destroy the current set from the scene
        if (player) {
            foreach (GameObject set in GameObject.FindGameObjectsWithTag("Set")) {
                Destroy(set);
            }
        }

        SpawnNewTutorialSet(1);

        // Fade In
        framesToTransition = 0f;
        tAlpha = 1f;
        while (framesToTransition < transitionFrames) {
            if (!GameManager.isPaused && player) {
                framesToTransition += 1f;
                tAlpha = framesToTransition / transitionFrames;
                BGFOColor.a = Mathf.Lerp(1.0f, 0.0f, tAlpha);
                BGFO.color = BGFOColor;
                yield return null;
            } else {
                break;
            }
        }
    }

    private IEnumerator ChangeSet() {
        int previousSet;
        int newCurrentSet;
        float transitionFrames = 70f;
        float framesToTransition = 0f;
        float tAlpha = 0f;
        SpriteRenderer BGFO = GameObject.FindGameObjectWithTag("BGFO").GetComponent<SpriteRenderer>();
        Color BGFOColor = BGFO.color;

        // Fade Out
        while(framesToTransition < transitionFrames) {
            if(!GameManager.isPaused && player) {
                framesToTransition += 1f;
                tAlpha = framesToTransition / transitionFrames;
                BGFOColor.a = Mathf.Lerp(0.0f, 1.0f, tAlpha);
                BGFO.color = BGFOColor;
                yield return null;
            } else {
                break;
            }
        }

        // We remove the current set from the poll
        // and choose a new one randomly
        previousSet = currentSet;
        sets.Remove(currentSet);
        newCurrentSet = sets[Random.Range(0, sets.Count)];
        //Debug.Log("new set: " + newCurrentSet);

        // We destroy the current set from the scene
        if (player) {
            foreach(GameObject set in GameObject.FindGameObjectsWithTag("Set")) {
                Destroy(set);
            }
        }

        // We instantiate the new set
        currentSet = newCurrentSet;
        spawnNewSet(newCurrentSet);

        // We add the previous set to the poll
        sets.Add(previousSet);

        // Fade In
        framesToTransition = 0f;
        tAlpha = 1f;
        while(framesToTransition < transitionFrames) {
            if(!GameManager.isPaused && player) {
                framesToTransition += 1f;
                tAlpha = framesToTransition / transitionFrames;
                BGFOColor.a = Mathf.Lerp(1.0f, 0.0f, tAlpha);
                BGFO.color = BGFOColor;
                yield return null;
            } else {
                break;
            }
        }
        
        setChanged = true;
    }

    public void ResetScene() {
        enemyHealthMultiplier = 2;
        enemySpeedMultiplier = 1.2f;
        totalDifficultLevel = 4;
        waveCount = 0;
        totalBuffedEnemiesCount = 1;
        timeBetweenWaves = originalTimeBetweenWaves;
    }

    public void MinusDifficultLevel(int minusDifficult) {
        currentDifficultLevel -= minusDifficult;
    }
}