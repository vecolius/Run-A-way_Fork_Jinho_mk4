using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hojun
{
    // 코드 훔쳐감. 산타(??)
    // 글씨체 개 귀엽네요.
    // 타자소리 되게 타닥거린다고 생각했는데 치는 맛 쩐다.
    // 훔쳐가도 되나요? ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ
    
    
    //                **
    //             ********
    //               ****
    //              *    *

    //                **
    //              ******
    //            **********
    //              ******
    //            **********
    //          **************
    //        ******************
    //            **********
    //         ****************
    //       ********************
    //     ************************
    //              ******
    //              ******
    //              ******


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
