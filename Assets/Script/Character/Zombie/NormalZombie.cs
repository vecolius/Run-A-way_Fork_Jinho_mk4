using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Hojun;
using UnityEditor;
using UnityEngine.AI;

namespace Hojun 
{

    public enum MoveState
    {
        IDLE,
        WALK,
        RUN
    }
    public class NormalZombie : Zombie, IMoveAble
    {


        public new void Awake()
        {
        
            base.Awake();


            moveDict.Add(ZombieMove.SEARCH, new SearchStrategy(this));
            moveDict.Add(ZombieMove.IDLE, new IdleStrategy(this));
            
            stateMachine.AddState(Zombie.ZombieState.IDLE, new IdleState(stateMachine));
            stateMachine.AddState(Zombie.ZombieState.SEARCH, new SearchState(stateMachine));

            stateMachine.SetState(Zombie.ZombieState.IDLE);
            //MoveStrategy = moveDict[ZombieMove.IDLE];
        
        }


        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
        }


    }



}
