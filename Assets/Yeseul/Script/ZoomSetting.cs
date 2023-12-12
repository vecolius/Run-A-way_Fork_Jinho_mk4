using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yeseul
{
    public class ZoomSetting : MonoBehaviour
    {
        public bool isZoom = false;                 //ZoomIn 여부
        public CinemachineVirtualCamera ZoomInCam;

        public void ZoomIn() //우클릭시 줌인, 우클릭 해제시 줌아웃
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (!isZoom)
                {
                    ZoomInCam.m_Priority = 11;     //ZoomInCam의 우선순위를 ZoomOutCam(10) 보다 높임
                    isZoom = true;
                }
            }
            else
                ZoomInCam.m_Priority = 9;          //ZoomInCam의 우선순위를 ZoomOutCam(10) 보다 낮춤
            isZoom = false;

        }
        // Start is called before the first frame update
        void Start()
        {
            ZoomInCam = gameObject.GetComponent<CinemachineVirtualCamera>();
        }

        // Update is called once per frame
        void Update()
        {
            ZoomIn();
        }
    }

}
