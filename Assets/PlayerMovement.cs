using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody2D rb;

    [Header("Player Stats")]
    public float moveSpeed = 5f;

    [Header("Rolling")]
    public float rollSpeed = 10f;
    public float rollDuration = 0.3f;
    public float rollCooldown = 1f;
    public float rollInvincibility = 0.3f;
    private bool isRolling = false;
    private bool canRoll = true;
    private Vector2 rollDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isRolling)
        {
            // Handle movement input
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Normalize movement vector to prevent faster diagonal movement
            if (movement.magnitude > 1)
            {
                movement.Normalize();
            }

            rb.velocity = movement * moveSpeed;

            // Handle rolling input
            if (canRoll && Input.GetKeyDown(KeyCode.Space) && movement != Vector2.zero)
            {
                StartCoroutine(Roll(movement));
            }
        }
    }

    private IEnumerator Roll(Vector2 direction)
    {
        isRolling = true;
        canRoll = false;
        rollDirection = direction;

        // Apply initial roll velocity
        rb.velocity = rollDirection * rollSpeed;

        // Enable invincibility
        Physics2D.IgnoreLayerCollision(6, 8, true);
        Physics2D.IgnoreLayerCollision(6, 7, true);

        // Gradually reduce speed during the roll
        float elapsedTime = 0f;
        while (elapsedTime < rollDuration)
        {
            rb.velocity = Vector2.Lerp(rollDirection * rollSpeed, Vector2.zero, elapsedTime / rollDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isRolling = false;
        // Ensure the velocity is zero after rolling
        rb.velocity = Vector2.zero;

        // Disable invincibility after roll duration
        Physics2D.IgnoreLayerCollision(6, 8, false);
        Physics2D.IgnoreLayerCollision(6, 7, false);

        // Wait for cooldown before allowing the next roll
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }
}
