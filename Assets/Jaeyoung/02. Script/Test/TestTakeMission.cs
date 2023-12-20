using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTakeMission : MonoBehaviour
{
    private void Start()
    {
        Jaeyoung.MissionManager.instance.TakeMission();
    }
}
