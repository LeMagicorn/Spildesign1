using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    // Float for the directional inputs
    private float horizontal;
    private float vertical;

    // Float for player movespeed
    private float speed = 8f;
    private float climbSpeed = 4f;

    // A bool we'll be using to check which way the player is moving
    private bool isFacingRight = true;

    // Bool we will use to check if the player is touch a ladder
    public bool onLadder = false;

    private void Start()
    {
        // Links the Rigidbody in the scipt to the playercharacters Rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        // This tells us when an input action is triggered
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enable onLadder bool, if the player collides with a GameObjet with the ladder trigger
        if (collision.gameObject.CompareTag("Ladder"))
        {
            onLadder = true;

            //disables gravity so you don't fall while on the ladder
            rb.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Disable the onLadder bool if the player exists a collision with a Gameobject with the ladder trigger
        if (collision.gameObject.CompareTag("Ladder"))
        {
            onLadder = false;

            // Sets the vertical speed of the player to 0, so they don't shoot off the ladder
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            // Reenables gravity so it works like normal after exiting the ladder
            rb.gravityScale = 1f;
        }
    }

    private void FixedUpdate()
    {
        // Applies the movement speed vector to the characters Rigidbody multiplied by the speed parameter.
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (onLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
        }
    }

    private void Flip()
    {
        // Multiplies the x-scale of the character by -1, flipping the sprite.
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void Update()
    {
        // Checks which way the player is facing, and applies the flip function.
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }
}
