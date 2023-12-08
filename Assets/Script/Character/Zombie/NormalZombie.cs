using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Hojun;
using UnityEditor;
using UnityEngine.AI;
using Jaeyoung;

namespace Hojun 
{


    public class NormalZombie : Zombie
    {


        public new void Awake()
        {
        
            base.Awake();


            moveDict.Add(ZombieMove.SEARCH, new SearchStrategy(this));
            moveDict.Add(ZombieMove.IDLE, new IdleStrategy(this));
            
            stateMachine.AddState((int)Zombie.ZombieState.IDLE, new IdleState(stateMachine));
            stateMachine.AddState((int)Zombie.ZombieState.SEARCH, new SearchState(stateMachine));

            stateMachine.SetState((int)Zombie.ZombieState.IDLE);
            //MoveStrategy = moveDict[ZombieMove.IDLE];
        
        }


        public void Start()
        {
            hearComponent = gameObject.GetComponent<HearComponent>();
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
        }


    }



}
