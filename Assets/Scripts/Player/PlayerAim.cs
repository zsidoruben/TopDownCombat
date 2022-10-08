using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAim : Aim
{
    [SerializeField]
    public InputActionReference mousepos;

    public override void Update()
    {
        Target = Camera.main.ScreenToWorldPoint( mousepos.action.ReadValue<Vector2>());
        //worldPos = mainCam.ScreenToWorldPoint(Mouse.current.position);

        base.Update();
    }
}
