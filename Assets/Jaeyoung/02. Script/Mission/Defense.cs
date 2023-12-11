using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    public class Defense : Mission
    {
        [SerializeField] private float timeLimit;
        public float curTime;

        public override void Play()
        {
            if (curTime >= timeLimit)
            {
                MissionManager.instance.IsFinish = true;
                return;
            }

            curTime += Time.deltaTime;
            base.Play();
        }
    }
}
