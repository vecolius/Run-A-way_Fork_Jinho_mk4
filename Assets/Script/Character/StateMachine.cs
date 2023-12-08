using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Hojun.Zombie;

namespace Hojun
{

    public interface IStateMachine
    {
        object GetOwner();
        void SetState(ZombieState stateName);
    }

    public class StateMachine<T> : IStateMachine where T : class
    {
        public T owner = null;

        public State CurState 
        {
            get
            {
                return curState;
            }
        }
        State curState;

        public Dictionary<ZombieState, State> stateDict;

        public StateMachine(T owner)
        {
            this.owner = owner;
            stateDict = new Dictionary<ZombieState, State>();
            
        }

        public void AddState(ZombieState stateName, State state)
        {
            if (stateDict.ContainsKey(stateName))
                return;

            stateDict.Add(stateName, state);
            state.Init(this);
        }



        public object GetOwner()
        {
            return owner;
        }

        public void SetState(ZombieState stateName)
        {
            if (stateDict.ContainsKey(stateName))
            {
                if (curState != null)
                {
                    curState.Exit();
                }
                curState = stateDict[stateName];
                curState.Enter();

            }
        }


        public void Update()
        {
            curState?.Update();
        }

    }
}
