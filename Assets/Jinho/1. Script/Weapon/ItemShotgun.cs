using Gayoung;
using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Photon.Pun;

namespace Jinho
{
    public class ItemShotgun : WeaponMonoBehaviour, IAttackItemable, Yeseul.IInteractive, IReLoadAble
    {
        public Player Player 
        { 
            get => player;
            set 
            { 
                player = value;
                if(player != null)
                {
                    strategy = new ShotGunStregy(player);
                }
            }
        }
        [SerializeField] Player player = null;

        public Transform firePos;   //총알 발사 위치
        public AudioClip gunFireSound;
        public AudioClip reloadSound;
        public GameObject fireEffect;

        IAttackStrategy strategy;
        Transform AimPos
        {
            get => player.Aim.aimObjPos; //총알이 날아갈 위치
        }


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
        public Collider weaponCol;

        void SetTransform(Vector3[] array)   //삿건 전용 총알 9개가 가야할 죄표
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Random.insideUnitSphere * 1.0f + AimPos.position;    //aimPos에서 일정 구 범위 안의 랜덤 좌표로 저장
            }
        }
        void OnEnable()
        {
            strategy = new ShotGunStregy(player);
        }
        [PunRPC]
        public void Use()
        {
            if (BulletCount <= 0)
                return;

            player.animator.SetBool("Shot", true);
            strategy.Attack();
        }
        [PunRPC]
        public void MakeBullet()
        {
            BulletCount--;
            // make bullet -> obj_pull

            //이펙트 + 사운드
            Vector3[] targetPosArray = new Vector3[9];
            SetTransform(targetPosArray);

            //총알이 나가는 효과
            for (int i = 0; i < targetPosArray.Length; i++)
            {
                //GameObject bulletObj = Instantiate(bullet);
                GameObject bulletObj = PoolingManager.instance.PopObj(PoolingType.BULLET);
                Bullet_Component bulletScript = bulletObj.GetComponent<Bullet_Component>();
                bulletObj.SetActive(true);
                bulletScript.SetBulletData(weaponData, Player);
                bulletScript.SetBulletVec(firePos, targetPosArray[i]);
            }

            this.sound.ActiveSound();
            SoundEffect(gunFireSound, firePos);
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
