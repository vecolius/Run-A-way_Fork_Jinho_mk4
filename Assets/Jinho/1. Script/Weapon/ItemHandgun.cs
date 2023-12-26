using Gayoung;
using Jaeyoung;
using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Jinho
{
    public class ItemHandgun : WeaponMonoBehaviour, IAttackItemable, Yeseul.IInteractive , IReLoadAble
    {
        public Player Player 
        { 
            get => player; 
            set 
            { 
                player = value; 
                if(player != null)
                {
                    strategy = new HandgunAttackStrategy(player);
                }
            } 
        }
        public Player player = null;

        public Transform firePos;   //총알 발사 위치
        public Collider weaponCol;
        Transform aimPos;           //총알이 날아갈 위치
        public AudioClip gunFireSound;
        public AudioClip reloadSound;
        public GameObject fireEffect;

        IAttackStrategy strategy;
        public ItemType ItemType => weaponData.itemType;
        public IAttackStrategy AttackStrategy
        {
            get => strategy;
            set
            {
                strategy = value;
            }
        }

        public SoundComponent sound;

        void OnEnable()
        {
            strategy = new HandgunAttackStrategy(player);
        }

        [PunRPC]
        public void Use()
        {
            
            if (BulletCount <= 0)
                return;

            player.animator.SetBool("Shot", true);
            strategy.Attack();
        }


        public void SetItem(Player player)
        {
            SetWeapon(player, gameObject, 1, this);
        }


        public void Interaction(GameObject interactivePlayer)
        {
            if (interactivePlayer.TryGetComponent(out Player player) && this.player == null)
            {
                SetItem(player);
            }
        }
        [PunRPC]
        public void MakeBullet()
        {
            BulletCount--;
            // make bullet -> obj_pull

            aimPos = player.Aim.aimObjPos;
            GameObject bulletObj = PoolingManager.instance.PopObj(PoolingType.BULLET);
            Bullet_Component bulletScript = bulletObj.GetComponent<Bullet_Component>();
            bulletObj.SetActive(true);
            bulletScript.SetBulletData(weaponData, Player);
            bulletScript.SetBulletVec(firePos, aimPos.position);

            this.sound.ActiveSound();
            SoundEffect(gunFireSound, firePos, 25);
        }

        public void UseEffect()
        {
            if (BulletCount <= 0)
                return;
            InstantiateEffect(fireEffect, firePos);
            MakeBullet();
        }

        public void Reloading()    //근접무기는 재장전 없음
        {
            if (strategy is IReLoadAble)
                ((IReLoadAble)strategy).ReLoad();
            SoundEffect(reloadSound, transform);
        }
        public void ReloadEffect()
        {
            ReLoad();
        }

        public void ReLoad()
        {
            int needBulletCount = maxBullet - BulletCount;

            if (TotalBullet >= needBulletCount)
                BulletCount = maxBullet;
            else
                BulletCount += TotalBullet;

            TotalBullet -= needBulletCount;
        }


    }
}
