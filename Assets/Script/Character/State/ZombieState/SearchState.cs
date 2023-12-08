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


        
        // 들은 소리가 runHearValue이상이면 달리게 됨
        public float runHearValue = 20f;

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
            ownerZombie.MoveStrategy = ownerZombie.GetMoveDict(Zombie.ZombieMove.SEARCH);
            

            Debug.Log("searchEnter");
        }

        public override void Exit()
        {
            //aniCompo.SetBool("Walk" , false);
            agent.enabled = false;
        }

        public override void Update()
        {

            Debug.Log("search update");

            ownerZombie.Move();

            if (ownerZombie.HearValue >= runHearValue)
            {
                Debug.Log("running");
                agent.speed = ownerZombie.zombieData.Speed*1.4f;
            }

        }

    }

}