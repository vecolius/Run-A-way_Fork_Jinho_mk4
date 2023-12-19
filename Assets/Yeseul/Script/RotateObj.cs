using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yeseul
{
    public class RotateObj : MonoBehaviour
    {
        public float rotSpeed = 500f; 

        void Update()
        {
            transform.Rotate(Vector3.right, rotSpeed * Time.deltaTime);
        }
    }

}
