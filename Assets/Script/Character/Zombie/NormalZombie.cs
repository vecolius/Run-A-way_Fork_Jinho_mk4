using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Hojun;
using UnityEditor;


namespace Hojun 
{


    public class NormalZombie : Zombie
    {
        Vector3 myvec;

        public new void Awake()
        {
            myvec = gameObject.transform.localPosition;

            base.Awake();
            stateMachine = new StateMachine<Zombie>(this);
            stateMachine.AddState(Zombie.ZombieState.IDLE, new IdleState(stateMachine));
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update();
        }

        public override void Move()
        {

        }


    }



}
