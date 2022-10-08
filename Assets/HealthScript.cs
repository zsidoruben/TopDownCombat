using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public enum DamageType
{
    RangedDamage,
    LightDamage,
    HeavyDamage, 
    Normal
}

public abstract class HealthScript : MonoBehaviour
{
    //public Animator anim;
    public List<MonoBehaviour> scriptsToDisable;
    public List<GameObject> gameObjectsToDisable;

    public GameObject deadObj;
    public SpriteRenderer renderer;
    public Slider HealthSlider;
    public float maxHealth;
    public float armor = 0;
    //public float fireDamageMult = 1;
    //public float electricityDamageMult = 1;
    //public float physicalDamageMult = 1;
    public Rigidbody2D rb;
    public UnityEvent<float> OnDamaged;
    public float minDmgDelay = 0.1f;

    private float lastDamageTime = 0;
    private float currHealth;
    private CircleCollider2D coll;

    void Start()
    {
        currHealth = maxHealth;
        HealthSlider.maxValue = maxHealth;
        HealthSlider.value = currHealth;
        coll = GetComponent<CircleCollider2D>();

    }
    public virtual void TakeDamage(float damage, Transform attacker, float knockback, DamageType type)
    {
        if (Time.time < lastDamageTime + minDmgDelay)
        {
            return;
        }
        float calculatedDamage = CalculateDamage(type, damage);
        OnDamaged?.Invoke(calculatedDamage);
        currHealth -= calculatedDamage;
        Vector3 force = transform.position - attacker.transform.position;
        //rb.AddForce(force.normalized * knockback, ForceMode2D.Impulse);
        rb.velocity = force * knockback;
        lastDamageTime = Time.time;
        if (currHealth <= 0)
        {
            currHealth = 0;
            Die();
        }
        HealthSlider.value = currHealth;

    }
    public virtual void Die()
    {
        foreach (MonoBehaviour item in scriptsToDisable)
        {
            item.enabled = false;
        }
        foreach (GameObject item in gameObjectsToDisable)
        {
            item.SetActive(false);
        }
        renderer.enabled = false;
        coll.enabled = false;
        rb.isKinematic = true;
        deadObj.SetActive(true);

        //Destroy(gameObject);
    }

    public abstract float CalculateDamage(DamageType type, float damage);
    }
