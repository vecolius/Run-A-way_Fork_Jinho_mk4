using Hojun;
using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionComponent : MonoBehaviour, IAttackAble
{
    public float explosionRange;        //Æø¹ß ¹üÀ§
    public float damage;                //Æø¹ß ´ë¹ÌÁö
    public GameObject effectObj;        //Æø¹ß ÀÌÆåÆ®
    public AudioClip effectSound;       //Æø¹ß »ç¿îµå
    public Jinho.Player player;
    Hojun.IHitAble target;
    public void Attack()
    {
        target.Hit(damage, this);
    }
    public GameObject GetAttacker()
    {
        return player.gameObject;
    }
    public void Explosion(float damage, float explosionRange = 0, Jinho.Player player = null) //Æø¹ß
    {
        this.damage = damage;
        this.explosionRange = explosionRange;
        this.player = player;
        //ÀÌÆåÆ® + »ç¿îµå ¹ß»ý
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
        throw new System.NotImplementedException();
    }
}
