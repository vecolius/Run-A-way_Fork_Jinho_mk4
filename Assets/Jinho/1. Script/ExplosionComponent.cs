using Hojun;
using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionComponent : MonoBehaviour, IAttackAble
{
    public float explosionRange;        //폭발 범위
    public float damage;                //폭발 대미지
    public GameObject effectObj;        //폭발 이펙트
    public AudioClip effectSound;       //폭발 사운드
    public Jinho.Player player;
    Hojun.IHitAble target;
    public void Attack()
    {
        //target.Hit(GetDamage(), this);
    }
    public GameObject GetAttacker()
    {
        return player.gameObject;
    }
    public void Explosion(float damage, float explosionRange = 0, Jinho.Player player = null) //폭발
    {
        this.damage = damage;
        this.explosionRange = explosionRange;
        this.player = player;
        //이펙트 + 사운드 발생
        GameObject effectObj = PoolingManager.instance.PopObj(Jaeyoung.PoolingType.SOUND);
        //Destroy(effectObj);
        Destroy(gameObject, 6.0f);
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRange);
        if(cols.Length > 0)
        {
            foreach(var col in cols)
            {
                if (col.TryGetComponent(out Hojun.IHitAble hitable))
                {
                    target = hitable;
                    Attack();
                }
            }
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}
