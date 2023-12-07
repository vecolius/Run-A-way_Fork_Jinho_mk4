using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Hojun;
using UnityEditor;
using UnityEngine.AI;

namespace Hojun 
{


    public class NormalZombie : Zombie
    {


        public new void Awake()
        {
            base.Awake();
            stateMachine.AddState(Zombie.ZombieState.IDLE, new IdleState(stateMachine));
            stateMachine.AddState(Zombie.ZombieState.SEARCH , new SearchState(stateMachine) );
            stateMachine.SetState(Zombie.ZombieState.IDLE);
            moveDict.Add(ZombieMove.WALK , new SearchStrategy(this) );
            

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


    }



}
