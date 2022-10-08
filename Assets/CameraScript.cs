using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraVelocity = Vector3.zero;
    public Vector3 cameraOffset;
    public float CameraFollowSmoothTime;

   
    private void FixedUpdate()
    {
        Vector3 targetPos = player.position + cameraOffset;
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref cameraVelocity, CameraFollowSmoothTime);
    }
}
