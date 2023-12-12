using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Jaeyoung
{
    public class FollowComponent : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        private NavMeshAgent agent;
        [SerializeField] private float distance; // 타겟과의 거리를 얼마나 둘지 설정

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            agent.SetDestination(target.transform.position);

            if (agent.remainingDistance < distance)
                agent.isStopped = true;
            else
                agent.isStopped = false;
        }
    }
}