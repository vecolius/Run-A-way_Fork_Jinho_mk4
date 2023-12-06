using Hojun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    Zombie ownerZombie;
    Animator animator;

    public IdleState(IStateMachine sm) : base(sm)
    {

        ownerZombie = owner.GetComponent<Zombie>();

        if (ownerZombie == null)
            Debug.Log("Error");

        animator = owner.GetComponent<Animator>();

        if (animator == null)
            Debug.Log("Error");

    }

    public override void Enter()
    {
        animator.SetBool("Idle" , true);
    }

    public override void Exit() 
    {
        animator.SetBool("Idle" , false);
    }

    public override void Update() 
    {

    }


}
