using Hojun;
using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

namespace Jaeyoung
{
    public class HearComponent : MonoBehaviour , IHearAble
    {
        // 추격, 인식 범위 (밸런스 수정시 값 수정)
        const float ChaseValue = 2.0f;
        const float DetectiveValue = 1.0f;

        float resultDistance;
        GameObject soundOwner;
        Vector3 soundArea;

        float hp;
        Text text;

        void ChangeHP(float value)
        {
            hp += value;
            text.text = hp.ToString();
        }


        public GameObject SoundOwner 
        {
            get 
            {
                return soundOwner;
            }
            set
            {
                soundOwner = value;
                soundArea = value.transform.position;
            }
        }
        
        public Vector3 SoundArea 
        {
            get { return soundArea; }
        }

        public float ResultDistance 
        {
            get
            {
                return resultDistance;
            }    
        }

        public void Hear(GameObject soundOwner)
        {
            float soundSize = soundOwner.GetComponent<SoundComponent>().soundAreaSize;
            resultDistance = (soundSize - Vector3.Distance(transform.position, soundOwner.transform.position));
            SoundOwner = soundOwner;
        }
    }
}