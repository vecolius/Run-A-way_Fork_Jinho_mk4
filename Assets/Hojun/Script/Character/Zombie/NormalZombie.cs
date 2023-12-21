using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Hojun;
using UnityEditor;
using UnityEngine.AI;
using Jaeyoung;
using System;
using Unity.VisualScripting;
using JetBrains.Annotations;
using Photon.Pun;
using Photon;
using Photon.Realtime;



namespace Hojun 
{

    public class NormalZombie : Zombie 
    {

        public event Action dieAction;
        public event Action attackAction;

        public IAttackStrategy attackStrategy;
        public IHitStrategy hitStrategy;
        Animator animator;

        public override float Hp 
        {
            get =>base.Hp;
            set
            {
                if (value <= 0)
                    stateMachine.SetState( (int)Zombie.ZombieState.DEAD );
                
                base.Hp = value;
            }
        }

        public override IAttackStrategy AttackStrategy => attackStrategy;

        public new void Awake()
        {
        
            base.Awake();

            moveDict.Add(ZombieMove.SEARCH, new SearchStrategy(this));
            moveDict.Add(ZombieMove.IDLE, new IdleStrategy(this));
            moveDict.Add(ZombieMove.FIND, new FindStrategy(this));

            stateMachine.AddState((int)Zombie.ZombieState.IDLE, new IdleState(stateMachine));
            stateMachine.AddState((int)Zombie.ZombieState.SEARCH, new SearchState(stateMachine));
            stateMachine.AddState((int)Zombie.ZombieState.FIND , new FindState(stateMachine));
            stateMachine.AddState((int)Zombie.ZombieState.DEAD, new DeadState(stateMachine));
            stateMachine.AddState((int)Zombie.ZombieState.ATTACK, new AttackState(stateMachine));

            stateMachine.SetState((int)Zombie.ZombieState.IDLE);

            hearComponent = gameObject.GetComponent<HearComponent>();
            animator = GetComponent<Animator>();


            dieAction += () => { StartCoroutine(DieCo()); };

            //attackStrategy = new ZombieAttack();

        }



        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
        }


        [PunRPC]
        IEnumerator DieCo()
        {
            float deathTime = 3.0f;
            animator.SetInteger("State", (int)ZombieState.DEAD);
            yield return new WaitForSeconds(deathTime);

            Destroy(this.gameObject);
        }

        public override void Die()
        {
            dieAction();
        }

        public void OnTriggerEnter(Collider other)
        {
            
            if (other.TryGetComponent<IAttackAble>(out IAttackAble attack))
            {
                photonView.RPC("Hit", RpcTarget.All, attack.GetDamage());
            }

        }


        [PunRPC]
        public override void Hit(float damage, IAttackAble attacker)
        {
            Debug.Log("hit");
            Hp -= damage;
        }

        public override float GetDamage()
        {
            return attackStrategy.GetDamage();
        }

        [PunRPC]
        public override void Hit(float damage)
        {
            Debug.Log("hit");
            Hp -= damage;
        }
    }



}
