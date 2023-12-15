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
            // 애니메이션에서 무기종류에 따라 공격이 나가는 부분이다.
            player.animator.SetInteger("WeaponType", 2);

            // 무가 교체 부분이다.(애니메이션)
            if (Input.GetKeyDown(KeyCode.Alpha2))
                base.WeaponSwap(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                base.WeaponSwap(2);
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                base.WeaponSwap(3);
          
        }

        public void ReLoad()// 총의 총알을 재장전 하는 부분이다.
        {
            player.animator.SetTrigger("Reload");
            player.animator.SetFloat("ReloadType", 0.6f);
        }
    }

}
