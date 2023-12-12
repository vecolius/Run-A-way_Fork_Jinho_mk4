using Jinho;
using Hojun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public float damage;
    public WeaponData parentWeaponData = null;
    public Jinho.Player player = null;
    Action attackAction;

    IHitAble target;


    void OnEnable()
    {
        Invoke("BulletDestroy", 1.2f);  //총알이 불러와지면 1.2초 뒤 스스로 파괴됨
    }

    void Start()
    {
        //attackAction += BulletAttack;
    
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    void BulletDestroy()
    {
        Destroy(gameObject);
    }
    public void SetBulletData(WeaponData weaponData, Jinho.Player player)    //무기 damage 입력 함수
    {
        this.player = player;
        parentWeaponData = weaponData;
        damage = parentWeaponData.damage;
    }
    public void SetBulletVec(Transform firePos, Vector3 targetPos)
    {
        transform.position = firePos.position;
        transform.rotation = firePos.rotation;
        transform.forward = (targetPos - transform.position).normalized;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IHitAble>( out IHitAble hitObj))
        {
            BulletDestroy();
            target = hitObj;
        }
    }


    // 내가 짜긴 했는데, 뭔가 내가 생각한 거랑 좀 다르네.. 파이팅!! 진호야 넌 할 수 있어.
    //public void Attack()
    //{
    //    attackAction();
    //}

    //public void BulletAttack()
    //{
    //    target.Hit(damage, this);
    //}

    //public GameObject GetAttacker()
    //{
    //    Debug.Log("이거 플레이어 넘겨줘야 하는데 진호가 플레이어 수정해야 된다고 해서 내비 둠 진호야 해 . 줘");
    //    return null;
    //}

}
