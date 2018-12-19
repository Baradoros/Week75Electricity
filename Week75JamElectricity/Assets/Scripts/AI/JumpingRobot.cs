using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A robot that jumps each time it hits the ground.
/// </summary>
public class JumpingRobot : RobotScript
{
    [SerializeField] Vector2 jumpForce;
    bool onGround = true;

    // Collisions
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        bool touchedGround = other.gameObject.CompareTag("Ground");

        if (touchedGround)
        {
            onGround = true;
            Jump();
        }
    }

    
    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            onGround = false;
    }

    // Jumping and controls
    protected virtual void Jump()
    {
        rb2d.AddForce(jumpForce);
    }

    protected override void HandleAIControls()
    {
        if (onGround)
            Jump();

        // Keep the robot moving in the x direction when possible.
        Vector2 newVel = velocity;
        newVel.x = direction.x * _moveSpeed;
        velocity = newVel;
    }

    protected override void HandlePlayerControls()
    {
        base.HandlePlayerControls();

        // Let the player jump when on the ground
        if (onGround && Input.GetButton("Jump"))
            Jump();
    }
}
