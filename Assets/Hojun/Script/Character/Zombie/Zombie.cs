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

        protected Action initTargetAction;

        DetectiveComponent detectiveCompo;

        [SerializeField]
        private CharacterData zombieData;

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

        public abstract IAttackStrategy AttackStrategy { get; }

        IMoveStrategy moveStrategy;

        protected void Awake()
        {
            

            hearComponent = GetComponent<HearComponent>();
            //zombieData = new CharacterData(50,10,20);
            stateMachine = new StateMachine<Zombie>(this);
            detectiveCompo = GetComponent<DetectiveComponent>();

            zombieData = zombieData.GetClone;
        }

        public virtual void Move() 
        {
            moveStrategy.Move();
        }

        public void Start()
        {
            initTargetAction = hearComponent.InitTarget;
        }

        public abstract void Die();
        public abstract void Hit(float damage, IAttackAble attacker);

        public GameObject GetAttacker()
        {
            throw new System.NotImplementedException();
        }

        public abstract float GetDamage();

        public void InitTarget()
        {
            initTargetAction();
        }

        public abstract void Hit(float damage);
    }



}
