using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public GameObject slash;
    private Animator animator;
    public float slashDistance = 3f;
    public float slashSpeed = 10f;
    public float nextSlash = 0f;

    public Camera mainCamera;

    public Vector3 direction;

    void Start()
    {
        animator = slash.GetComponent<Animator>();
        slash.SetActive(false);
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (slashSpeed > 0)
        {
            nextSlash -= Time.deltaTime;
        }
        else
        {
            nextSlash = 0;
        }

        if (Input.GetButton("Fire1") && nextSlash <= 0)
        {
            Attack(direction);
            nextSlash = slashSpeed;
        }

        
        direction = GetCameraDirection();
    }

    Vector3 GetCameraDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.position);
        Vector3 direction = (mousePos - screenPoint).normalized;
        return direction;
    }

    void Attack(Vector3 direction)
    {
        Vector3 position = transform.position + direction * slashDistance;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        slash.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        slash.transform.position = position;
    }

    void OnEnable()
    {
        slashDistance = gameObject.GetComponentInParent<Player>().hero.attackRange;
        slashSpeed = gameObject.GetComponentInParent<Player>().hero.attackSpeed;
        nextSlash = slashSpeed;
    }

    void OnDisable()
    {
        nextSlash = 0;
    }


    public void ActivateSlash()
    {
        slash.SetActive(true);
    }

    public void DeactivateSlash()
    {
        slash.SetActive(false);
    }

}
