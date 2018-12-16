using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RobotDirectionReverser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        RobotScript robot = other.gameObject.GetComponent<RobotScript>();

        // Reverse the direction the robot is moving if it's controlled by an ai.
        if (robot != null && robot.aiControlled)
        {
            robot.direction = -robot.direction;
        }
    }
}
