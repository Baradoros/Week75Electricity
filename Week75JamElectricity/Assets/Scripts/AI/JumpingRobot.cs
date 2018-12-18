﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A robot that jumps each time it hits the ground.
/// </summary>
public class JumpingRobot : RobotScript
{
    [SerializeField] Vector2 jumpForce;
    bool onGround = true;

    protected override void Awake()
    {
        base.Awake();

    }
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        bool touchedGround = other.gameObject.CompareTag("Ground");

        if (touchedGround)
        {
            onGround = true;
            Jump();
        }
    }

    protected override void Update()
    {
        HandleMovement();
    }
    protected virtual void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            onGround = false;
    }

    protected virtual void Jump()
    {
        rb2d.AddForce(jumpForce);
    }

    protected override void HandleMovement()
    {
        if (onGround)
            Jump();

        Vector2 newVel = velocity;
        newVel.x = direction.x * _moveSpeed;
        velocity = newVel;
    }
}
