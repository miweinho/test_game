using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public InputAction LeftAction;
    public InputAction MoveAction;

    Rigidbody2D rigidbody2d;
    Vector2 move;

    public int maxHealth = 5;

    int currentHealth;

    public float timeInvincible = 2.0f;
    bool isInvincible = true;
    float damageCooldown;

    // Start is called before the first frame update
    void Start()
    {
        //Set the player to 0,0 
        LeftAction.Enable();
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            Debug.Log(damageCooldown);
            if (damageCooldown <= 0)
            {
                isInvincible = false;
            }
        }




    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * 3.0f * Time.deltaTime;
        rigidbody2d.MovePosition(position);
        Camera.main.transform.position = new(position.x, position.y, -10.0f);

    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0 && !isInvincible)
        {
            isInvincible = true;
            damageCooldown = timeInvincible;
        
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    public int health
    {
        get
        {
            return currentHealth;
        }
    }
}
