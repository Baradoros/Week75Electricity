using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeGauge : VariableHPBar
{
    [Tooltip("How many charge points this loses over time.")]
    [SerializeField] float depletionOverTime = 3;

    protected virtual void Update()
    {
        DepleteOverTime();
    }

    protected virtual void DepleteOverTime()
    {
        if (health.value > 0)
            health.value -= Time.deltaTime * depletionOverTime;
    }
}
