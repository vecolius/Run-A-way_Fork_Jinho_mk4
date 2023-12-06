using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojun;


namespace Hojun
{

    public class State
    {
        public IStateMachine sm = null;
        public GameObject owner;


        public State(IStateMachine sm)
        {
            if(sm.GetOwner() is GameObject)
                owner = (GameObject)sm.GetOwner();

            if (owner == null)
                Debug.Log("ERROR");

        }

        public virtual void Init(IStateMachine sm)
        {
            this.sm = sm;
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


