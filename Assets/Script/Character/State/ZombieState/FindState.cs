using Hojun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hojun 
{

    public class FindState : Hojun.State
    {

        Zombie ownerZombie;
        Animator animator;
        NavMeshAgent agent;
        float runSpeed = 1.4f;

        public FindState(IStateMachine sm) : base(sm)
        {
            ownerZombie = owner.GetComponent<Zombie>();
            animator = owner.GetComponent<Animator>();
            agent = owner.GetComponent<NavMeshAgent>();


            if (ownerZombie == null)
            {
                Debug.Log("ERROR");
            }
            
        }


        public override void Enter()
        {
            animator.SetInteger( "State", (int)Zombie.ZombieMove.FIND );
            animator.SetBool( "Run", true );
            ownerZombie.MoveStrategy = ownerZombie.GetMoveDict(Zombie.ZombieMove.FIND);
            agent.speed = ownerZombie.Speed * runSpeed;
        }
        public override void Update() 
        {
            if (ownerZombie.IsAttack)
            {
                Debug.Log("attack");
                stateMachine.SetState((int)Zombie.ZombieState.ATTACK);
                return;
            }
            
            ownerZombie.Move();
        }

        public override void Exit() 
        {

        }


    }
}
