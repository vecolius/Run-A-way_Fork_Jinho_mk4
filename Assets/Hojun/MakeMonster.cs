using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMonster : MonoBehaviour
{

    public TestMonster2 monster;
    public TestMonster monster1;
    public Transform pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject temp;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            temp = Re_PoolManager.instance.PopPool(monster);
            temp.transform.position = pos.position;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            temp = Re_PoolManager.instance.PopPool(monster1);
            temp.transform.position = pos.position;
        }
    }
}
