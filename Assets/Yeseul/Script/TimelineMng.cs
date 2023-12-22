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
        private PlayableDirector playableDirector;  //����ī�޶� Playable Director �־����. �÷��̾� ������ �����ʿ�
        public CinemachineBrain cinemachineBrain;   //����ī�޶� �޷�����

        public float defaultBlendValue = 2;         //Ÿ�Ӷ��� ����� ��ȯ�ӵ�
        public float newBlendValue = 0.2f;          //������ �� (���ؽ� �� ��ȯ �ӵ�)

        public Queue<TimelineAsset> timelines;      //Ÿ�Ӷ��� ���� ť. �׳� gameObject�� �޾Ƽ� �İ� �� �����ų��? 
        public Queue<GameObject> tlObjs;

        private void Start()
        {
            playableDirector = GetComponent<PlayableDirector>();
            playableDirector.stopped += TimelineStopped;

            ////timelines = new Queue<TimelineAsset>();
            ////timelines.Enqueue(Resources.Load("TimeLine1") as TimelineAsset); //���߿� Resources ������ �־����. ��ŸƮŸ�ӱ��� �־���ϳ�? 
            ////timelines.Enqueue(Resources.Load("TimeLine2") as TimelineAsset);
            ////timelines.Enqueue(Resources.Load("TimeLine3") as TimelineAsset);
        }

        private void TimelineStopped(PlayableDirector director) //Ÿ�Ӷ��� ������ ȣ��
        {
            Debug.Log("Ÿ�Ӷ��� ��! state: " + playableDirector.state);

            // Ÿ�Ӷ��� ����� ������ �� �극���� ������ �� ����
            CinemachineBlendDefinition customBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, newBlendValue);
            cinemachineBrain.m_DefaultBlend = customBlend;

        }

        public void TimelinePlay()   //�����ϰ� ���
        {
            CinemachineBlendDefinition customBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, defaultBlendValue);
            cinemachineBrain.m_DefaultBlend = customBlend;
            //playableDirector.playableAsset = timelines.Dequeue();
            //playableDirector.Play();
        }
    }

}
