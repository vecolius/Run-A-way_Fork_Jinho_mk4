using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jinho;


namespace Gayoung
{
    public abstract class AttackStrategy : IAttackStrategy
    {
        protected Player player = null;
        protected KeyCode keycode;

        public AttackStrategy(object owner)
        {
            player = (Player)owner;
        }

        public virtual void Attack()
        {

        }
        public virtual void WeaponSwap(int index, float value = 0.5f)
        {
            player.animator.SetTrigger("WeaponChange");
            player.animator.SetFloat("WeaponChangeBlendTree", 0.5f);
            player.weaponIndex = index;
        }
        
        

    }



}



