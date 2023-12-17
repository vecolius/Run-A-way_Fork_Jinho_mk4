using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gayoung;
using static UnityEngine.UI.GridLayoutGroup;

namespace Gayoung 
{
    public class RifleAttackStrategy : AttackStrategy , IReLoadAble
    {
        public RifleAttackStrategy(object owner) : base(owner)
        {

        }
        public override void Attack()
        {
            //멀티일 때 오류날 수 도 있을거 같다
            if (player == null)
                return;
            Debug.Log("라이플 모션");
            //player.WeaponIndex = 0;
            // 애니메이션에서 무기종류에 따라 공격이 나가는 부분이다.
            player.animator.SetInteger("WeaponType", 1);

            // 무가 교체 부분이다.(애니메이션)
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                base.WeaponSwap(1);
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



        public void ReLoad()// 총의 총알을 재장전 하는 부분이다.
        {
            player.animator.SetTrigger("Reload");
            player.animator.SetFloat("ReloadType", 0.3f);
        }
    }

}
