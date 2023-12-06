using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hojun
{

    public interface IStateMachine
    {
        object GetOwner();
        void SetState(MoveState stateName);
    }

    public class StateMachine<T> : IStateMachine where T : class
    {
        public T owner = null;
        public State curState;

        public Dictionary<Hojun.MoveState, State> moveDic;


        public StateMachine(T owner)
        {
            this.owner = owner;
            moveDic = new Dictionary<Hojun.MoveState, State>();
        }

        public void AddState(MoveState stateName, State state)
        {
            if (moveDic.ContainsKey(stateName))
                return;

            moveDic.Add(stateName, state);
            state.Init(this);
        }





        public object GetOwner()
        {
            return owner;
        }

        public void SetState(Hojun.MoveState stateName)
        {
            if (moveDic.ContainsKey(stateName))
            {
                if (curState != null)
                {
                    curState.Exit();
                }
                curState = moveDic[stateName];
                curState.Enter();

            }
        }


        public void Update()
        {
            curState?.Update();
        }

    }
}
