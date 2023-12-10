using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionComponent : MonoBehaviour
{
    public float explosionRange;        //Æø¹ß ¹üÀ§
    public float damage;                //Æø¹ß ´ë¹ÌÁö
    public void Explosion(float damage, float explosionRange = 0) //Æø¹ß
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRange);
        //ÀÌÆåÆ® + »ç¿îµå ¹ß»ý

        if(cols.Length > 0)
        {
            foreach(var col in cols)
            {
                //if(col.TryGetComponent(out IHitable hitable)) hitable.Hit(damage);
            }
        }
    }
}
