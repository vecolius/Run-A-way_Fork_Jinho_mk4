using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hojun
{
    public class DeadState : State
    {
        Zombie ownerZombie;
        Animator aniCompo;
        NavMeshAgent agent;

        public DeadState(IStateMachine sm) : base(sm)
        {
            ownerZombie = owner.GetComponent<Zombie>();
            aniCompo = owner.GetComponent<Animator>();
            agent = owner.GetComponent<NavMeshAgent>();

            if (ownerZombie == null)
            {
                Debug.Log("ERROR");
            }

        }

        public override void Enter()
        {
            Debug.Log("dead");
            ownerZombie.Die();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }

}
