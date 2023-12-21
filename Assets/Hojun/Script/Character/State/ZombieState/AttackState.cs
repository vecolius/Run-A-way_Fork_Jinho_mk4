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
            // 추가적으로  state 말고도 좀 추가할 것
            // 예를들면 strategy 에 의해서 Attack을 좀 변하게 해야할듯.
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
                stateMachine.SetState((int)Zombie.ZombieState.FIND);
                return;
            }
        }

    }
}