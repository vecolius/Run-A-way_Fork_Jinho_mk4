using Gayoung;
using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Jinho
{
    public class ItemShotgun : MonoBehaviour, IAttackItemable, Yeseul.IInteractive
    {
        public WeaponData WeaponData 
        { 
            get => weaponData;
        }
        [SerializeField] WeaponData weaponData;

        public Player Player 
        { 
            get => player;
            set { player = value; }
        }
        [SerializeField] Player player = null;

        public Transform firePos;   //총알 발사 위치
        public GameObject bullet;   //날아갈 총알 GameObject
        IAttackStrategy strategy;
        Transform AimPos
        {
            get => player.Aim.aimObjPos; //총알이 날아갈 위치
        }


        public ItemType ItemType => weaponData.itemType;
        public IAttackStrategy AttackStrategy => strategy;
        public int maxBullet;       //장전되는 총알 양
        [SerializeField] int bulletCount;            //현재 총에 들어있는 총알 양
        
        
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
        [SerializeField] int maxTotalBullet;         //최대로 내가 가지고 있는 총알의 합계
        [SerializeField] int totalBullet;            //내가 가지고 있는 총알의 합계
        public Collider weaponCol;
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
        public void Use()
        {
            
            if (BulletCount == 0)
                return;
            BulletCount--;
            
            //이펙트 + 사운드
            Vector3[] targetPosArray = new Vector3[9];
            SetTransform(targetPosArray);

            //총알이 나가는 효과
            for(int i=0; i<targetPosArray.Length; i++)
            {
                //GameObject bulletObj = Instantiate(bullet);
                GameObject bulletObj = PoolingManager.instance.PopObj(PoolingType.BULLET);
                Bullet_Component bulletScript = bulletObj.GetComponent<Bullet_Component>();
                bulletScript.SetBulletData(weaponData, Player);
                bulletScript.SetBulletVec(firePos, targetPosArray[i]);
            }
        }

        public void Reload()
        {
            if (BulletCount == maxBullet) 
                return;
            BulletCount += 1;
        }

        public void SetItem(Player player)
        {
            WeaponItem.SetWeapon(player, gameObject, 0, this.player);
        }

        public void Interaction(GameObject interactivePlayer)
        {
            if (interactivePlayer.TryGetComponent(out Player player) && this.player == null)
            {
                SetItem(player);
            }
        }
        private void OnTriggerEnter(Collider other)
        {

        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public GameObject GetAttacker()
        {
            throw new System.NotImplementedException();
        }

        public float GetDamage()
        {
            throw new System.NotImplementedException();
        }

        public void UseEffect()
        {
            throw new System.NotImplementedException();
        }
    }
}
