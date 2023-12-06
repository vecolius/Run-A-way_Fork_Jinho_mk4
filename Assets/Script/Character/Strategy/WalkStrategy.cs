using Hojun;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkStrategy : IMoveStrategy
{


    public GameObject Owner => owner.gameObject;
    
    Zombie owner;
    

    public WalkStrategy(Zombie owner)
    {
        this.owner = owner;
    }


    public void Move(GameObject target)
    {

        //TODO_LIST navimeshAgent 사용해서 target의 위치로 갈 것 걷는 거니까 속도  *0.7 정도 하면 좋을 듯 
        owner.transform.Translate( owner.transform.forward * owner.Data.Speed);
        
    }



}
