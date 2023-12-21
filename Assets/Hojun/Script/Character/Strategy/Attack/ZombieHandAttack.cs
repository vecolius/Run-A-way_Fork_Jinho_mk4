using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hojun
{


    public class ZombieHandAttack : MonoBehaviour , IAttackAble
    {
        [SerializeField] CharacterData data;
        public GameObject GetAttacker()
        {
            return gameObject;
        }

        public float GetDamage()
        {
            return data.attackPoint ;
        }
    }
}