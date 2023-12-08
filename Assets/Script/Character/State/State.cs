using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojun;


namespace Hojun
{

    public abstract class State
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

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }



}


