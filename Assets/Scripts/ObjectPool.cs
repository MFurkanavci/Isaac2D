using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //instance of the object pool
    public static ObjectPool Instance;

    [System.Serializable]
    public class Pool
    {
        public string tag; // Tag to identify the object type (e.g., "Bullet", "Enemy")
        public GameObject prefab; // The prefab to pool
        public int poolSize; // Initial size of the pool
        public Transform parent; // Parent transform to keep objects organized in the hierarchy
    }

    public List<Pool> pools; // List of different pools
    private Dictionary<string, Queue<GameObject>> poolDictionary;

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
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(pool.parent);
                objectPool.Enqueue(obj);
                obj.tag = pool.tag;
                obj.name = pool.tag + " " + i;
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Method to get an object from the pool
    public GameObject GetFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject obj = poolDictionary[tag].Count > 0 ? poolDictionary[tag].Dequeue() : InstantiatePrefab(tag);

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }

    // Method to return an object to the pool
    public void ReturnToPool(GameObject obj, string tag)
    {
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);

    }

    // Instantiate a new prefab if the pool is empty (optional)
    private GameObject InstantiatePrefab(string tag)
    {
        Pool pool = pools.Find(p => p.tag == tag);
        if (pool != null)
        {
            GameObject obj = Instantiate(pool.prefab);
            obj.SetActive(false);
            obj.transform.SetParent(pool.parent);
            return obj;
        }
        return null;
    }
}
