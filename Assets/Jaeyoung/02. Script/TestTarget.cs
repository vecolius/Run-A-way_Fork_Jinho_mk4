using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Jaeyoung
{

    // 듣는 전략
    public class HearTest : Hojun.IHearStrategy
    {

    }

    //전략을 가지는 오브젝트
    public class TestTarget : MonoBehaviour
    {
        public Hojun.IHearStrategy HearStrategy;
        public Hojun.StateMachine<TestTarget> stateMachine;
        public NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            stateMachine = new Hojun.StateMachine<TestTarget>(this);
            HearStrategy = new HearTest(this, 2.0f);
            //stateMachine.AddState(Hojun.MoveState.IDLE, );
            //stateMachine.AddState(Hojun.MoveState.WALK, );
            //stateMachine.AddState(Hojun.MoveState.RUN, );
            stateMachine.SetState(Hojun.MoveState.IDLE);
        }

        private void Update()
        {
            stateMachine.Update();
        }

    }
}