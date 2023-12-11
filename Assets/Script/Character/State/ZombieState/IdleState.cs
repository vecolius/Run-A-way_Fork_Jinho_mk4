using Hojun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Hojun.Zombie;

namespace Hojun
{

    public class IdleState : Hojun.State
    {

        Zombie ownerZombie;
        Animator animator;

        public float HearSoundWalk
        {
            get
            {

                Debug.Log("팀원과 상의해서 추가 변경 할 것. 지우지 말것!");
                // TODO_LIST 계산식 추가하던가 해서 체크해 볼 것;
                return 10f;
            }
        }

        public float HearSoundRun
        {

            get
            {
                Debug.Log("팀원과 상의해서 추가 변경 할 것. 지우지 말것!");
                return 20f;
            }
        }

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
            animator.SetInteger( "State" , (int)ZombieState.IDLE);
            ownerZombie.MoveStrategy = ownerZombie.GetMoveDict(ZombieMove.IDLE);
        }

        public override void Exit()
        {

        }

        public override void Update()
        {

            Debug.Log("not bug");


            if (ownerZombie.IsFindPlayer)
                stateMachine.SetState((int)Zombie.ZombieState.FIND);
            

            if (ownerZombie.HearValue >= 0.1f)
                stateMachine.SetState((int)Zombie.ZombieState.SEARCH);

        }


    }
}