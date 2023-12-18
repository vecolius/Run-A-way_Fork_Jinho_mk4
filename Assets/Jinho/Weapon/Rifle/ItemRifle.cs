using Gayoung;
using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class ItemRifle : MonoBehaviour, IAttackItemable, Yeseul.IInteractive, IReLoadAble
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
        public GameObject bullet;   //날아갈 총알 GameObject
        Transform aimPos;           //총알이 날아갈 위치
        public AudioClip gunFireSound;

        public int maxBullet;       //장전되는 총알 양
        [SerializeField]int maxTotalBullet;         //최대로 내가 가지고 있는 총알의 합계
        [SerializeField]int bulletCount;            //현재 총에 들어있는 총알 양
        [SerializeField]int totalBullet;            //내가 가지고 있는 총알의 합계
        IAttackStrategy strategy;

        void OnEnable()
        {
            strategy = new RifleAttackStrategy(player);

        }

        public ItemType ItemType => weaponData.itemType;

        public int BulletCount
        {
            get { return bulletCount; }
            set
            {
                bulletCount = value;
                if (bulletCount > maxBullet) bulletCount = maxBullet;
                if (bulletCount < 0) bulletCount = 0;
            }
        }

        public int TotalBullet
        {
            get { return totalBullet; }
            set
            {
                totalBullet = value;
                if (totalBullet > maxTotalBullet) totalBullet = maxTotalBullet;
                if (totalBullet < 0) totalBullet = 0;
            }
        }

        public void Use()
        {
            if (BulletCount == 0)
                return;
            
            strategy.Attack();

            //AttackStrategy.Attack();


            //Attack();
            /*
            if (BulletCount == 0)
                return;
            BulletCount--;
            */


            //-- hojun 231216 refactoring
            //Debug.Log("라이플 총알 생성");
            //aimPos = player.Aim.aimObjPos;
            //GameObject bulletObj = PoolingManager.instance.PopObj(PoolingType.BULLET);
            //Bullet_Component bulletScript = bulletObj.GetComponent<Bullet_Component>();
            //bulletScript.SetBulletData(weaponData, Player);
            //bulletScript.SetBulletVec(firePos, aimPos.position);
            //---


            //bulletObj.SetActive(true);
            //이펙트 + 사운드
            /*
            GameObject soundObj = PoolingManager.instance.PopObj(PoolingType.SOUND);
            soundObj.transform.position = firePos.position;
            soundObj.GetComponent<AudioSource>().clip = gunFireSound;
            soundObj.SetActive(true);
            */
        }


        public void SetItem(Player player)
        {
            WeaponItem.SetWeapon(player, gameObject, 0, this);
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
            MakeBullet();
        }
        public void MakeBullet()
        {
            //BulletCount--;
            // make bullet -> obj_pull

            aimPos = player.Aim.aimObjPos;
            GameObject bulletObj = PoolingManager.instance.PopObj(PoolingType.BULLET);
            Bullet_Component bulletScript = bulletObj.GetComponent<Bullet_Component>();

            bulletScript.SetBulletData(weaponData, Player);
            bulletScript.SetBulletVec(firePos, aimPos.position);
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
