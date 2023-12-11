using Hojun;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hojun
{

    public class SearchStrategy : IMoveStrategy
    {

        public GameObject Owner => ownerZombie.gameObject;
        Zombie ownerZombie;
        NavMeshAgent agent;

        public SearchStrategy(Zombie owner)
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

            Debug.Log("searchstrategy on 찾기 시작");

            agent.SetDestination(ownerZombie.SoundTraceArea);


            agent.speed = 10f;
            if (ownerZombie.HearValue <= ownerZombie.RunHearValue)
            {
                Debug.Log("달리는 중");
                agent.speed = ownerZombie.Speed * 1.4f;
            }


        }

    }
}