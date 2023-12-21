using Gayoung;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yeseul;

namespace Jinho {
    public class ItemGrenade : WeaponMonoBehaviour, IAttackItemable, IInteractive
    {
        public Player Player 
        { 
            get => player; 
            set 
            { 
                player = value; 
                if(player != null)
                {
                    strategy = new GranadeAttackStrategy(player);
                }
            } 
        }
        Player player = null;
        public GameObject grenade;
        public float explosionRange;        //폭발 범위
        IAttackStrategy strategy;
        public ItemType ItemType { get => weaponData.itemType; }
        public IAttackStrategy AttackStrategy
        {
            get => strategy;
            set
            {
                strategy = value;
            }
        }
        Vector3 endPos, startPos;           //날아갈 위치
        
        
        void OnEnable()
        {
            strategy = new GranadeAttackStrategy(player);
        }
        public void Use()
        {
            
            if (BulletCount == 0)
                return;
            strategy.Attack();
        }

        public void SetItem(Player player)
        {
            SetWeapon(player, gameObject, 3, this);
        }

        public void UseEffect()
        {
            BulletCount--;

            endPos = player.Aim.aimObjPos.position;
            GameObject bulletObj = Instantiate(grenade);
            bulletObj.GetComponent<Grenade>().SetGrenadeData(transform.position, endPos, player, explosionRange, weaponData.damage);
        }

        public void Interaction(GameObject interactivePlayer)
        {
            if (interactivePlayer.TryGetComponent(out Player player) && this.player == null)
            {
                SetItem(player);
            }
        }

        public void Reloading()
        {
            return;
        }

        public void ReloadEffect()
        {
            return;
        }
    }
}
