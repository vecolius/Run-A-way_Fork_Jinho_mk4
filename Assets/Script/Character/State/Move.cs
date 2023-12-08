using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hojnun
{
    public class Move : MonoBehaviour
    {


        public NavMeshAgent agent;
        public GameObject target;


        // Start is called before the first frame update
        void Start()
        {
            agent.SetDestination(target.transform.position);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}