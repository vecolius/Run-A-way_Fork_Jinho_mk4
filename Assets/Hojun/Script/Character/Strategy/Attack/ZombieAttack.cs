using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hojun
{

    public class ZombieAttack : IAttackStrategy
    {

        public void Attack()
        {
            throw new System.NotImplementedException();
        }


        public float GetDamage()
        {
            Debug.Log("getdamage call 이거 수정 할것 return 값 상수임 데미지 안변함");
            return 10f;
        }
    }
}