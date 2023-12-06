using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojun;


namespace Hojun
{

    public abstract class State
    {
        public IStateMachine sm = null;
        protected GameObject owner;

        public State( IStateMachine sm )
        {
            this.sm = sm;

            if (sm.GetOwner() is GameObject)
                owner = (GameObject)sm.GetOwner();
            else
                Debug.Log("Check This State");
            
        }

        public virtual void Init(IStateMachine sm)
        {
            this.sm = sm;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
        
    }



}