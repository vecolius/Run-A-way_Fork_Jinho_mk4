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

            if (Input.GetKeyDown(KeyCode.Alpha1))
                base.WeaponSwap(0);
        }

        public void ReLoad()
        {
            player.animator.SetTrigger("Reload");
            player.animator.SetFloat("ReloadType", 0.3f);
        }
    }




}
