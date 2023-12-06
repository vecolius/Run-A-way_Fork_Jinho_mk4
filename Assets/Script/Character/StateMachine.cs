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

    public enum MoveState
    {
        IDLE,
        WALK,
        RUN
    }


    public class StateMachine<T> : IStateMachine where T : class
    {
        public T owner = null;
        public State curState;
        public Dictionary<MoveState, State> stateDic;

        public StateMachine(T owner)
        {
            this.owner = owner;
            stateDic = new Dictionary<MoveState, State>();
        }

        public void AddState(MoveState stateName, State state)
        {
            if (stateDic.ContainsKey(stateName))
                return;

            stateDic.Add(stateName, state);
            state.Init(this);

        }

        public object GetOwner()
        {
            return owner;
        }

        public void SetState(MoveState stateName)
        {
            if (stateDic.ContainsKey(stateName))
            {
                if (curState != null)
                {
                    curState.Exit();
                }
                curState = stateDic[stateName];
                curState.Enter();

            }
        }

        public void Update()
        {
            curState?.Update();
        }

    }
}
