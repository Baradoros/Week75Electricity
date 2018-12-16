using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform spawn;

    [Header("Player Parameters")]
    public float maxSpeed;
    public float jumpHeight;
    public float resetPosition;

    [Header("Ground Detection")]
    public Transform groundDetector;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private Rigidbody2D rb2d;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        HandleMovement();
        ResetOnFall(resetPosition);
    }

    private void ResetOnFall(float distance) {
        if (transform.position.y < distance) {
            GameManager.manager.TeleportObject(this.gameObject, spawn.position);
        }
    }

    void HandleMovement() {

        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * maxSpeed, rb2d.velocity.y);    // Left right movement

        if (Input.GetAxisRaw("Jump") != 0 && CheckForGround()) {                                     // Jump
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
        }

        if (Input.GetAxisRaw("Jump") == 0 && !CheckForGround() && rb2d.velocity.y > 0) {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y / 2);
        }
    }

    bool CheckForGround() {
        if (Physics2D.OverlapCircle(groundDetector.position, groundCheckRadius, whatIsGround))
            return true;
        else
            return false;
    }
}
