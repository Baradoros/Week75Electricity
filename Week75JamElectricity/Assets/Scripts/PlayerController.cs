using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    [Header("Robo-swapping")]
    [SerializeField] float swapRadius = 3;
    bool controllingOtherRobot = false;
    bool controlledByPlayer = true;

    private Rigidbody2D rb2d;
    private bool canJump = true;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {

        if (!controlledByPlayer)
            return;

        HandleMovement();
        ResetOnFall(resetPosition);
        ShowSwapRadius();
        HandleRoboSwapping();
    }

    private void ResetOnFall(float distance) 
    {
        if (transform.position.y < distance) {
            GameManager.manager.TeleportObject(this.gameObject, spawn.position);
        }
    }

    void HandleMovement() 
    {

        // Movement on the x axis.
        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * maxSpeed, rb2d.velocity.y);    

        // Only allow jumping when this object is in a position to jump.
        bool onGround = CheckForGround();
        bool pressedJumpButton = Input.GetButton("Jump");

        if (pressedJumpButton && onGround && canJump) {                                     
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
            canJump = false;
        }

        // When the player is falling and not pressing the jump button, lower the speed they fall.
        if (!pressedJumpButton && !onGround)
            rb2d.gravityScale = 0.5f;
        
        else 
            rb2d.gravityScale = 1;

        /* 
        if (Input.GetAxisRaw("Jump") == 0) {
            if (rb2d.velocity.y > 0)
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y / 2);

            canJump = true;
        }
        */
    }

    bool CheckForGround() {
        if (Physics2D.OverlapCircle(groundDetector.position, groundCheckRadius, whatIsGround)) {
            return true;
        }
        else {
            return false;
        }
    }

    protected virtual void HandleRoboSwapping()
    {
        if (Input.GetButtonDown("RoboSwap") && !controllingOtherRobot) // RoboSwap is set to R at the time of this writing.
        {
            Vector3 thisPos = transform.position;

            //  Find the nearest enemy within a given radius.
            var enemies = 
                                    (from enemy in GameObject.FindObjectsOfType<RobotScript>()
                                    where Vector2.Distance(thisPos, enemy.transform.position) <= swapRadius
                                    select enemy).OrderBy(enem => Vector2.Distance(thisPos, enem.transform.position));

            if (enemies.Count() == 0)
                return;

            RobotScript nearestEnemy = enemies.First();
            
            // If there is one, take control of it and release control of the main player robot.
            nearestEnemy.aiControlled =     false;
            this.controlledByPlayer =       false;

            
        }
    }

    void ShowSwapRadius()
    {
        // Draw lines to roughly show the swap radius.

        Vector3 thisPos = transform.position;

        Vector3 northEnd = thisPos + (Vector3.up * swapRadius);
        Vector3 eastEnd = thisPos + (Vector3.right * swapRadius);
        Vector3 southEnd = thisPos + (Vector3.down * swapRadius);
        Vector3 westEnd = thisPos + (Vector3.left * swapRadius);

        Debug.DrawLine(thisPos, northEnd, Color.red);
        Debug.DrawLine(thisPos, eastEnd, Color.red);
        Debug.DrawLine(thisPos, southEnd, Color.red);
        Debug.DrawLine(thisPos, westEnd, Color.red);
    }
}
