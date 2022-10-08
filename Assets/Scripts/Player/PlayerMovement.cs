using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region movement variables
    public float speed;
    public float frictionModifier;
    public float acceleration;
    public float deceleration;
    public float velPower;
    #endregion
    #region dash variables
    public float dashVelocity;
    public float dashTime;
    public float dashRechargeRate;
    public float maxDashCharges;
    float currDashCharges;
    bool canDash = true;
    bool isDashing;
    public GameObject dashEffects;
    #endregion
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator anim;
    public Slider DashSlider1;
    public Slider DashSlider2;
    public Collider2D coll;
    public float currentSpeed;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currDashCharges = maxDashCharges;
    }

    private void Update()
    {
        currDashCharges += Time.deltaTime * dashRechargeRate;
        currDashCharges = Mathf.Min(currDashCharges, maxDashCharges);
        if (currDashCharges >= 1)
        {
            DashSlider1.value = 1;
            DashSlider2.value = currDashCharges - 1;
        }
        else if (currDashCharges < 1)
        {
            DashSlider1.value = currDashCharges;
            DashSlider2.value = 0;
        }


        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash(movement);
        }
    }

    private void FixedUpdate()
    {
        
        //if (Mathf.Abs(movement.x) >= 0.01 || Mathf.Abs(movement.y) >= 0.01)
        //{
        //    anim.SetBool("IsMoving", true);
        //}
        //else
        //{
        //    anim.SetBool("IsMoving", false);
        //}
        //MoveWirhRbMP();
        
        //MoveWithForcesV2(movement);
        if (isDashing)
        {
            return;
        }
        //MoveWithForces(movement);
        MoveWithVelocity(movement);
    }

    

    public void MoveWirhRbMP()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    public void MoveWithVelocity(Vector2 direction)
    {
        if (movement.magnitude > 0 && currentSpeed >= 0)
        {
            currentSpeed += acceleration * speed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deceleration * speed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, speed);
        rb.velocity = movement * currentSpeed;
    }



    public void MoveWithForces(Vector2 direction)
    {
        direction.Normalize();
        if ((Mathf.Abs(direction.x) < 0.01f) && (Mathf.Abs(direction.y) < 0.01f) && !isDashing)
        {
            rb.velocity = Vector3.zero;
            return;
        }


        var targetSpeed = direction * speed;
            //calculate difference between current velocity and desired velocity
            var speedDiff = targetSpeed - rb.velocity;
            //change acceleration rate depending on situation
            float accelRateX = (Mathf.Abs(targetSpeed.x) > 0.01f) ? acceleration : deceleration;
            float accelRatey = (Mathf.Abs(targetSpeed.y) > 0.01f) ? acceleration : deceleration;

            //applies acceleration to speed difference, the raises to a set power so acceleration increases with higher speeds
            //finally multiplies by sign to reapply direction
            float movementX = Mathf.Pow(Mathf.Abs(speedDiff.x) * accelRateX, velPower) * Mathf.Sign(speedDiff.x);
            float movementY = Mathf.Pow(Mathf.Abs(speedDiff.y) * accelRatey, velPower) * Mathf.Sign(speedDiff.y);

            //applies force force to rigidbody, multiplying by Vector2.right so that it only affects X axis 
            rb.AddForce(new Vector2(movementX, movementY) * rb.mass);
        

        float xFriction = 0;
        float yFriction = 0;
        if (Mathf.Abs(direction.x) < 0.01f)
        {
            xFriction = rb.velocity.x;
        }
        if (Mathf.Abs(direction.y) < 0.01f)
        {
            yFriction = rb.velocity.y;
        }
        Vector2 frictionForce = frictionModifier * rb.mass * new Vector2(xFriction, yFriction);
        if (frictionForce != Vector2.zero)
        {
            rb.AddForce(-frictionForce, ForceMode2D.Impulse);
        }


    }

    public void MoveWithForcesV2(Vector2 direction)
    {
        rb.AddForce(direction * speed * rb.mass);
    }

    #region dash 
    public void Dash(Vector2 direction)
    {
        if (!canDash)
        {
            return;
        }
        if (currDashCharges < 1)
        {
            return;
        }
        currDashCharges -= 1;
        canDash = false;
        isDashing = true;
        coll.enabled = false;
        
        dashEffects.SetActive(true);
        rb.velocity = Vector2.zero;
        rb.velocity += direction.normalized * dashVelocity;
        StartCoroutine(StopDashing());
        
    }



    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        dashEffects.SetActive(false);
        isDashing = false;
        canDash = true;
        coll.enabled = true;
    }
    #endregion
}
