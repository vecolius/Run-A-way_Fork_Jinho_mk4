using Hojun;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchStrategy : IMoveStrategy
{

    public GameObject Owner => owner.gameObject;
    Zombie owner;
    NavMeshAgent agent;

    public SearchStrategy(Zombie owner)
    {
        this.owner = owner;

        agent = owner.GetComponent<NavMeshAgent>();
        if (null == agent) 
        {
            Debug.Log("agent is null, check this error");
        }
        
    }

    public void Move(GameObject target)
    {
        agent.SetDestination(target.transform.position);
        Debug.Log("target class");
    }

    public void Move(Vector3 target)
    {
        agent.SetDestination(target);
        float temp = Vector3.Distance(owner.transform.position, target);

        Debug.Log("target vector3");
    }

}
