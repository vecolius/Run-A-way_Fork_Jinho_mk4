using Jaeyoung;
using Jinho;
using System.Collections.Generic;
using UnityEngine;



public enum CustomObjectLayer
{
    ZOMBIE = 0<<20,

}


namespace Hojun
{

    public class ZombieData
    {
        public float hp;
        public float speed;
        public bool isDead;
        public float attackPoint;
        public string zombieName;

        public ZombieData(float hp , float spped , float attack) 
        {
            this.hp = hp;
            this.speed = spped;
            this.isDead = false;
            this.attackPoint = attack;
        }

    }


    public abstract class Zombie : Character, IMoveAble ,IDieable , IAttackAble 
    { 

        public enum ZombieState
        {
            IDLE,
            SEARCH,
            FIND,
            DEAD,
            ATTACK,
            HIT
        }

        public enum ZombieMove
        {
            IDLE,
            SEARCH,
            FIND
        }



        DetectiveComponent detectiveCompo;

        [SerializeField]
        float runHearValue;
        public float RunHearValue { get => runHearValue; }

        protected StateMachine<Zombie> stateMachine;

        public virtual float Hp 
        {
            get => zombieData.hp; set { zombieData.hp = value; } 
        }

        public virtual float Speed
        {
            get => zombieData.speed; set { zombieData.speed = value; }
        }

        public virtual float AttackPoint
        {
            get => zombieData.attackPoint; set {zombieData.attackPoint = value;  }
        }
        public virtual bool IsDead
        {
            get=> zombieData.isDead; set {  zombieData.isDead = value; }
        }

        protected CharacterData zombieData;


        public bool IsFindPlayer 
        {
            get
            {
                if (detectiveCompo.IsFind)
                {
                    Target = detectiveCompo.targetObj.gameObject;
                    return true;
                }
                    
                return false;
            }
        }
        
        public GameObject Target 
        {
            protected set 
            {
                target = value;
            }
            get
            {
                return target;
            }
        }
        [SerializeField]
        GameObject target;

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

        public bool IsAttack 
        {
            get
            {
                if (detectiveCompo.IsAttack)
                {
                    if (Target.GetComponent<IHitAble>() != null)
                    {
                        return true;
                    }

                }


                return false;
            }
        }


        [SerializeField]
        protected HearComponent hearComponent;


        public CharacterData Data { get => zombieData;}



        //protected Dictionary<AttackStrategy >
        protected Dictionary<ZombieMove, IMoveStrategy> moveDict = new Dictionary<ZombieMove, IMoveStrategy>();
        protected Dictionary<ZombieState, State> stateDict = new Dictionary<ZombieState, State>();


        public IMoveStrategy GetMoveDict(ZombieMove move)
        {
            return moveDict[move];
        }

        public IMoveStrategy MoveStrategy { get => moveStrategy; set { moveStrategy = value; } }

        public abstract WeaponData WeaponData { get; }
        public abstract ItemType ItemType { get; }

        IMoveStrategy moveStrategy;

        protected void Awake()
        {
            hearComponent = GetComponent<HearComponent>();
            //zombieData = new CharacterData(50,10,20);
            stateMachine = new StateMachine<Zombie>(this);
            detectiveCompo = GetComponent<DetectiveComponent>();
        }

        public virtual void Move() 
        {
            moveStrategy.Move();
        }

        public abstract void Die();
        public abstract void Attack();
        public abstract GameObject GetAttacker();
    }



}
