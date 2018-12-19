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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    
}
