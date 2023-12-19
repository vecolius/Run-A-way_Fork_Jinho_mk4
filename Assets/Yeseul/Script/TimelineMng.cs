using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using Cinemachine;

namespace Yeseul
{
    public class TimelineMng : MonoBehaviour
    {
        private PlayableDirector playableDirector;  //메인카메라에 Playable Director 넣어야함. 플레이어 프리펩 수정필요
        public CinemachineBrain cinemachineBrain;   //메인카메라에 달려있음

        public float defaultBlendValue = 2;         //타임라인 재생시 전환속도
        public float newBlendValue = 0.2f;          //세팅할 값 (조준시 씬 전환 속도)

        public Queue<TimelineAsset> timelines;      //타임라인 담을 큐. 그냥 gameObject로 받아서 파고 들어서 실행시킬까? 
        private void Start()
        {
            playableDirector = GetComponent<PlayableDirector>();
            playableDirector.stopped += TimelineStopped;

            timelines = new Queue<TimelineAsset>();
            timelines.Enqueue(Resources.Load("TimeLine1") as TimelineAsset); //나중에 Resources 폴더에 넣어야함. 스타트타임까지 넣어야하나? 
            timelines.Enqueue(Resources.Load("TimeLine2") as TimelineAsset);
            timelines.Enqueue(Resources.Load("TimeLine3") as TimelineAsset);
        }

        private void TimelineStopped(PlayableDirector director) //타임라인 끝날때 호출
        {
            Debug.Log("타임라인 끝! state: " + playableDirector.state);

            // 타임라인 재생이 끝났을 때 브레인의 블렌딩 값 변경
            CinemachineBlendDefinition customBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, newBlendValue);
            cinemachineBrain.m_DefaultBlend = customBlend;

        }

        public void TimelinePlay()   //세팅하고 재생
        {
            CinemachineBlendDefinition customBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, defaultBlendValue);
            cinemachineBrain.m_DefaultBlend = customBlend;
            playableDirector.playableAsset = timelines.Dequeue();
            playableDirector.Play();
        }
    }

}
