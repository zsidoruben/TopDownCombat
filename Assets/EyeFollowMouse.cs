using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollowMouse : EyeFollowPlayer
{
    // Update is called once per frame
    public override void Update()
    {
       Vector3 WorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        EyeFollow(WorldPoint);
    }
}
