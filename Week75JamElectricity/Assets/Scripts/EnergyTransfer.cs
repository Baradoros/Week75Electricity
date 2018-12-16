using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTransfer : MonoBehaviour
{
    public GameObject Ball;
    public bool Shooting;
    public GameObject EBall;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Shooting == false)
        {
            
            StartCoroutine("Shoot");
        }
    }
    IEnumerator Shoot()
    {
        GetComponent<PlayerController>().enabled = false;
        Shooting = true;
        Time.timeScale = 0.2f;
        EBall = Instantiate(Ball, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.4f);
        Time.timeScale = 1;
        Shooting = false;
        Debug.Log("???");
        GetComponent<RobotScript>().enabled = true;
       
        GetComponent<EnergyTransfer>().enabled = false;

    }
}
