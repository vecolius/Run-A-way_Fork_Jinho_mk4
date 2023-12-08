using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hojun
{

    public class IdleStrategy : IMoveStrategy
    {
        public GameObject Owner => owner.gameObject;
        Zombie owner;
        NavMeshAgent agent;


        public IdleStrategy( Zombie owner )
        {
            this.owner = owner;
            agent = owner.GetComponent<NavMeshAgent>();
            if (null == agent)
            {
                Debug.Log("agent is null, check this error");
            }
        }

        public void Move()
        {
            Debug.Log("idleStrategy on 아무것도 안함");
        }

    }

}
