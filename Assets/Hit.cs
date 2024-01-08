using Hojun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour , IHitAble
{
    public CharacterData Data => throw new System.NotImplementedException();

    void IHitAble.Hit(float damage, IAttackAble attacker)
    {
        Debug.Log("hit");
    }

    void IHitAble.Hit(float damage)
    {
        Debug.Log("hit");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
