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
        public State curState;

        public Dictionary<ZombieState, State> moveDic;


        public StateMachine(T owner)
        {
            this.owner = owner;
            moveDic = new Dictionary<ZombieState, State>();
        }

        public void AddState(ZombieState stateName, State state)
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

        public void SetState(ZombieState stateName)
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
