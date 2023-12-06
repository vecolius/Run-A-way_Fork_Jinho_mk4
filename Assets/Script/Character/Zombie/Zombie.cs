using Hojun;
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


    public abstract class Zombie : Character, IZombieAble
    {

        protected StateMachine<Zombie> stateMachine;
        protected ZombieData zombieData;
        public ZombieData Data { get => zombieData;}

        public virtual float HearValue 
        {
            get 
            {
                return hearValue;
            }
            set 
            {
                hearValue = value;
            }
        }
        float hearValue;

        Dictionary<ZombieState, IMoveStrategy> zombieSt;
        public IMoveStrategy GetMoveDict(ZombieState move)
        {
            return zombieSt[move];
        }
        
        public IMoveStrategy MoveStrategy { get => moveStrtegy; set { moveStrtegy = value; } }
        IMoveStrategy moveStrtegy;



        //public IHearStrategy HearStrategy { get => hearStrategy; set { hearStrategy = value; } }
        //IHearStrategy hearStrategy;
        

        protected void Awake()
        {
            stateMachine = new StateMachine<Zombie>(this);
        }

        public virtual void Move(GameObject target) 
        {
            MoveStrategy.Move(target);
        }

    }



}
