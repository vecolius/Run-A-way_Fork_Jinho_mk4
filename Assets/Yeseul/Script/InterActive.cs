using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yeseul
{

    public class InterActive : MonoBehaviour
    {
        // 피의자

        public float range = 2f; // 상호작용 대상 탐지범위

        GameObject FindNearestObj(Collider[] cols) // 가까운 인터페이스 소유대상 체크 
        {

            GameObject nearestObj = null;

            float leastDistance = Mathf.Infinity;

            foreach (Collider itemCol in cols)
            {
                if (itemCol.TryGetComponent(out IInteractive inter))    //IInteractive 인터페이스를 가졌으면 거리체크
                {
                    float distance = Vector3.Distance(transform.position, itemCol.transform.position);

                    if (distance < leastDistance)
                    {
                        leastDistance = distance;
                        nearestObj = itemCol.gameObject;
                    }
                }
            }

            return nearestObj;
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, range);
                
                if (cols.Length != 0)
                {
                    IInteractive interactiveObj = FindNearestObj(cols).GetComponent<IInteractive>();
                    interactiveObj?.Interaction(this.gameObject);
                }
            }
        }


    }
}
