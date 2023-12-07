using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Hojun;
using UnityEditor;


namespace Hojun 
{

<<<<<<< Updated upstream

    public class NormalZombie : Zombie
=======
    public enum MoveState
    {
        IDLE,
        WALK,
        RUN
    }
    public class NormalZombie : Zombie, IMoveAble
>>>>>>> Stashed changes
    {
        Vector3 myvec;

        public new void Awake()
        {
            myvec = gameObject.transform.localPosition;

            base.Awake();
            stateMachine = new StateMachine<Zombie>(this);
            stateMachine.AddState(Zombie.ZombieState.IDLE, new IdleState(stateMachine));
            stateMachine.AddState(Zombie.ZombieState.SEARCH_WALK , new SearchState(stateMachine) );
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
        }

        public override void Move()
        {

        }


    }



}
