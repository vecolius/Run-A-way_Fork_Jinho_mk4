using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hojun
{

    public class FindStrategy : IMoveStrategy
    {
        public GameObject Owner => ownerZombie.gameObject;
        Zombie ownerZombie;
        NavMeshAgent agent;


        public FindStrategy( Zombie owner )
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

            Debug.Log("findstrategy on");
            // 달리는 애니메이션 추가 할 것
            agent.SetDestination(ownerZombie.SoundTraceTarget.transform.position);
            agent.speed = ownerZombie.zombieData.Speed * 1.4f;

        }

    }
}