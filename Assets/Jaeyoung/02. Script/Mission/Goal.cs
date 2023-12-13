using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void Start()
    {
        ((Mission)MissionManager.instance.curMission).clearEvent.AddListener(() => { this.gameObject.SetActive(false); });
    }

    private void OnTriggerEnter(Collider other)
    {
        ((Breakthrough)MissionManager.instance.curMission).CurCount++;
    }

    private void OnTriggerExit(Collider other)
    {
        ((Breakthrough)MissionManager.instance.curMission).CurCount--;
    }
}
