using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    float laserLifeTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(ReturnToPool());
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SetPosition()
    {

    }
    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(laserLifeTime);
        ObjectPool.Instance.ReturnToPool(gameObject, gameObject.tag);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(1);
        }
    }
}
