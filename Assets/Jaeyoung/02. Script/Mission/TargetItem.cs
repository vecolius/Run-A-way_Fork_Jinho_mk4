using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetItem : MonoBehaviour, Yeseul.IInteractive
{
    public void Start()
    {
        ((Mission)MissionManager.instance.curMission).clearEvent.AddListener(() => { this.gameObject.SetActive(false); });
    }

    public void Interaction(GameObject interactivePlayer)
    {
        ((Search)MissionManager.instance.curMission).CurCount++;
        gameObject.SetActive(false);
    }

    // 상호작용 테스트하고 지워야함
    private void OnTriggerEnter(Collider other)
    {
        Interaction(other.gameObject);
    }
}
