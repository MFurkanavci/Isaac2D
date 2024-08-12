using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Pooling")]
    public ObjectPool objectPool;
    public string enemyTag = "Enemy";

    [Header("Spawning")]
    public GameObject Room;
    private Player player;
    private Vector2 roomSize;
    private Transform spawnPoint;
    public float distanceFromPlayer;
    public int totalEnemies;
    public int enemiesPerGroup;
    public float spawnRate;
    private float nextSpawnTime;

    private void Start()
    {
        //We will get the room size and spawn point
        player = GameObject.Find("Player").GetComponent<Player>();
        roomSize = Room.GetComponent<SpriteRenderer>().bounds.size;

        //We will spawn the first group of enemies
        SpawnGroup();
    }

    void SetNewSpawnPoint()
    {
        //We will get a random position within the room but away from the player
        Vector2 randomPosition = new Vector2(Random.Range(-roomSize.x / 2, roomSize.x / 2), Random.Range(-roomSize.y / 2, roomSize.y / 2));
        if (Vector2.Distance(randomPosition, player.transform.position) < distanceFromPlayer)
        {
            SetNewSpawnPoint();
        }
        else
        {
            spawnPoint.position = randomPosition;
        }
    }

    private void Update()
    {
        //If we have spawned all the enemies in the room, we will destroy the spawner
        if (totalEnemies <= 0)
        {
            Destroy(gameObject);
        }
        //If the time is greater than the next spawn time, we will spawn the next group
        if (Time.time >= nextSpawnTime)
        {
            SpawnGroup();
        }
    }

    private void SpawnGroup()
    {
        //We will get a random number of enemies to spawn in the group
        int enemiesToSpawn = Random.Range(3, 6);
        //We will get a random spawn point for each enemy in the group
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SetNewSpawnPoint();
            GameObject enemy = objectPool.GetFromPool(enemyTag, spawnPoint.position, Quaternion.identity);
            enemy.GetComponent<Enemy>().OnSpawn();
        }
        //We will decrease the total enemies by the number of enemies spawned
        totalEnemies -= enemiesToSpawn;
        //We will set the next spawn time
        nextSpawnTime = Time.time + spawnRate;
    }

}
