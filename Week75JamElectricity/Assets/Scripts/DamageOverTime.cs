using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    [SerializeField] FloatReference toReduce;
    [SerializeField] float damagePerSecond = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toReduce.value > 0)
            toReduce.value -= Time.deltaTime * damagePerSecond;
    }
}
