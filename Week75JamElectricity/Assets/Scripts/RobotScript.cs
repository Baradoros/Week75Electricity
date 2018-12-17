using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour {
    public RaycastHit2D Ray;
    public bool startMovingLeft = true;
    public GameObject RayTarget;
    public Rigidbody2D rb2d { get; protected set; }
    [SerializeField] float _moveSpeed = 3;
    [SerializeField] bool _aiControlled = true;
    public Vector2 direction { get; set; }
    public Vector2 velocity
    {
        get { return rb2d.velocity; }
        set { rb2d.velocity = value; }
    }

    public bool aiControlled { get { return _aiControlled; } }


    protected virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        if (startMovingLeft)
            direction = Vector2.left;
        else 
            direction = Vector2.right;
    }

	// Update is called once per frame
	protected virtual void Update () {
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

        HandleMovement();
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

    protected virtual void HandleMovement()
    {
        // Keep moving this robot in one direction.
        //transform.position += Direction * transform.right * Time.deltaTime;

        velocity = direction * _moveSpeed;
    }
}
