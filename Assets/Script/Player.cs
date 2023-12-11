using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojun;




public class Player : MonoBehaviour, IAttackAble, IHitAble, IDieable
{
    [SerializeField]
    CharacterData data;

    public CharacterData Data => throw new System.NotImplementedException();

    int killCount;
    public int KIllCount
    {
        get { return killCount; }
        set { killCount = value; }
    }

    public float Hp
    {
        get { return data.hp; }
        set
        { 
            data.hp = value; 
            if(data.hp <= 0)
            {
                Die();
            }
        }
    }


    public void Attack()
    {
        GameObject target = GetAttacker(); 
    }

    public void Die()
    {
        //델리게이트를 쓰자. 뭘할지 모르니까. 
        //죽으면 뭐함?
        //(싱글)이면 게임오버
        //(멀티)이면 캠 전환? 
        //애니메이션
    }

    public GameObject GetAttacker()
    {
        GameObject enemy = null;
        return enemy;
    }

    public void Hit(float damage, IAttackAble attacker) 
    {
        Hp -= damage; 
        //+ 피격 애니메이션 
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
