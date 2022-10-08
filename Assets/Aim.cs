using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Vector2 Target { get; set; }
    public bool isInstant = true;
    public bool isSlerping = false;
    public bool isTurret = false;
    public bool isrotating = true;



    public float turnSpeed;

    // Update is called once per frame
    public virtual void Update()
    {
        float RotationZ = transform.rotation.z;
        Vector3 Difference = (Vector3)Target - transform.position;
        Difference.Normalize();
        RotationZ = Mathf.Atan2(Difference.y, Difference.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, RotationZ); ;
        if (isInstant)
        {
            transform.rotation = targetRotation;
        }
        else if (isSlerping)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else if (isTurret)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.time * turnSpeed / 100);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, Target);
    }
}
