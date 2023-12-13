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
           
        }
        public void ReLoad()
        {
            throw new System.NotImplementedException();
        }
    }



}
