using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveComponent : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float searchDistance;
    [SerializeField] private float attackDistance;
    [SerializeField] private LayerMask target;


    public Transform targetObj;
    public bool IsFind 
    {
        get 
        {
            return isFind;
        }
    }
    bool isFind;

    public bool IsAttack 
    {
        get => isAttack;
    }
    bool isAttack;
    
    Vector3 direction;


    void Update()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, radius, target);

        Debug.Log(coll.Length);

        if (coll.Length > 0)
        {

            targetObj = coll[0].transform;
            direction = ( targetObj.position - transform.position ).normalized;

            // search raycast
            if ( Physics.Raycast(transform.position, direction ,searchDistance, (int)target))
            {
                isFind = true;
            }
            else { isFind = false; }

            // attack distance check raycast

            if (Physics.Raycast(transform.position, direction, attackDistance, (int)target))
            {
                isAttack = true;
            }
            else 
            {
                isAttack = false; 
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
