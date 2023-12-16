using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


namespace Hojun 
{

    public interface IMoveStrategy 
    {
        public GameObject Owner { get;}
        public void Move();
    }

    public interface IMoveAble
    {
        public IMoveStrategy MoveStrategy { get; set; }
        public void Move();
    }

    public interface IHearAble
    {
        public void Hear(GameObject soundOwner);
    }

    public interface IHitStrategy
    {
        public void Hit(float damage , IAttackAble attacker);
    }

    public interface IHitAble
    {
        [PunRPC]
        public void Hit(float damage , IAttackAble attacker);

        public CharacterData Data 
        {
            get;
        }

    }

    public interface IAttackStrategy
    {
        public float GetDamage();
        public void Attack();
    }

    public interface IAttackAble 
    {
        public float GetDamage();
        public GameObject GetAttacker();
    }


    public interface IDieable
    {
        public void Die();
    }


}
