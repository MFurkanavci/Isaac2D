using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    public RoomManager roomManager;

    [Header("Enemy Pooling")]
    public List<string> enemyTagsEasy;
    public List<string> enemyTagsHard;
    public List<string> enemyTagsNormal;

    public List<string> enemyTagsImpossible;


    [Header("Spawning")]
    public GameObject Room;
    private Player player;
    private Vector2 roomSize;
    private Vector3 spawnPoint;
    public float distanceFromPlayer;
    public int totalEnemies;
    public int totalIncrease;
    public int currentEnemies;
    public float spawnRate;
    private float nextSpawnTime;

    [SerializeField] private List<GameObject> enemies = new List<GameObject>();

    private bool isSpawning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        currentEnemies = totalEnemies;
    }
    private void Update()
    {
        if (isSpawning)
        {
            if (Time.time >= nextSpawnTime && currentEnemies > 0)
            {
                SpawnGroup(roomManager.currentRoomType);
            }
            if (currentEnemies <= 0 && enemies.Count <= 0)
            {
                CleanUp();
                DoorManager.Instance.OpenDoors();
                DoorManager.Instance.HandleExperience();
            }
        }
    }

    public void StartSpawningEasy()
    {

        currentEnemies = totalEnemies;
        DoorManager.Instance.CloseDoors();
        player = GameObject.Find("Player").GetComponent<Player>();
        Room = RoomManager.Instance.currentRoom.gameObject;
        roomSize = Room.GetComponent<SpriteRenderer>().bounds.size;
        nextSpawnTime = Time.time + spawnRate;

        isSpawning = true;
    }
    public void StartSpawningNormal()
    {

        currentEnemies = totalEnemies;
        DoorManager.Instance.CloseDoors();
        player = GameObject.Find("Player").GetComponent<Player>();
        Room = RoomManager.Instance.currentRoom.gameObject;
        roomSize = Room.GetComponent<SpriteRenderer>().bounds.size;
        nextSpawnTime = Time.time + spawnRate;

        isSpawning = true;
    }
    public void StartSpawningHard()
    {
        List<string>[] enemyTagLists = { enemyTagsNormal, enemyTagsEasy, enemyTagsHard };
        List<string> selectedList = enemyTagLists[UnityEngine.Random.Range(0, enemyTagLists.Length)];
        string enemyTag = selectedList[UnityEngine.Random.Range(0, selectedList.Count)];
        currentEnemies = totalEnemies;
        DoorManager.Instance.CloseDoors();
        player = GameObject.Find("Player").GetComponent<Player>();
        Room = RoomManager.Instance.currentRoom.gameObject;
        roomSize = Room.GetComponent<SpriteRenderer>().bounds.size;
        nextSpawnTime = Time.time + spawnRate;

        isSpawning = true;
    }
    public void StartSpawningImpossible()
    {

        currentEnemies = totalEnemies;
        DoorManager.Instance.CloseDoors();
        player = GameObject.Find("Player").GetComponent<Player>();
        Room = RoomManager.Instance.currentRoom.gameObject;
        roomSize = Room.GetComponent<SpriteRenderer>().bounds.size;
        nextSpawnTime = Time.time + spawnRate;

        isSpawning = true;
    }

    public void CleanUp()
    {
        currentEnemies = 0;
        isSpawning = false;
    }

    private void SpawnGroup(RoomType roomType)
    {
        int enemiesToSpawn;
        int minEnemiesPerGroup;
        int maxEnemiesPerGroup;
        if (currentEnemies <= 5)
        {
            enemiesToSpawn = currentEnemies;
        }
        else
        {
            minEnemiesPerGroup = Mathf.CeilToInt(currentEnemies / 5f);
            maxEnemiesPerGroup = Mathf.CeilToInt(currentEnemies / 3f);

            enemiesToSpawn = UnityEngine.Random.Range(minEnemiesPerGroup, maxEnemiesPerGroup);
        }

        //We will set the next spawn time
        nextSpawnTime = Time.time + spawnRate;



        Vector3 _spawnPoint = SetNewSpawnPoint();
        float radius = 1f;
        //We will spawn the enemies with their own radius
        if (roomType == RoomType.CombatRoomEasy)
        {
            print("start spawn");
            for (int i = 0; i < enemiesToSpawn; i++)
            {

                string enemyTag = enemyTagsEasy[UnityEngine.Random.Range(0, enemyTagsEasy.Count)];
                Vector3 spawnPosition = _spawnPoint + UnityEngine.Random.insideUnitSphere * radius;
                GameObject _enmy = ObjectPool.Instance.GetFromPool(enemyTag, spawnPosition, quaternion.identity);
                enemies.Add(_enmy);
                currentEnemies--;
            }
        }
        else if (roomType == RoomType.CombatRoomMedium)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                List<string> selectedList;
                float randomValue = UnityEngine.Random.value * 100f;
                if (randomValue < 40f)
                {
                    selectedList = enemyTagsNormal;
                }
                else selectedList = enemyTagsEasy;

                string enemyTag = selectedList[UnityEngine.Random.Range(0, selectedList.Count)];
                Vector3 spawnPosition = _spawnPoint + UnityEngine.Random.insideUnitSphere * radius;
                GameObject _enmy = ObjectPool.Instance.GetFromPool(enemyTag, spawnPosition, quaternion.identity);
                enemies.Add(_enmy);
                currentEnemies--;
            }
        }
        else if (roomType == RoomType.CombatRoomHard)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                List<string> selectedList;
                float randomValue = UnityEngine.Random.value * 100f;
                if (randomValue < 15f)
                {
                    selectedList = enemyTagsHard;
                }
                else if (randomValue < 45f)
                {
                    selectedList = enemyTagsNormal;
                }
                else selectedList = enemyTagsEasy;

                string enemyTag = selectedList[UnityEngine.Random.Range(0, selectedList.Count)];
                Vector3 spawnPosition = _spawnPoint + UnityEngine.Random.insideUnitSphere * radius;
                GameObject _enmy = ObjectPool.Instance.GetFromPool(enemyTag, spawnPosition, quaternion.identity);
                enemies.Add(_enmy);
                currentEnemies--;
            }
        }

    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    Vector3 SetNewSpawnPoint()
    {
        //We will set the new spawn point
        spawnPoint = new Vector3(UnityEngine.Random.Range(-roomSize.x / 2, roomSize.x / 2), UnityEngine.Random.Range(-roomSize.y / 2, roomSize.y / 2), 0);
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

        return spawnPoint;
    }
}
