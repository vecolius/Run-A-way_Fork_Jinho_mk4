using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    public class Defense : Mission
    {
        [SerializeField] private float timeLimit;
        private float curTime;
        public float CurTime
        {
            get { return timeLimit; }
            set
            { 
                timeLimit = value;
                // 변헀을 때 미션 설명 UI변경
            }
        }

        //private void Start()
        //{
        //    StartCoroutine(미션 코루틴);   
        //}
        // 연출이 나온 다음에 미션이 진행되도록 코루틴 짜야함
        // 없으면 바로 시작

        public override void Play()
        {
            base.Play();
        }

        public override bool Condition()
        {
            // 제한시간동안 버텼는가?
            return curTime >= timeLimit;
        }
    }
}
