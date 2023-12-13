using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace Hojun
{


    public class AttackState : State
    {

        Zombie ownerZombie;
        Animator aniCompo;
        NavMeshAgent agent;


        public AttackState(IStateMachine sm) : base(sm)
        {
            ownerZombie = owner.GetComponent<Zombie>();
            aniCompo = owner.GetComponent<Animator>();
            agent = owner.GetComponent<NavMeshAgent>();

        }

        public override void Enter()
        {
            agent.ResetPath();
            agent.velocity = Vector3.zero;
            agent.speed = 0;
            aniCompo.SetInteger("State" , (int)Zombie.ZombieState.ATTACK );
            agent.SetDestination(ownerZombie.transform.position);
        }

        public override void Exit()
        {
            agent.isStopped = false;
        }

        public override void Update()
        {
            agent.isStopped = true;

            
            
            if (!ownerZombie.IsAttack)
            {
                Debug.Log("check");
                stateMachine.SetState( (int)Zombie.ZombieState.FIND );

                return;
            }
        }

    }
}