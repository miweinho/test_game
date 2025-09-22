using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GoblinController : MonoBehaviour
{
    public Transform player;                 // Drag your Player here (or found by tag)
    public float chaseSpeed = 3f;

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Top-down friendly defaults
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        if (player == null)
        {
            var p = GameObject.FindGameObjectWithTag("MadCat");
            player = p.transform;
        }
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference is null in FixedUpdate.");
            return;
        }

        // Calculate direction towards the player
        Vector2 desiredDir = ((Vector2)player.position - rb.position).normalized;
        Debug.Log("Desired direction towards player: " + desiredDir);

        // Move towards the player
        Vector2 next = rb.position + desiredDir * chaseSpeed * Time.deltaTime;
        rb.MovePosition(next);
    }

        void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
            Destroy(gameObject);
        }
;    }
}
