using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    private Vector3 aimPos;
    const float aimHitMaxRange = 50f;   //최대 사거리
    public GameObject testAimObj;
    GameObject aimObj;
    public Transform aimObjPos { get => aimObj.transform; }
    private void Start()
    {
        aimObj = Instantiate(testAimObj);
    }
    void Update()
    {
        RaycastHit aimHit;
        Debug.DrawLine(transform.position, transform.forward * aimHitMaxRange, Color.green);
        if (Physics.Raycast(transform.position, transform.forward, out aimHit, aimHitMaxRange))
        {
            aimPos = aimHit.point;
        }
        else
        {
            aimPos = this.transform.position + transform.forward.normalized * 30f;
        }
        aimObj.transform.position = aimPos;
    }
}
