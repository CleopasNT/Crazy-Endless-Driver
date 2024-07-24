using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    //Objects to spawn
    public GameObject[] obstacles;
    public GameObject jump;
    public GameObject magnet;
    public GameObject invincible;
    public GameObject coins;
    public GameObject hearts;
    //Spawns objects in the right range
    private float zSpawn = -6;
    private float ySpawn = 0.6f;
    private float xSpawnRange = 7.5f;
    //Time in seconds between each spawn
    private float jumpSpawnTime = 60f;
    private float invicibleSpawnTime = 100.0f;
    private float magnetSpawnTime = 50.0f;
    private float coinsSpawnTime = 5.0f;
    private float heartsSpawnTime = 40.0f;
    private float obstaclesSpawnTime = 3.0f;
    private float startDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomObstacles", startDelay, obstaclesSpawnTime);
        InvokeRepeating("SpawnJump", startDelay, jumpSpawnTime);
        InvokeRepeating("SpawnCoins", startDelay, coinsSpawnTime);
        InvokeRepeating("SpawnHearts", startDelay, heartsSpawnTime);
        InvokeRepeating("SpawnInvicible", startDelay, invicibleSpawnTime);
        InvokeRepeating("SpawnMagnet", startDelay, magnetSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Spawns obstacles in the right range
    void SpawnRandomObstacles()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        int randomIndexObstacle = Random.Range(0, obstacles.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);

        Instantiate(obstacles[randomIndexObstacle], spawnPos, obstacles[randomIndexObstacle].gameObject.transform.rotation);
    }

    //Spawns jump in the right range
    void SpawnJump()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);

        Instantiate(jump, spawnPos, jump.gameObject.transform.rotation);
    }

    //Spawns invicible in the right range
    void SpawnInvicible()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);
        Instantiate(invincible, spawnPos, invincible.gameObject.transform.rotation);
    }

    //Spawns magnet in the right range
    void SpawnMagnet()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);
        Instantiate(magnet, spawnPos, magnet.gameObject.transform.rotation);
    }

    //Spawns coins in the right range
    void SpawnCoins()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);
        Instantiate(coins, spawnPos, coins.gameObject.transform.rotation);
    }

    //Spawns hearts in the right range
    void SpawnHearts()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);
        Instantiate(hearts, spawnPos, hearts.gameObject.transform.rotation);
    }

}