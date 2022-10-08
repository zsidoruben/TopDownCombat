using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : Damage
{
    protected override void GetHealthScript(GameObject go)
    {
        EnemyHealthScript script = go.GetComponent<EnemyHealthScript>();
        if (script != null)
        {
            script.TakeDamage(damage, attacker, knockback, currentDamageType);
            shakeScript.StartShake(0.1f, damage);
        }
    }

}
