using UnityEngine;

public class Cleaner : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        ObjectPool.Instance.ReturnToPool(other.gameObject, other.gameObject.tag);

        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Clean();
        }
    }

    void OnEnable()
    {
        Invoke("DisableCleaner", 0.1f);
    }

    public void DisableCleaner()
    {
        gameObject.SetActive(false);
    }
}
