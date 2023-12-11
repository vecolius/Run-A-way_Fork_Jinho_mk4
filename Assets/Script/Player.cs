using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojun;




public class Player : MonoBehaviour, IAttackAble, IHitAble, IDieable
{
    [SerializeField]
    CharacterData data;

    public CharacterData Data => throw new System.NotImplementedException();

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public GameObject GetAttacker()
    {
        throw new System.NotImplementedException();
    }

    public void Hit(float damage, IAttackAble attacker)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        //data = new CharacterData(hp,speed,atk);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
