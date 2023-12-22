using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    public class MissionManager : MonoBehaviour
    {
        public static MissionManager instance;
        public IMissionable curMission;
        public Func<bool> condition;
        public bool isEnding = false;

        [SerializeField] private List<Mission> missionList = new List<Mission>();
        private Queue<IMissionable> missionQueue = new Queue<IMissionable>();

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

            condition = () => { return false; };
        }

        private void Update()
        {
            if (condition())
                ((Mission)curMission).clearEvent.Invoke();

            curMission?.Play();
        }

        public void TakeMission()
        {
            ((Mission)curMission)?.gameObject.SetActive(false);
            ((Mission)curMission)?.clearEvent.Invoke();

            if (missionQueue.TryDequeue(out IMissionable mission))
            {
                curMission = mission;
                condition = curMission.Condition;
                ((Mission)curMission).gameObject.SetActive(true);
                UIManager.instance.MissionUpdate(((Mission)curMission).missiontName, ((Mission)curMission).missiontInfo);
            }
            else
                MissionOver();
        }

        public void MissionOver()
        {
            Debug.Log("엔딩");
            isEnding = true;
            this.enabled = false;
            // 엔딩
        }
    }
}