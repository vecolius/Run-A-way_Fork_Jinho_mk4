using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojun;


namespace Hojun
{

    public class State
    {
        protected IStateMachine stateMachine = null;
        protected GameObject owner;


        public State(IStateMachine sm)
        {

            owner = ((Zombie)sm.GetOwner()).gameObject;

            if (owner == null)
                Debug.Log("ERROR");

        }

        public virtual void Init(IStateMachine sm)
        {
            this.stateMachine = sm;
        }

        public virtual void Enter()
        {
        }
        public virtual void Update()
        {

        }
        public virtual void Exit()
        {
        }
    }



}


