using Gayoung;
using Jaeyoung;
using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class ItemHandgun : WeaponMonoBehaviour, IAttackItemable, Yeseul.IInteractive , IReLoadAble
    {
        public WeaponData weaponData;
        public WeaponData WeaponData { get { return weaponData; } }
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
        public GameObject bullet;   //날아갈 총알 GameObject
        Transform aimPos;           //총알이 날아갈 위치
        public AudioClip gunFireSound;

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


        public void Use()
        {
            
            if (BulletCount == 0)
                return;

            strategy.Attack();
        }


        public void SetItem(Player player)
        {
            WeaponItem.SetWeapon(player, gameObject, 1, this);
        }
        


        public void Interaction(GameObject interactivePlayer)
        {
            if (interactivePlayer.TryGetComponent(out Player player) && this.player == null)
            {
                SetItem(player);
            }
        }

        public void MakeBullet()
        {
            BulletCount--;
            // make bullet -> obj_pull

            aimPos = player.Aim.aimObjPos;
            this.sound.ActiveSound();
            GameObject bulletObj = PoolingManager.instance.PopObj(PoolingType.BULLET);
            Bullet_Component bulletScript = bulletObj.GetComponent<Bullet_Component>();

            bulletScript.SetBulletData(weaponData, Player);
            bulletScript.SetBulletVec(firePos, aimPos.position);

            GameObject soundObj = PoolingManager.instance.PopObj(PoolingType.SOUND);
            soundObj.transform.position = firePos.position;
            AudioSource sound = soundObj.GetComponent<AudioSource>();
            sound.clip = gunFireSound;
            soundObj.SetActive(true);
            sound.Play();
        }

        public void UseEffect()
        {
            MakeBullet();
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
