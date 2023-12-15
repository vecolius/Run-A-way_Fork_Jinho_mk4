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

            // 12-13 일 수정해야 하는 부분
            player.animator.SetInteger("WeaponType", 3);

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                base.WeaponSwap(2, 1.0f);
                // 권총은 모션이 달라서 이렇게 넣어보았습니다! = 가영
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                base.WeaponSwap(0, 1.0f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                base.WeaponSwap(3, 1.0f);
            }
        }   

        public void ReLoad()
        {
            player.animator.SetTrigger("Reload");
            player.animator.SetFloat("ReloadType", 1f);
        }
    }


}
