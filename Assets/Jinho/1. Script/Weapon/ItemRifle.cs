using Gayoung;
using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class ItemRifle : WeaponMonoBehaviour, IAttackItemable, Yeseul.IInteractive, IReLoadAble
    {
        public WeaponData weaponData;
        public WeaponData WeaponData {  get { return weaponData; } }
        
        public Player Player 
        {  
            get=> player;
            set 
            { 
                player = value; 
                if(player != null)
                {
                    strategy = new RifleAttackStrategy(player);
                }
            }
        }
        public IAttackStrategy AttackStrategy
        {
            get => strategy;
            set
            {
                strategy = value;
            }
        }
        [SerializeField] Player player = null;
        
        public Transform firePos;   //총알 발사 위치
        public Collider weaponCol;
        Transform aimPos;           //총알이 날아갈 위치
        public AudioClip gunFireSound;
        public AudioClip reloadSound;
        public GameObject fireEffect;

        public SoundComponent sound;
        IAttackStrategy strategy;

        void OnEnable()
        {
            strategy = new RifleAttackStrategy(player);

        }

        public ItemType ItemType => weaponData.itemType;

        public void Use()
        {
            if (BulletCount == 0)
                return;
            
            strategy.Attack();

        }


        public void SetItem(Player player)
        {
            SetWeapon(player, gameObject, 0, this);
        }



        public void Interaction(GameObject interactivePlayer)
        {
            if (interactivePlayer.TryGetComponent(out Player player) && this.player == null)
            {
                SetItem(player);
            }
        }

        public void UseEffect()
        {
            InstantiateEffect(fireEffect, firePos);
            MakeBullet();
        }
        public void MakeBullet()
        {
            BulletCount--;
            // make bullet -> obj_pull

            aimPos = player.Aim.aimObjPos;
            GameObject bulletObj = PoolingManager.instance.PopObj(PoolingType.BULLET);
            Bullet_Component bulletScript = bulletObj.GetComponent<Bullet_Component>();

            bulletScript.SetBulletData(weaponData, Player);
            bulletScript.SetBulletVec(firePos, aimPos.position);

            this.sound.ActiveSound();
            SoundEffect(gunFireSound, firePos);
        }
        public void Reloading()    //근접무기는 재장전 없음
        {
            if (strategy is IReLoadAble)
                ((IReLoadAble)strategy).ReLoad();
        }
        public void ReloadEffect()
        {
            ReLoad();
            SoundEffect(reloadSound, transform);
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
