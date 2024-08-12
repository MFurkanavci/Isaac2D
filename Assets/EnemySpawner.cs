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
    private Vector3 spawnPoint;
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
        spawnPoint = new Vector3(0, 0, 0);
        //We will spawn the first group of enemies
        SpawnGroup();
    }

    void SetNewSpawnPoint()
    {
        //We will set the new spawn point
        spawnPoint = new Vector3(Random.Range(-roomSize.x / 2, roomSize.x / 2), Random.Range(-roomSize.y / 2, roomSize.y / 2), 0);
        //If the spawn point is too close to the player, we will set a new one
        if (Vector2.Distance(spawnPoint, player.transform.position) < distanceFromPlayer)
        {
            SetNewSpawnPoint();
        }
        else
        {
            //We will set the new spawn point
            spawnPoint = new Vector3(spawnPoint.x + Room.transform.position.x, spawnPoint.y + Room.transform.position.y, 0);
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
        //We will set the next spawn time
        nextSpawnTime = Time.time + spawnRate;
        //We will set the new spawn point
        SetNewSpawnPoint();
        //We will spawn the enemies
        for (int i = 0; i < enemiesPerGroup; i++)
        {
            GameObject enemy = objectPool.GetFromPool(enemyTag, spawnPoint, Quaternion.identity);
            enemy.GetComponent<Enemy>().OnSpawn();
            totalEnemies--;
        }
    }

}
