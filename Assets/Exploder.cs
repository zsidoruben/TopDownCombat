using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public float time;
    public float range;
    public float force;

    private float nextTime;
    // Start is called before the first frame update
    void Start()
    {
        nextTime = Time.time + time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
            foreach (Collider2D collider in colliders)
            {
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 dir = rb.position - (Vector2)transform.position;
                    dir.Normalize();
                    rb.AddForce(dir * force, ForceMode2D.Impulse);
                }
            }
        }
    }
}
