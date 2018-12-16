using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour {
    public RaycastHit2D Ray;
    public int Direction;
    public GameObject RayTarget;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
        transform.position += Direction*transform.right * Time.deltaTime; 
	}
    void OnTriggerEnter2D(Collider2D other)
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
}
