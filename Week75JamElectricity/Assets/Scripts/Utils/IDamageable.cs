using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable<T>
{
    bool TakeDamage(T damage, bool triggerInvin = false);
}
