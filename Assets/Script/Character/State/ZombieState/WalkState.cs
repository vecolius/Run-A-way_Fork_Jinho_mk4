using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hojun
{


    public class WalkState : State
    {

        Zombie ownerZombie;
        Animator aniCompo;

        public WalkState(IStateMachine sm) : base(sm)
        {
            GameObject owner = (GameObject)sm.GetOwner();

            if( owner.TryGetComponent<Zombie>( out ownerZombie ) ) 
            {
            }
            else
                Debug.Log("Don't have Zombie Compo");
            
        }

        public override void Enter()
        {
            aniCompo.SetBool("Walk" , true);
            ownerZombie.MoveStrategy = ownerZombie.GetMoveDict(Zombie.ZombieState.SEARCH_WALK);
            ownerZombie.Move();
        }

        public override void Exit()
        {
            aniCompo.SetBool("Walk" , false);
        }

        public override void Update()
        {



            // TODOLIST 재영이형 Heara 구현된거 return  바탕으로 분기점 나눌 것
            // 어떻게 나눌 것 이냐.
            // 거기서 던져준 enum이 넘어가야할 상태를 뜻 함
            // 그냥 그거 매칭해서 각각의 상태가 넘어가야할 곳 을 정해서 필요한 값만
            // 넘어가면 문제 없을 듯

        }

    }

}