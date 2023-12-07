using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hojun
{


    public class SearchState : State
    {

        Zombie ownerZombie;
        Animator aniCompo;

        public float walkHearValue = 10f;
        public float runHearValue = 20f;


        public SearchState(IStateMachine sm) : base(sm)
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
            ownerZombie.MoveStrategy = ownerZombie.GetMoveDict(Zombie.ZombieMove.IDLE);
        }

        public override void Exit()
        {
            aniCompo.SetBool("Walk" , false);
        }

        public override void Update()
        {

            if( ownerZombie.hearValue <= walkHearValue && ownerZombie.hearValue >= runHearValue ) 
            {
                Debug.Log("walk");
                ownerZombie.MoveStrategy = ownerZombie.GetMoveDict(Zombie.ZombieMove.WALK);
            }
            else if ( ownerZombie.hearValue <= runHearValue )
            {

                Debug.Log("Run");
                ownerZombie.MoveStrategy = ownerZombie.GetMoveDict(Zombie.ZombieMove.RUN);
            }

            ownerZombie.Move();
        }

    }

}