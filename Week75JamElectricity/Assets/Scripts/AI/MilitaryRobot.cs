using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Can shoot, but has low jump and low speed. Well, those latter two are implemented through the Inspector.
/// </summary>
public class MilitaryRobot : JumpingRobot
{
    [Tooltip("When controlled by the AI, this will target objects on any of these layers.")]
    [SerializeField] LayerMask aiTargets;
    
    [SerializeField] protected Gun2D gun;

    [Tooltip("How many seconds between shots when the AI is controlling this.")]
    [SerializeField] protected float aiShootInterval = 1;
    protected float aiShotTimer = 1;
    
    protected override void Awake() 
    {
        base.Awake();
        ChangeGunSettings();
    }



    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        UpdateGunPosition();
    }

    protected override void HandleAIControls()
    {
        base.HandleAIControls();
        HandleAIShooting();
        
    }

    void ChangeGunSettings()
    {
        gun.fireRate = this.aiShootInterval * 0.9f;
    }

    protected virtual void HandleAIShooting()
    {
        // Shoot automatically when this is ai-controlled based on a timer.
        if (aiControlled && aiShotTimer > 0)
        {
            aiShotTimer -= Time.deltaTime;
        }
        else if (aiControlled && aiShotTimer <= 0)
        {
            gun.Shoot(direction);
            aiShotTimer = 1 / aiShootInterval;
        }
    }

    protected override void HandlePlayerControls()
    {
        base.HandlePlayerControls();

        // Update the direction so the shooting is as it should.
        if (velocity.x < 0)
            direction = Vector2.left;
        else if (velocity.x > 0)
            direction = Vector2.right;

        // Let the player try to shoot the gun all the want. The fire rate will be handled by 
        // the gun.
        if (Input.GetButton("Fire1"))
            gun.Shoot(direction);

    }

    protected virtual void UpdateGunPosition()
    {
        Vector3 offset = new Vector3(1.25f, 0, 0);

        if (direction == Vector2.right)
            gun.transform.position = transform.position + offset;
        
        else if (direction == Vector2.left)
            gun.transform.position = transform.position - offset;
    }

}
