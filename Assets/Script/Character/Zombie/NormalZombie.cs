using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojun;
using UnityEngine.UIElements;

public class NormalZombie : Zombie , IMoveAble
{

    


    public new void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine<Zombie>(this);
        stateMachine.AddState(MoveState.IDLE , new IdleState( stateMachine ) );
        
    
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
}
