using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damage : MonoBehaviour
{
    public LayerMask layer;
    public float damage; 
    public float knockback;
    public Transform attacker;
    public CameraShake shakeScript;
    public DamageType currentDamageType = DamageType.Normal;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == layer);
        {
            GetHealthScript(collision.gameObject);
            
        }
    }

    protected abstract void GetHealthScript(GameObject go);
}
