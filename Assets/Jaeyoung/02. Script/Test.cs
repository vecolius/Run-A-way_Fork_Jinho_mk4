using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            EventManager.instance.TakeMission(MissionType.Defense);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            EventManager.instance.TakeMission(MissionType.Search);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            EventManager.instance.TakeMission(MissionType.Breakthrough);
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            EventManager.instance.MissionOver();   
    }
}
