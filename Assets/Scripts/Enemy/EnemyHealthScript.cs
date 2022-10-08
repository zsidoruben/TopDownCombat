using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealthScript : HealthScript
{
    public float rangedDamageMult = 1;
    public float lightDamageMult = 1;
    public float heavyDamageMult = 1;
    

    public override float CalculateDamage(DamageType type, float damage)
    {
        float finalDamage = damage;

        if (type == DamageType.RangedDamage)
        {
            finalDamage *= rangedDamageMult;
        }
        if (type == DamageType.LightDamage)
        {
            finalDamage *= lightDamageMult;
        }
        if (type == DamageType.HeavyDamage)
        {
            finalDamage *= heavyDamageMult;
        }
        if (finalDamage >= 0)
        {
            finalDamage -= armor;
            if (finalDamage < 0)
            {
                finalDamage = 0;
            }
        }
        
        return finalDamage;
    }

    IEnumerator animenable(float staggerTime)
    {
        yield return new WaitForSeconds(0.1f);
        //anim.enabled = true;
    }
}

