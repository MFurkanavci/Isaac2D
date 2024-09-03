using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public Animator anim;
    public int damage;
    public float animationSpeed;
    public float attackArea;
    public AnimationClip animClip;


    public Vector2 direction;

    void OnEnable()
    {
        StartCoroutine(SlashEnumerator());
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }
    public void InitializeSlash(int damage, float range, float speed)
    {
        this.damage = damage;
        this.attackArea = range;
        this.animationSpeed = speed;
        transform.position = -(Player.Instance.transform.position - (Vector3)direction);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = targetRotation;
    }
    IEnumerator SlashEnumerator()
    {
        yield return new WaitForSeconds(animClip.length);
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
        }
    }

}
