using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Jaeyoung
{
    public class Search : Mission
    {
        [SerializeField] private GameObject targetItem;
        [SerializeField] private SpawnPoint spawnItemPoint;
        public int targetCount;
        [SerializeField] private int curCount;
        public int CurCount
        { 
            get { return curCount; }
            set 
            {
                curCount = value;
                // 변헀을 때 미션 설명 UI변경
            }
        }

        private void Start()
        {
            if (!PhotonNetwork.IsMasterClient)
                return;

            if(targetCount > spawnItemPoint.points.Count)
                targetCount = spawnItemPoint.points.Count;

            // 찾아야 하는 갯수만큼 생성(위치는 겹치지 않음)
            for (int i = 0; i < targetCount; i++)
            {
                int index = Random.Range(0, spawnItemPoint.points.Count);
                GameObject obj = PhotonNetwork.Instantiate(targetItem.name, spawnItemPoint.points[index].position, spawnItemPoint.points[index].rotation);
                spawnItemPoint.points.RemoveAt(index);
            }
        }

        public override void Play()
        {
            base.Play();
        }

        public override bool Condition()
        {
            // 아이템을 일정갯수 찾았는가?
            return curCount == targetCount;
        }
    }
}
