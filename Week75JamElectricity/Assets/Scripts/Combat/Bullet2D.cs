using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet2D : MonoBehaviour
{
    [SerializeField] float damage;
    [Tooltip("How many objects this has to collide with before it is automatically erased.")]
    [SerializeField] int collisionsBeforeDeath = 1;
    
    [Tooltip("How many seconds this bullet is allowed to live, regardless of collisions.")]
    [SerializeField] float lifetime = 3;
    [Tooltip("This bullet can damage things on these layers.")]
    [SerializeField] LayerMask damageOnLayers;
    [Tooltip("This bullet can damage things with any of these tags.")]
    [SerializeField] List<string> _damageWithTags;
    int collisionsDone = 0;
    float deathTimer;

    public Rigidbody2D rb2d { get; protected set; }
    public Vector2 velocity 
    {
        get { return rb2d.velocity; }
        set { rb2d.velocity = value; }
    }

    public List<string> damageWithTags
    {
        get { return _damageWithTags; }
        protected set { _damageWithTags = value; }
    }

    public new Collider2D collider { get; set; }

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        deathTimer = lifetime;
    }

    void Update()
    {
        DeathCountdown();
    }
   
   protected virtual void OnTriggerEnter2D(Collider2D other)
   {
       HandleDamageApplication(other.gameObject);
       collisionsDone++;
       HandleSelfDestruction();
   }

   protected virtual void OnCollisionEnter2D(Collision2D other)
   {
       HandleDamageApplication(other.gameObject);
       collisionsDone++;
       HandleSelfDestruction();
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

   void HandleSelfDestruction()
   {
       if (collisionsDone >= collisionsBeforeDeath)
        Destroy(this.gameObject);
   }

    protected virtual void DeathCountdown()
    {
        if (deathTimer > 0)
            deathTimer -= Time.deltaTime;
        else if (deathTimer <= 0)
            Destroy(this.gameObject);
    }

}
