using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2D : MonoBehaviour
{
    [Tooltip("How many bullets are shot per second.")]
    [SerializeField] protected float fireRate = 1;
    [SerializeField] protected Bullet2D bulletPrefab;
    [SerializeField] protected Vector2 shotForce;
    protected float fireTimer = 0;


    // Update is called once per frame
    protected virtual void Update()
    {
        ShotCountdown();
    }

    /// <summary>
    /// Returns true if successful, false otherwise.
    /// </summary>
    public virtual bool Shoot(Vector2 direction)
    {
        if (fireTimer <= 0)
        {
            // Instantiate the bullet wherever this is, and have it move in the direction of the shotForce.
            Bullet2D bullet = Instantiate<Bullet2D>(bulletPrefab);

            bullet.velocity = shotForce;

            // Set the timer for the delay between this and the next shot.
            fireTimer = 1 / fireRate;

            return true;
        }

        return false;
    }

    protected virtual void ShotCountdown()
    {
        
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
    }
}
