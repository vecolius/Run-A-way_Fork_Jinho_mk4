using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Goal : MonoBehaviourPun
{
    private void Start()
    {
        ((Mission)MissionManager.instance.curMission).clearEvent.AddListener(() => { this.gameObject.SetActive(false); });
    }

    private void OnTriggerEnter(Collider other)
    {
        photonView.RPC("GoalCountChange", RpcTarget.AllBuffered, 1);
    }

    private void OnTriggerExit(Collider other)
    {
        photonView.RPC("GoalCountChange", RpcTarget.AllBuffered, -1);
    }

    [PunRPC]
    private void GoalCountChange(int value)
    {
        ((Breakthrough)MissionManager.instance.curMission).CurCount += value;
    }
}
