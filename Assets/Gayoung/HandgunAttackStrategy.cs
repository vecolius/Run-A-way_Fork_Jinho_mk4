using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jinho;

namespace Gayoung
{
    public class HandgunAttackStrategy : AttackStrategy, IReLoadAble
    {
    
        public HandgunAttackStrategy(object owner) : base(owner)
        {

        }
        public override void Attack()
        {
            
            // 애니메이션에서 무기종류에 따라 공격이 나가는 부분이다.
            player.animator.SetInteger("WeaponType", 3);

            // 무가 교체 부분이다.(애니메이션)
            // 권총은 무기교체 애니메이션이 달라서 이렇게 넣었습니다.
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                base.WeaponSwap(1, 1.0f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                base.WeaponSwap(2, 1.0f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                base.WeaponSwap(3, 1.0f);
            }

        }

        public void ReLoad() // 총의 총알을 재장전 하는 부분이다.
        {
            player.animator.SetTrigger("Reload");
            player.animator.SetFloat("ReloadType", 1f);
        }
    }

}
