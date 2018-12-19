using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2D : MonoBehaviour
{
    [SerializeField] float damage;
    [Tooltip("How many objects this has to collide with before it is automatically erased.")]
    [SerializeField] int collisionsBeforeDeath = 1;
    [Tooltip("This bullet can damage things on these layers.")]
    [SerializeField] LayerMask damageOnLayers;
    [Tooltip("This bullet can damage things with any of these tags.")]
    [SerializeField] List<string> damageWithTags;

    public Rigidbody2D rb2d { get; protected set; }
    public Vector2 velocity 
    {
        get { return rb2d.velocity; }
        set { rb2d.velocity = value; }
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

   
   protected virtual void OnTriggerEnter2D(Collider2D other)
   {
       HandleDamageApplication(other.gameObject);
   }

   protected virtual void OnCollisionEnter2D(Collision2D other)
   {
       HandleDamageApplication(other.gameObject);
   }

   void HandleDamageApplication(GameObject other)
   {
       // If the other thing is damageable and within the right parameters, damage it.
       IDamageable<float> damageable = other.GetComponent<IDamageable<float>>();

       if (damageable == null)
            return;

        bool hasRightTag = damageWithTags.Contains(other.tag);
        bool inRightLayer = damageOnLayers.Contains(other.layer);

        if (hasRightTag || inRightLayer)
            damageable.TakeDamage(damage, false);
   }


}
