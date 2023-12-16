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
            if (player == null)
                return;

            //player.WeaponIndex = 1;
            // �ִϸ��̼ǿ��� ���������� ���� ������ ������ �κ��̴�.
            player.animator.SetInteger("WeaponType", 3);

            // ���� ��ü �κ��̴�.(�ִϸ��̼�)
            // ������ ���ⱳü �ִϸ��̼��� �޶� �̷��� �־����ϴ�.
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                base.WeaponSwap(0, 1.0f);
                player.WeaponChange();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                base.WeaponSwap(2, 1.0f);
                player.WeaponChange();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                base.WeaponSwap(3, 1.0f);
                player.WeaponChange();
            }
        }   

        public void ReLoad() // ���� �Ѿ��� ������ �ϴ� �κ��̴�.
        {
            player.animator.SetTrigger("Reload");
            player.animator.SetFloat("ReloadType", 1f);
        }
    }

}
