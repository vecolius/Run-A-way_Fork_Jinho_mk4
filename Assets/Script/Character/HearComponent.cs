using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    public class HearComponent : MonoBehaviour
    {
        // 추격, 인식 범위 (밸런스 수정시 값 수정)
        const float ChaseValue = 2.0f;
        const float DetectiveValue = 1.0f;

        public float Hear(GameObject target)
        {
            //float soundSize = target.GetComponent<TestBoom>().soundAreaSize;
            //float resultDistance = (soundSize - Vector3.Distance(transform.position, target.transform.position));

            return 0f;
        }
    }
}