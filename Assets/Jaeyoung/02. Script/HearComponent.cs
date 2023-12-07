using Hojun;
using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Jaeyoung
{
    public class HearComponent : MonoBehaviour , IHearAble
    {
        // 추격, 인식 범위 (밸런스 수정시 값 수정)
        const float ChaseValue = 2.0f;
        const float DetectiveValue = 1.0f;

        void IHearAble.Hear(GameObject soundOwner)
        {
            float soundSize = soundOwner.GetComponent<SoundComponent>().soundAreaSize;
            float resultDistance = (soundSize - Vector3.Distance(transform.position, soundOwner.transform.position));

            if(TryGetComponent<Zombie>( out  Zombie owner)) 
            {
                owner.hearValue = resultDistance;
            }


        }
    }
}