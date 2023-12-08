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

        public FindState(IStateMachine sm) : base(sm)
        {
            ownerZombie = owner.GetComponent<Zombie>();
            agent = owner.GetComponent<NavMeshAgent>();
            if (ownerZombie == null)
            {
                Debug.Log("ERROR");
            }

        }


        public override void Enter()
        {
            //aniCompo.SetBool("Walk" , true);
            agent.enabled = true;
            ownerZombie.MoveStrategy = ownerZombie.GetMoveDict(Zombie.ZombieMove.FIND);

        }
        public override void Update() 
        {
            //aniCompo.SetBool("Walk" , false);
            //agent.enabled = false;

            ownerZombie.Move();


        }

        public override void Exit() 
        {




        }


    }
}
