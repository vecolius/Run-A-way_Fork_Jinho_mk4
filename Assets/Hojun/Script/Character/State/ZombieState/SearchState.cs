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

        float arriveDestination = 5f;
        const float runSpeed = 1.4f;

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
            agent.speed = runSpeed;
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


            Vector3 ownerPos =  ownerZombie.transform.position;
            Vector3 detectedPos = ownerZombie.TargetArea;


            if (Vector3.Distance(ownerPos, detectedPos) <= arriveDestination)
            {
                stateMachine.SetState((int)Zombie.ZombieState.IDLE);
                ownerZombie.InitTarget();
                return;
            }
            ownerZombie.Move();

        }

    }

}