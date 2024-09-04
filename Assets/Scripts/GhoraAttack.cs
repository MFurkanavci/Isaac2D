using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoraAttack : MonoBehaviour
{
    public GameObject slash;
    private Animator animator;
    public float slashDistance = 3f;

    public Camera mainCamera;

    void Start()
    {
        animator = slash.GetComponent<Animator>();
        slash.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Attack();
        }
    }

    void Attack()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        worldMousePosition.z = 0f;
   
        Vector3 direction = (worldMousePosition - transform.position).normalized;


        slash.transform.position = transform.position + direction * slashDistance;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        slash.transform.rotation = Quaternion.Euler(0f, 0f, angle);

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

