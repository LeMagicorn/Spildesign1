using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    // Float for the horizontal input
    private float horizontal;

    // Float for player movespeed
    private float speed = 8f;

    // A bool we'll be using to check which way the player is moving
    private bool isFacingRight = true;

    private void Start()
    {
        // Links the Rigidbody in the scipt to the playercharacters Rigidbody
        rb = GetComponent<Rigidbody2D>();
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

    private void FixedUpdate()
    {
        // Applies the movement speed vector to the characters Rigidbody multiplied by the speed parameter.
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void Flip()
    {
        // Multiplies the x-scale of the character by -1, flipping the sprite.
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        // This tells us when an action is triggered, and returns a vector2, from which we want the x value
        horizontal = context.ReadValue<Vector2>().x;
    }
}
