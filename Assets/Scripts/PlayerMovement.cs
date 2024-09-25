using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private HeroSO hero;
    [Header("Player Components")]
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    [Header("Player Stats")]
    public float moveSpeed = 5f;

    [Header("Rolling")]
    public int rollCount = 0;
    public float rollSpeed = 10f;
    public float rollDuration = 0.3f;
    public float rollCooldown = 1f;
    public float rollInvincibility = 0.3f;
    private bool isRolling = false;

    public GameObject rollDisplayParent;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitializePlayerMovement(HeroSO hero)
    {

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        this.hero = hero;
        moveSpeed = hero.moveSpeed;
        rollSpeed = hero.rollSpeed;
        rollDuration = hero.rollDuration;
        rollCooldown = hero.rollCooldown;

        rollDisplayParent = GameObject.Find("RollDisplay");

        for (int i = 0; i < hero.rollCount; i++)
        {
            AddDash();
        }
    }

    public void AddDash()
    {
        Instantiate(Player.Instance.dashThing, rollDisplayParent.transform);
        rollCount++;
    }

    void Update()
    {
        if (!isRolling)
        {
            // Handle movement input
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Flip the player sprite based on movement direction
            if (!Input.GetButton("Fire1"))
            {
                if (movement.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
                else if (movement.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
            }

            //play run animation
            if (movement != Vector2.zero)
            {
                BoolAnim("isRunning", true);
            }
            else
            {
                BoolAnim("isRunning", false);
            }


            // Normalize movement vector to prevent faster diagonal movement
            if (movement.magnitude > 1)
            {
                movement.Normalize();
            }

            rb.velocity = movement * moveSpeed;

            // Handle rolling input
            if (CanRoll() && Input.GetKeyDown(KeyCode.Space) && movement != Vector2.zero)
            {
                StartCoroutine(Roll(movement));
            }
        }
    }

    public void RollDisplay()
    {
        for (int i = 0; i < rollDisplayParent.transform.childCount; i++)
        {
            rollDisplayParent.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < rollCount; i++)
        {
            rollDisplayParent.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public bool CanRoll()
    {
        if (rollCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator Roll(Vector2 direction)
    {

        if (rollCount <= 0) yield break; // Prevent rolling if no rolls are left.

        isRolling = true;
        rollCount--; // Decrease roll count after initiating a roll.

        TriggerAnim("Roll");
        
        RollDisplay();

        Vector2 rollDirection = direction;

        // Apply initial roll velocity
        rb.velocity = rollDirection * rollSpeed;

        // Enable invincibility
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(6, 10, true);

        // Gradually reduce speed during the roll
        float elapsedTime = 0f;
        while (elapsedTime < rollDuration)
        {
            rb.velocity = Vector2.Lerp(rollDirection * rollSpeed, Vector2.zero, elapsedTime / rollDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the velocity is zero after rolling
        rb.velocity = Vector2.zero;

        // Disable invincibility after roll duration
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        Physics2D.IgnoreLayerCollision(6, 10, false);

        isRolling = false;

        // Wait for cooldown before allowing the next roll
        yield return new WaitForSeconds(rollCooldown);
        
        rollCount++;

        RollDisplay();
    }


    public void BoolAnim(string boolName = "", bool value = false)
    {
        Player.Instance.anim.SetBool(boolName, value);
    }

    public void TriggerAnim(string triggerName = "")
    {
        Player.Instance.anim.SetTrigger(triggerName);
    }

    public void SetFloatAnim(string floatName = "", float value = 0)
    {
        Player.Instance.anim.SetFloat(floatName, value);
    }

    public void SetIntAnim(string intName = "", int value = 0)
    {
        Player.Instance.anim.SetInteger(intName, value);
    }
}
