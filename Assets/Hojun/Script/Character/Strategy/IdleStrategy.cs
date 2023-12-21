using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hojun
{

    public class IdleStrategy : IMoveStrategy
    {
        public GameObject Owner => ownerZombie.gameObject;
        Zombie ownerZombie;
        NavMeshAgent agent;

        Vector3 zombieForward = new Vector3(0.0001f,0,0);
        public IdleStrategy( Zombie owner )
        {
            this.ownerZombie = owner;
            agent = owner.GetComponent<NavMeshAgent>();
            if (null == agent)
            {
                Debug.Log("agent is null, check this error");
            }
        }

        public void Move()
        {
            
            agent.SetDestination(ownerZombie.transform.position + zombieForward);
        }

    }

}
