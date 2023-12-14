using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jinho;

namespace Gayoung 
{
    public class GranadeAttackStrategy : AttackStrategy , IReLoadAble
    {
        public GranadeAttackStrategy(object owner) : base(owner)
        {
        }
        public override void Attack()
        {
            // 공격 관련 애니메이션 연결하기


            // 무가 교체 부분이다.(애니메이션)
            if (Input.GetKeyDown(KeyCode.Alpha1))
                base.WeaponSwap(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                base.WeaponSwap(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                base.WeaponSwap(2);

        }
        public void ReLoad()
        {
            //수류탄 애니메이션을 찾아서 쓸지 결정하자!
            throw new System.NotImplementedException();
        }
    }



}
