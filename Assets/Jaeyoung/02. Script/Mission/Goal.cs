using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Player")
        //    MissionManager.instance.IsFinish = true;

        gameObject.SetActive(false);
    }
}
