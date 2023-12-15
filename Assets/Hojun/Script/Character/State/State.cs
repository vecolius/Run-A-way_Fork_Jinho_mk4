using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojun;
using Photon.Pun;

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

        [PunRPC]
        public abstract void Enter();
        [PunRPC]
        public abstract void Update();
        [PunRPC]
        public abstract void Exit();
    }



}


