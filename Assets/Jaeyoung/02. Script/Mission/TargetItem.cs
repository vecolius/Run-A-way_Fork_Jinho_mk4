using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TargetItem : MonoBehaviourPun, Yeseul.IInteractive
{
    public void Start()
    {
        ((Mission)MissionManager.instance.curMission).clearEvent.AddListener(() => { this.gameObject.SetActive(false); });
    }

    public void Interaction(GameObject interactivePlayer)
    {
        photonView.RPC("GetItem", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void GetItem()
    {
        ((Search)MissionManager.instance.curMission).CurCount++;
        gameObject.SetActive(false);
    }

    // 상호작용 테스트하고 지워야함
    private void OnTriggerEnter(Collider other)
    {
        photonView.RPC("GetItem", RpcTarget.AllBuffered);
    }
}
