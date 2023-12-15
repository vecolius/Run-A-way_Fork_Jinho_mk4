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
            
            // �ִϸ��̼ǿ��� ���������� ���� ������ ������ �κ��̴�.
            player.animator.SetInteger("WeaponType", 3);

            // ���� ��ü �κ��̴�.(�ִϸ��̼�)
            // ������ ���ⱳü �ִϸ��̼��� �޶� �̷��� �־����ϴ�.
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

        public void ReLoad() // ���� �Ѿ��� ������ �ϴ� �κ��̴�.
        {
            player.animator.SetTrigger("Reload");
            player.animator.SetFloat("ReloadType", 1f);
        }
    }

}
