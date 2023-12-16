using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jinho;

namespace Gayoung
{
    public class MeleeAttackStrategy : AttackStrategy
    {
        public MeleeAttackStrategy(object owner) : base(owner)
        {
        }
        public override void Attack()
        {
            if (player == null)
                return;
           // player.WeaponIndex = 1;
            // 공격 관련 애니메이션 연결하기
            player.animator.SetInteger("WeaponType", 4);

            // 무가 교체 부분이다.(애니메이션)
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                base.WeaponSwap(0);
                player.WeaponChange();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                base.WeaponSwap(2);
                player.WeaponChange();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            { 
                base.WeaponSwap(3);
                player.WeaponChange();
            }

        }

    }

}