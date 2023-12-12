using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    public class MissionManager : MonoBehaviour
    {
        public static MissionManager instance;
        public Queue<IMissionable> missionQueue = new Queue<IMissionable>();
        public List<Mission> missionList = new List<Mission>();
        public Func<bool> condition;
        private IMissionable curMission;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);

            foreach(Mission mission in missionList)
            {
                missionQueue.Enqueue(mission);
            }

            TakeMission();
        }

        private void Update()
        {
            if(condition())
                TakeMission();

            curMission?.Play();
        }

        private void TakeMission()
        {
            //IsFinish = false;
            ((Mission)curMission)?.gameObject.SetActive(false);

            if (missionQueue.TryDequeue(out IMissionable mission))
            {
                curMission = mission;
                ((Mission)curMission).gameObject.SetActive(true);
            }
            else
                MissionOver();
        }

        public void MissionOver()
        {
            Debug.Log("엔딩");
            // 엔딩
        }
    }
}