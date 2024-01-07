using Jaeyoung;
using Jinho;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;



public enum CustomObjectLayer
{
    ZOMBIE = 0<<20,
}

namespace Hojun
{

    public abstract class Zombie : Character, IMoveAble ,IDieable , IAttackAble , IHitAble
    {

        const float initHearValue = 0f;

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

        public Action InitTargetAction;
        DetectiveComponent detectiveCompo;
        HearComponent hearComponent;

        [SerializeField]
        private CharacterData zombieData;
        protected StateMachine<Zombie> stateMachine;

        public CharacterData Data { get => zombieData; }
        protected Dictionary<int, IMoveStrategy> moveDict = new Dictionary<int, IMoveStrategy>();
        IMoveStrategy moveStrategy;


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

        public bool IsFindPlayer 
        {
            get
            {
                if (detectiveCompo.IsFind)
                {
                    Target = detectiveCompo.targetObj.gameObject;
                    targetArea = Target.transform.position;
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

                if (value != null)
                    targetArea = value.transform.position;
            
            }
            get
            {
                if(target != null)
                    targetArea = target.transform.position;
                
                return target;
            }
        }
        [SerializeField]
        GameObject target;

        
        public Vector3 TargetArea
        {
            get 
            {
                if(detectiveCompo.targetObj != null)
                    return detectiveCompo.targetObj.transform.position;

                if (hearComponent.SoundOwner != null)
                    return hearComponent.SoundArea;

                return targetArea;
            }
        }
        [SerializeField]Vector3 targetArea;

        public float HearValue 
        {
            get
            {
                if (hearComponent.SoundOwner == null)
                    return initHearValue;

                return hearComponent.ResultDistance;
            }
        }

        public bool IsAttack 
        {
            get
            {
                if (detectiveCompo.IsAttack)
                {
                    if (Target == null)
                        return false;

                    if (Target.GetComponent<IHitAble>() != null)
                    {
                        return true;
                    }

                }
                return false;
            }
        }

        public IMoveStrategy GetMoveDict(ZombieMove move)
        {
            return moveDict[(int)move];
        }

        public IMoveStrategy MoveStrategy 
        { 
            get => moveStrategy;
            set { moveStrategy = value; }
        }

        protected void Awake()
        {
            hearComponent = GetComponent<HearComponent>();
            stateMachine = new StateMachine<Zombie>(this);
            detectiveCompo = GetComponent<DetectiveComponent>();
            zombieData = zombieData.GetClone;
        }

        public void Start()
        {
            InitTargetAction += hearComponent.InitTarget;
            InitTargetAction += detectiveCompo.InitTarget;
            InitTargetAction += () => { stateMachine.SetState((int)Zombie.ZombieState.IDLE);};
        }

        public new void OnEnable()
        {
            base.OnEnable();
            InitTargetAction();
        }

        public virtual void Move() 
        {
            moveStrategy.Move();
        }

        public GameObject GetAttacker()
        {
            return this.gameObject;
        }

        public abstract IAttackStrategy AttackStrategy { get; }
        public abstract float GetDamage();
        public abstract void Die();
        public abstract void Hit(float damage, IAttackAble attacker);
        public abstract void Hit(float damage);

    }



}
