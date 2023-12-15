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
            Debug.Log("getdamage call �̰� ���� �Ұ� return �� ����� ������ �Ⱥ���");
            return 10f;
        }
    }
}