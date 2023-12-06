using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hojun
{

    public interface IStateMachine
    {
        object GetOwner();
        void SetState(ZombieState stateName);
    }


    public enum ZombieState
    {
        IDLE,
        SEARCH_WALK,
        SEARCH_RUN,
        FIND
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
<<<<<<< Updated upstream
        public Dictionary<MoveState, State> stateDic;
=======

>>>>>>> Stashed changes

        public StateMachine(T owner)
        {
            this.owner = owner;
<<<<<<< Updated upstream
            stateDic = new Dictionary<MoveState, State>();
=======
>>>>>>> Stashed changes
        }

        public void AddState(ZombieState stateName, State state)
        {
<<<<<<< Updated upstream
            if (stateDic.ContainsKey(stateName))
                return;

            stateDic.Add(stateName, state);
            state.Init(this);

=======

        
>>>>>>> Stashed changes
        }

        public object GetOwner()
        {
            return owner;
        }

<<<<<<< Updated upstream
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
=======
        public void SetState(Hojun.ZombieState stateName)
        {
        //    if (moveDic.ContainsKey(stateName))
        //    {
        //        if (curState != null)
        //        {
        //            curState.Exit();
        //        }
        //        curState = moveDic[stateName];
        //        curState.Enter();
        //    }
>>>>>>> Stashed changes
        }

        public void Update()
        {
            curState?.Update();
        }

    }
}
