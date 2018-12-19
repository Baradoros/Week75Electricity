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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        HandleAIControls();
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
}
