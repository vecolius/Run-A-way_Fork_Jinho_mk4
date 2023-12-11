using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    public class Defense : Mission
    {
        [SerializeField] private float timeLimit;
        public float curTime;

        public void Start()
        {
            // 이런 느낌으로 다른 미션도 수정해줘야함
            MissionManager.instance.condition = () => { return curTime >= timeLimit; };
        }

        public override void Play()
        {
            curTime += Time.deltaTime;
            base.Play();
        }
    }
}
