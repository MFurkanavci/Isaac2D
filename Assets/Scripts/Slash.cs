using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public Animator anim;
    public int damage;
    public float animationSpeed;
    public float attackArea;

    void OnEnable()
    {
        SetClipSpeed();
        StartCoroutine(SlashEnumerator());
    }

    public void SetClipSpeed()
    {
        anim.SetFloat("slashSpeed", animationSpeed);
    }
    void OnDisable()
    {
        transform.position = transform.parent.parent.position;
        StopAllCoroutines();
    }
    public void InitializeSlash(int damage, float range, float speed)
    {
        this.damage = damage;
        this.attackArea = range;
        this.animationSpeed = speed;
    }

    public void SetPositionAndDirection(Vector2 direction)
    {
        float offset = attackArea / 2;
        transform.position += (Vector3)direction * offset;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    IEnumerator SlashEnumerator()
    {
        yield return new WaitForSeconds(animationSpeed / 2);
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
