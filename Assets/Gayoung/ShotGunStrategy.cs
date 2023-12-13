using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gayoung;

namespace Gayoung 
{
    public class ShotGunStregy : AttackStrategy , IReLoadAble
    {
        public ShotGunStregy(object owner) : base(owner)
        {

        }

        public override void Attack()
        {

            player.animator.SetInteger("WeaponType", 2);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                base.WeaponSwap(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                base.WeaponSwap(2);
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                base.WeaponSwap(3);
        }

        public void ReLoad()
        {
            player.animator.SetTrigger("Reload");
            player.animator.SetFloat("ReloadType", 0.6f);
        }
    }



}
