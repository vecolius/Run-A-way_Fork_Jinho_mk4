using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yeseul
{
    public class MoveObj : MonoBehaviour
    {
        float moveSpeed = 7f;

        public float MoveSpeed
        {   
            get 
            { return moveSpeed; }
            set
            {
                moveSpeed = value;
                if (moveSpeed <= 0)
                    moveSpeed = 0;
            }    
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        private void OnTriggerStay(Collider other)
        {
            moveSpeed -= moveSpeed * Time.deltaTime;
        }
    }

}
