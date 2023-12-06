using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojun;
using UnityEngine.UIElements;

public class NormalZombie : Zombie , IMoveAble
{

<<<<<<< Updated upstream
    


    public new void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<Zombie>(this);
        stateMachine.AddState(MoveState.IDLE , new IdleState( stateMachine ) );
        
    
    }

    // Start is called before the first frame update
    void Start()
    {
        
=======
    public class NormalZombie : Zombie
    {

        public new void Awake()
        {
            base.Awake();
            stateMachine = new StateMachine<Zombie>(this);
            // stateMachine.AddState(MoveState.IDLE, new IdleState(stateMachine));
            

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
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
