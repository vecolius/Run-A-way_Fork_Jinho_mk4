using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Jaeyoung
{
    public class Breakthrough : Mission
    {
        public int groupCount;
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
            // 살아있는 인원수를 groupCount에 넣어줘야함
            groupCount = PhotonNetwork.CurrentRoom.PlayerCount;
        }

        public override bool Condition()
        {
            // 플레이어가 특정 위치에 인원수 만큼 
            return curCount == groupCount;
        }

        public override void Play()
        {
            base.Play();
        }
    }
}