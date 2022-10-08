using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : Damage
{
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void GetHealthScript(GameObject go)
    {
        PlayerHealthScript script = go.GetComponent<PlayerHealthScript>();
        if (script != null)
        {
            script.TakeDamage(damage, attacker, knockback, currentDamageType);
            shakeScript.StartShake(0.1f, damage);
        }
    }
}
