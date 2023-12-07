using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hojun
{


    public class SearchState : State
    {

        Zombie ownerZombie;
        Animator aniCompo;
        NavMeshAgent agent;
        public float runHearValue = 30f;




        public SearchState(IStateMachine sm) : base(sm)
        {
            ownerZombie = owner.GetComponent<Zombie>();
            agent = owner.GetComponent<NavMeshAgent>();
            if(ownerZombie == null) 
            {
                Debug.Log("ERROR");
            }

        }

        public override void Enter()
        {
            //aniCompo.SetBool("Walk" , true);
            agent.enabled = true;
            ownerZombie.MoveStrategy = ownerZombie.GetMoveDict(Zombie.ZombieMove.WALK);
            ownerZombie.Move();

            Debug.Log("searchEnter");
        }

        public override void Exit()
        {
            //aniCompo.SetBool("Walk" , false);
            agent.enabled = false;
        }

        public override void Update()
        {

            Debug.Log("check");
            
            Debug.Log(ownerZombie.traceTarget);
            Debug.Log(ownerZombie.destination);


            if (ownerZombie.hearValue >= runHearValue)
            {
                Debug.Log("Run");
                agent.speed = ownerZombie.zombieData.Speed * 1.4f;
                ownerZombie.MoveStrategy = ownerZombie.GetMoveDict(Zombie.ZombieMove.RUN);
            }
            else
            {
                agent.speed = ownerZombie.zombieData.Speed;
                Debug.Log("Walk");
            }

        }

    }

}