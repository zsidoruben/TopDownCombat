using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAttack : EnemyAttack
{
    public override void Attack()
    {
        anim.SetTrigger("Attack");
    }

}
