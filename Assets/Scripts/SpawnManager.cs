using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Objects to spawn
    public GameObject[] obstacles;
    public GameObject jump;
    public GameObject heart;
    public GameObject invincible;
    public GameObject fireball;

    //Spawns objects in the right range
    [SerializeField]
    private float zSpawn = 40;

    [SerializeField]
    private float ySpawn = 0.6f;

    [SerializeField]
    private float xSpawnRange = 7.5f;

    //Time in seconds between each spawn
    private readonly float jumpSpawnTime = 60f;
    private readonly float invicibleSpawnTime = 200.0f;
    private readonly float heartSpawnTime = 40;
    private readonly float fireballSpawnTime = 100;
    private readonly float obstaclesSpawnTime = 1.2f;
    private readonly float startDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomObstacles", startDelay, obstaclesSpawnTime);
        InvokeRepeating("SpawnJump", startDelay, jumpSpawnTime);
        InvokeRepeating("SpawnHearts", startDelay, heartSpawnTime);
        InvokeRepeating("SpawnInvicible", startDelay, invicibleSpawnTime);
        InvokeRepeating("SpawnFireball", startDelay, fireballSpawnTime);
    }

    //Spawns obstacles in the right range
    void SpawnRandomObstacles()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        int randomIndexObstacle = Random.Range(0, obstacles.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);

        Instantiate(
            obstacles[randomIndexObstacle],
            spawnPos,
            obstacles[randomIndexObstacle].gameObject.transform.rotation
        );
    }

    //Spawns jump in the right range
    void SpawnJump()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);

        Instantiate(jump, spawnPos, jump.gameObject.transform.rotation);
    }

    //Spawns fireball powerup in the right range
    void SpawnFireball()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);
        Instantiate(fireball, spawnPos, fireball.gameObject.transform.rotation);
    }

    //Spawns invicible in the right range
    void SpawnInvicible()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);
        Instantiate(invincible, spawnPos, invincible.gameObject.transform.rotation);
    }

    //Spawns hearts in the right range
    void SpawnHearts()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zSpawn);
        Instantiate(heart, spawnPos, heart.gameObject.transform.rotation);
    }
}
