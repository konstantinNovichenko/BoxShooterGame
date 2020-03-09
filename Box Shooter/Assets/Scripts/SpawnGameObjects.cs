 using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour
{
    
	// public variables
	public float secondsBetweenSpawning = 0.1f;
	public float xMinRange = -25.0f;
	public float xMaxRange = 25.0f;
	public float yMinRange = 8.0f;
	public float yMaxRange = 25.0f;
	public float zMinRange = -25.0f;
	public float zMaxRange = 25.0f;
	public GameObject[] spawnObjects; // what prefabs to spawn
    
    public GameObject spawnBoss; // what boss prefabs to spawn    
    public float bossSpawnScores = 0;
    public float xBoss = 0;
    public float yBoss = 0;
    public float zBoss = 0;

    public GameObject[] spawnBonuses; // time bonus during the fight with boss
    public float secondsBetweenSpawningBonuses = 0.1f;



    private float nextSpawnTime;
    private float nextBonusSpawnTime;
    private float secondsBetweenSpawningBoss = 100000000.0f;
    private float bossTimer = 0.0f;    
    private bool isBossSpawn = false;
    

	// Use this for initialization
	void Start ()
	{        
		// determine when to spawn the next object
		nextSpawnTime = Time.time+secondsBetweenSpawning;
        nextBonusSpawnTime = Time.time + secondsBetweenSpawningBonuses;
    }
	
	// Update is called once per frame
	void Update ()
	{
		// exit if there is a game manager and the game is over
		if (GameManager.gm) {
			if (GameManager.gm.gameIsOver)
				return;
		}

		// if time to spawn a new game object
		if (Time.time  >= nextSpawnTime && GameManager.gm.score < bossSpawnScores) {
			// Spawn the game object through function below
			MakeThingToSpawn ();

			// determine the next time to spawn the object
			nextSpawnTime = Time.time+secondsBetweenSpawning;
		}

        // spawn boss after target spawner stopped
        else if (GameManager.gm.score >= bossSpawnScores)
        {
            
            bossTimer += Time.deltaTime;
            if (bossTimer > 12 && Time.time >= nextSpawnTime)
            {
                GameManager.gm.BossMusic();
                MakeBossToSpawn();
                nextSpawnTime = Time.time + secondsBetweenSpawningBoss;
                isBossSpawn = true;                            
            }
        }

        // spawn time bonuses during the boss fight
        if (Time.time >= nextBonusSpawnTime && isBossSpawn == true)
        {
            
            MakeBonusToSpawn();
            nextBonusSpawnTime = Time.time + secondsBetweenSpawningBonuses;
        }




    }

	void MakeThingToSpawn ()
	{
		Vector3 spawnPosition;

		// get a random position between the specified ranges
		spawnPosition.x = Random.Range (xMinRange, xMaxRange);
		spawnPosition.y = Random.Range (yMinRange, yMaxRange);
		spawnPosition.z = Random.Range (zMinRange, zMaxRange);

		// determine which object to spawn
		int objectToSpawn = Random.Range (0, spawnObjects.Length);

		// actually spawn the game object
		GameObject spawnedObject = Instantiate (spawnObjects [objectToSpawn], spawnPosition, transform.rotation) as GameObject;

		// make the parent the spawner so hierarchy doesn't get super messy
		spawnedObject.transform.parent = gameObject.transform;
	}

    void MakeBossToSpawn ()
    {
        Vector3 spawnPosition;

        // get a random position between the specified ranges
        spawnPosition.x = xBoss;
        spawnPosition.y = yBoss;
        spawnPosition.z = zBoss;       

        // actually spawn the game object
        GameObject spawnedObject = Instantiate(spawnBoss, spawnPosition, transform.rotation) as GameObject;

        // make the parent the spawner so hierarchy doesn't get super messy
        spawnedObject.transform.parent = gameObject.transform;
    }

    void MakeBonusToSpawn()
    {
        Vector3 spawnPosition;

        // get a random position between the specified ranges
        spawnPosition.x = Random.Range(xMinRange, xMaxRange);
        spawnPosition.y = Random.Range(yMinRange, yMaxRange);
        spawnPosition.z = Random.Range(zMinRange, zMaxRange);

        // determine which object to spawn
        int objectToSpawn = Random.Range(0, spawnBonuses.Length);

        // actually spawn the game object
        GameObject spawnedObject = Instantiate(spawnBonuses[objectToSpawn], spawnPosition, transform.rotation) as GameObject;

        // make the parent the spawner so hierarchy doesn't get super messy
        spawnedObject.transform.parent = gameObject.transform;
    }
}
