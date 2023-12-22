using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSkip : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            MissionManager.instance.TakeMission();
    }
}
