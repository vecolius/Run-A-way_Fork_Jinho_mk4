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
            aniCompo = owner.GetComponent<Animator>();
            agent = owner.GetComponent<NavMeshAgent>();

            if(ownerZombie == null) 
            {
                Debug.Log("ERROR");
            }

        }

        public override void Enter()
        {
            aniCompo.SetInteger("State" , (int)Zombie.ZombieState.SEARCH );
            agent.enabled = true;
            ownerZombie.MoveStrategy = ownerZombie.GetMoveDict(Zombie.ZombieMove.SEARCH);
            
        }

        public override void Exit()
        {

        }

        public override void Update()
        {

            if (ownerZombie.IsFindPlayer)
            {
                stateMachine.SetState( (int)Zombie.ZombieState.FIND );
            }

            Debug.Log("search update");
            ownerZombie.Move();

            if (ownerZombie.HearValue >= runHearValue)
            {
                aniCompo.SetBool("Run" , true);
                Debug.Log("running");
                agent.speed = ownerZombie.Speed*1.4f;
            }




        }

    }

}