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

        public enum ZombieState
        {
            IDLE,
            SEARCH,
            FIND
        }

        public enum ZombieMove
        {
            IDLE,
            SEARCH,
            RUN
        }


        [SerializeField]
        float runHearValue;
        public float RunHearValue { get => runHearValue; }

        protected StateMachine<Zombie> stateMachine;
        public ZombieData zombieData;


        public bool IsFindPlayer 
        {
            get
            {
                return isFind;
            }
        }
        bool isFind;

        public float HearValue 
        {
            get => hearComponent.ResultDistance;
              
        }
        public GameObject SoundTraceTarget
        {
            get => hearComponent.SoundOwner;
        }
        public Vector3 SoundTraceArea
        { 
            get => hearComponent.SoundArea;
        }

        [SerializeField]
        protected HearComponent hearComponent;


        public ZombieData Data { get => zombieData;}

        protected Dictionary<ZombieMove, IMoveStrategy> moveDict = new Dictionary<ZombieMove, IMoveStrategy>();
        protected Dictionary<ZombieState, State> stateDict = new Dictionary<ZombieState, State>();


        public IMoveStrategy GetMoveDict(ZombieMove move)
        {
            return moveDict[move];
        }

        public IMoveStrategy MoveStrategy { get => moveStrategy; set { moveStrategy = value; } }
        IMoveStrategy moveStrategy;

        protected void Awake()
        {
            hearComponent = GetComponent<HearComponent>();
            zombieData = new ZombieData(50,10,20);
            stateMachine = new StateMachine<Zombie>(this);
        }

        public virtual void Move() 
        {
            moveStrategy.Move();
        }


    }



}
