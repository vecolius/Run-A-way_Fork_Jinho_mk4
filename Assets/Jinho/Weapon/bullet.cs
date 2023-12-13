using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;
using Jaeyoung;

public class Bullet : MonoBehaviour, Hojun.IAttackAble
{
    [SerializeField] float moveSpeed;
    public float damage;
    public WeaponData parentWeaponData = null;
    public Jinho.Player player = null;
    Action attackAction;

    Hojun.IHitAble target;

    void OnEnable()
    {
        Invoke("BulletDestroy", 1.2f);  //�Ѿ��� �ҷ������� 1.2�� �� ������ �ı���
    }
    void Start()
    {
        attackAction += BulletAttack;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    void BulletDestroy()    //�Ѿ��� ObjectPool�� ���ư�
    {
        PoolingManager.instance.ReturnPool(gameObject);
    }
    public void SetBulletData(WeaponData weaponData, Jinho.Player player)    //���� damage �Է� �Լ�
    {
        this.player = player;
        parentWeaponData = weaponData;
        damage = parentWeaponData.damage;
    }
    public void SetBulletVec(Transform firePos, Vector3 targetPos)  //Bullet�� ��ġ, ȸ��, ���Ⱚ ����
    {
        transform.position = firePos.position;
        transform.rotation = firePos.rotation;
        transform.forward = (targetPos - transform.position).normalized;
    }
    void BulletAttack()
    {
        if (other.gameObject.TryGetComponent<IHitAble>( out IHitAble hitObj))
        {
            target = hitObj;
            BulletDestroy();

        }
        if ( ( (1<<other.gameObject.layer) & LayerManager.instance.Nature) >= 1 )
        {   
            BulletDestroy();
        }
    }

    public GameObject GetAttacker()
    {
        return player.gameObject;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Hojun.IHitAble hit))
        {
            target = hit;
            Attack();
            BulletDestroy();
        }
    }
}
