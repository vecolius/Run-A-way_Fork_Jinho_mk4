using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gayoung;

namespace Gayoung 
{
    public class RifleAttackStrategy : AttackStrategy , IReLoadAble
    {
        public RifleAttackStrategy(object owner) : base(owner)
        {

        }
        public override void Attack()
        { 
            player.animator.SetInteger("WeaponType", 1);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                base.WeaponSwap(0);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                base.WeaponSwap(2);
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                base.WeaponSwap(3);
        }

        public void ReLoad()
        {
            player.animator.SetTrigger("Reload");
            player.animator.SetFloat("ReloadType", 0.3f);
        }
    }




}
