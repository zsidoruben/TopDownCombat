using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : HealthScript
{
    public override float CalculateDamage(DamageType type, float damage)
    {
        return damage;
    }
    public override void Die()
    {
        base.Die();
        //Game Manager.EndGame
    }
}
