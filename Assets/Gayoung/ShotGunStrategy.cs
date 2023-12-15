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

            if (Input.GetKeyDown(KeyCode.Alpha1))
                base.WeaponSwap(0);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                base.WeaponSwap(2);
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                base.WeaponSwap(3);
            // 실제로는 주무기는  Alpha1부분만 뺴고 가지고 있고 
            // 다른 부분들도 자신을 부르는 것만 빼고 작업하면 된다.
        }

        public void ReLoad()
        {
            player.animator.SetTrigger("Reload");
            player.animator.SetFloat("ReloadType", 0.6f);
        }
    }



}
