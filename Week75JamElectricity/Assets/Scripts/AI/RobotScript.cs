﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for the most basic robot behaviour.
/// </summary>
public class RobotScript : MonoBehaviour {
    public RaycastHit2D Ray;
    public bool startMovingLeft = true;
    public GameObject RayTarget;
    public Rigidbody2D rb2d { get; protected set; }
    [SerializeField] protected float _moveSpeed = 3;
    [SerializeField] protected bool _aiControlled = true;
    public Vector2 direction { get; set; }
    public Vector2 velocity
    {
        get { return rb2d.velocity; }
        set { rb2d.velocity = value; }
    }

    public bool aiControlled { get { return _aiControlled; } set { _aiControlled = value; } }


    protected virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if (aiControlled)
        {
            if (startMovingLeft)
                direction = Vector2.left;
            else 
                direction = Vector2.right;

            velocity = direction * _moveSpeed;
        }

        if (!aiControlled)
            direction = Vector2.right;
        
    }

	// Update is called once per frame
	protected virtual void Update () {
        /* 
        // Log the raycast target when it it found through raycasting
        Ray = Physics2D.Linecast(transform.position, RayTarget.transform.position);
        Debug.DrawLine(transform.position, RayTarget.transform.position );
        if (Ray)
        {
            Debug.Log(Ray.transform.gameObject);
        }
            if (Ray == false)
        {
           transform.Rotate(0, 180, 0);
        }
        */
        if (aiControlled)
            HandleAIControls();

        if (!aiControlled)
            HandlePlayerControls();
	}

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LightningBall" && other.gameObject != GetComponent<EnergyTransfer>().EBall) 
        {
            Debug.Log("HitLight");
            GetComponent<RobotScript>().enabled = false;
            GetComponent<PlayerController>().enabled = true;
            GetComponent<EnergyTransfer>().enabled = true;
            
            Destroy(other.gameObject);
        }
    }

    protected virtual void HandleAIControls()
    {
        // Keep moving this robot in one direction.
        //transform.position += Direction * transform.right * Time.deltaTime;

        velocity =          direction * _moveSpeed;
    }


    protected virtual void HandlePlayerControls()
    {
        HandlePlayerMovement();
    }

    protected virtual void HandlePlayerMovement()
    {
        // Just let the player move horizontally.
        float hAxis =                   Input.GetAxisRaw("Horizontal");
        //float yAxis = Input.GetAxisRaw("Vertical");

        Vector2 newVel =                velocity;
        newVel.x =                      hAxis * _moveSpeed;

        velocity =                      newVel;
    }

    
}
