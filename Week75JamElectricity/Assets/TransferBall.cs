using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferBall : MonoBehaviour
{
    public int State;
    public float Power;
    public GameObject Distance;
    public Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesHitTriggers = true;
        StartCoroutine("Launch");
    }
    // Update is called once per frame
    void Update()
    {
        if(State == 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
        }
       
    }
    void OnMouseDrag()
    {
        
        if (State == 1)
        {
           
            Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

           
            float radius = 1.8f;
            Vector2 dir = p - startPos;
            if (dir.sqrMagnitude > radius)
                dir = dir.normalized * radius;

            
            Distance.transform.position = startPos + dir;
        }
    }
    void OnMouseUp()
    {
        State = 2;
        Vector2 dir = startPos - (Vector2)Distance.transform.position;
        GetComponent<Rigidbody2D>().AddForce(dir.normalized * 4f,ForceMode2D.Force);
     

    }
    IEnumerator Launch()
    {
       
            
            yield return new WaitForSeconds(0.05f);
       
        startPos = transform.position;
        State = 1;
    }
    
}
