using Hojun;
using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hojun
{

    public class ZombieData
    {
        float hp;
        float speed;
        bool isDead;
        float attack;

        public ZombieData(float hp , float spped , float attack) 
        {
            this.hp = hp;
            this.speed = spped;
            this.isDead = false;
            this.attack = attack;
        }

        public float Hp { get => hp;}
        public float Speed { get => speed;}
        public bool IsDead { get => isDead;}
        public float Attack { get => attack;}

    }


    public abstract class Zombie : Character, IMoveAble
    {

        protected StateMachine<Zombie> stateMachine;
        protected ZombieData zombieData;
        public HearComponent hearComponent;

        public float hearValue;

        public ZombieData Data { get => zombieData;}

        public Transform traceTarget;
        public Vector3 destination = Vector3.negativeInfinity;

        public enum ZombieState
        {
            IDLE,
            SEARCH,
            FIND
        }

        public enum ZombieMove
        {
            IDLE,
            WALK,
            RUN
        }
        Dictionary<ZombieMove, IMoveStrategy> moveDict;

        public IMoveStrategy GetMoveDict(ZombieMove move)
        {
            return moveDict[move];
        }

        public IMoveStrategy MoveStrategy { get => moveStrategy; set { moveStrategy = value; } }
        IMoveStrategy moveStrategy;

        protected void Awake()
        {
            stateMachine = new StateMachine<Zombie>(this);
            hearComponent = GetComponent<HearComponent>();
        }


        public virtual void Move() 
        {
            if (traceTarget != null)
                moveStrategy.Move(traceTarget.gameObject);

            else if(destination != Vector3.negativeInfinity)
                MoveStrategy.Move(destination);
        }

        public void Hear(GameObject soundOwner)
        {

            // hear
            // TODOLIST
            // movestrategy가 사용할 변수인 tractarget , destination 셋팅 해 주기
            // 해당 정보는 soundOwner에서 추출해서 정해줄 것

            throw new System.NotImplementedException();
        }
    }



}
