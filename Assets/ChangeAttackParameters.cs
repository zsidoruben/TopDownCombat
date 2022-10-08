using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAttackParameters : StateMachineBehaviour
{
    public DamageType type;
    DamageEnemy Damage;
    public float damage;
    public float knockback;
    private float defDamage;
    private float defKnockback;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerCombat Script = animator.gameObject.GetComponentInParent<PlayerCombat>();
        Damage = Script.Weapon;
        defDamage = Damage.damage;
        defKnockback = Damage.knockback;
        Damage.damage = this.damage;
        Damage.knockback = this.knockback;
        Damage.currentDamageType = type;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Damage.damage = defDamage;
        Damage.knockback = defKnockback;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
