using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yeseul
{
    public class MoveFov : MonoBehaviour
    {
        public float sensitivity = 1.5f;      //시야 회전 감도
        Vector2 defaultAngle = Vector2.zero;  //카메라 기본 각도
        public float yAngle = 15f;            //상하 회전각
        //public float xAngle = 45f;          //좌우 회전각 (있어야하나?)

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            defaultAngle.x += mouseX * sensitivity;
            defaultAngle.y -= mouseY * sensitivity;
            defaultAngle.y = Mathf.Clamp(defaultAngle.y, -yAngle, yAngle); //상하 회전각도 제한
            //defaultAngle.x = Mathf.Clamp(defaultAngle.x, -xAngle, xAngle); //좌우 회전각도 제한

            transform.localRotation = Quaternion.AngleAxis(defaultAngle.x, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(defaultAngle.y, Vector3.right);
            transform.rotation = Quaternion.Euler(0, defaultAngle.x, 0);

        }
    }

}
