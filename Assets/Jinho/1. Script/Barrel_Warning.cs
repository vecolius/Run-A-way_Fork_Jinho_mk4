using Hojun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Warning : MonoBehaviour
{
    ExplosionComponent explosion;
    void Start()
    {
        explosion = GetComponent<ExplosionComponent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IAttackAble attackAble))
        {
            explosion.Explosion(attackAble.GetAttacker());
        }
    }
}
