using Hojun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    #region Player_interface
    public interface IMoveStrategy
    {
        void Moving();
    }
    public interface IAttackStrategy
    {
        void Attack();
  
    }
    public enum PlayerMoveState
    {
        idle,
        walk,
        run,
        dead,
        jump
    }
    public enum PlayerAttackState
    {
        Rifle,
        Shotgun,
        Handgun,
        melee,
        sub,
        heal,
        granade,
    // 총 타입 세분화 됨 (라이플, 샷건, 권총)
    }
    #endregion
    #region MoveStrategy_Class
    public class Idle : IMoveStrategy
    {
        Player player = null;
        public Idle(object owner)
        { 
            player = (Player)owner;

        }
        public void Moving()
        {
            player.animator.SetFloat("WalkType", 0f);
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                player.moveState = PlayerMoveState.walk;

            if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
            {
                player.animator.SetTrigger("Jump");
                player.animator.SetFloat("JumpType", 0f);   //그냥 여기서 짬푸
                player.moveState = PlayerMoveState.jump;
            }

        }
    }
    public class Walk : IMoveStrategy
    {
        Player player = null;
        public Walk(object owner) 
        {
            player = (Player)owner;
        }
        public void Moving()
        {
            Vector3 vec = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
                player.moveState = PlayerMoveState.run;

            if (Input.GetKey(KeyCode.A))
            {
                vec += Vector3.left;
                player.animator.SetFloat("WalkType", 0.2f);

            }
            if (Input.GetKey(KeyCode.W))
            {
                vec += Vector3.forward;
                player.animator.SetFloat("WalkType", 0.8f);
            }
            player.animator.SetBool("Front", true);
            if (Input.GetKey(KeyCode.D))
            {
                vec += Vector3.right;
                player.animator.SetFloat("WalkType", 0.4f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                vec += Vector3.back;
                player.animator.SetFloat("WalkType", 0.6f);
            }

            if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
            {
                player.moveState = PlayerMoveState.jump;
                player.animator.SetTrigger("Jump");
                player.animator.SetFloat("JumpType", 1f);   
            }


            if (vec == Vector3.zero)
                player.moveState = PlayerMoveState.idle;
            player.transform.Translate(vec.normalized * player.state.MoveSpeed * Time.deltaTime);
        }
    }
    public class Run : IMoveStrategy
    {
        Player player = null;
        public Run(object owner)
        {
            player = (Player)owner;
        }
        public void Moving()
        {
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                player.transform.Translate(Vector3.forward * (player.state.MoveSpeed * 1.2f) * Time.deltaTime);
                player.animator.SetFloat("WalkType", 1f);
                if (Input.GetKey(KeyCode.Space) && player.isGrounded)
                {
                    player.moveState = PlayerMoveState.jump;
                    player.animator.SetTrigger("Jump");
                    player.animator.SetFloat("JumpType", 1f);
                }
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
                player.moveState= PlayerMoveState.walk;

        }
    }
    public class Jump : IMoveStrategy
    {
        Player player = null;
        
        public Jump(object owner)
        {
            player = (Player)owner;
        }

        public void Moving()
        {
            player.isGrounded = false;
        }


    }
    #endregion
    #region AttackStrategy_class
    public class AttackStrategy : IAttackStrategy
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

        public virtual void BasicMotion()
        {
           
        }

        public virtual void OtherMotion()
        {
            
        }
    }
    public class RifleAttackStrategy : AttackStrategy
    {
        public RifleAttackStrategy(object owner) : base(owner)
        {

        }
        public override void Attack()
        {
            player.animator.SetTrigger("Shot");
            player.animator.SetFloat("GunType", 0.2f);
            //총은 꺼냈는데 공격 안하는중
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //총을 쏨
                player.animator.SetTrigger("Shot");
                player.animator.SetFloat("GunType", 0.4f);
                Debug.Log("어택 시작");
            }
           
            else if (Input.GetKey(KeyCode.R))
            {
                player.animator.SetTrigger("Reload");
                player.animator.SetFloat("ReloadType", 0.3f);
                Debug.Log("이제 장전 된다 ㅠ");
            }
            else if(Input.GetKeyDown(KeyCode.Alpha1) )
                base.WeaponSwap(0);
        }

    }

    public class ShotgunAttackStrategy : AttackStrategy
    {
        public ShotgunAttackStrategy(object owner) : base(owner)
        {

        }

        public override void Attack()
        {
            player.animator.SetTrigger("Shot");
            player.animator.SetFloat("GunType", 0.2f);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                player.animator.SetTrigger("Shot");
                player.animator.SetFloat("GunType", 0.6f);
                Debug.Log("어택 시작");

            }
            else if (Input.GetKey(KeyCode.R))
            {
                player.animator.SetTrigger("Reload");
                player.animator.SetFloat("ReloadType", 0.6f);
                Debug.Log("이제 장전 된다 ㅠ");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                base.WeaponSwap(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                base.WeaponSwap(2);
            else if(Input.GetKeyDown(KeyCode.Alpha4))
                base.WeaponSwap(3);
        }
    }

    public class HandgunAttackStrategy : AttackStrategy
    {
        public HandgunAttackStrategy(object owner) : base(owner)
        {

        }
        public override void Attack()
        {
            player.animator.SetTrigger("Shot");
            player.animator.SetFloat("GunType", 0.2f);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                player.animator.SetTrigger("Shot");
                player.animator.SetFloat("GunType", 0.8f);
                Debug.Log("어택 시작");
            }
            else if (Input.GetKey(KeyCode.R))
            {
                player.animator.SetTrigger("Reload");
                player.animator.SetFloat("ReloadType", 1f);
                Debug.Log("이제 장전 된다 ㅠ");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                base.WeaponSwap(2, 1.0f);
                // 권총은 모션이 달라서 이렇게 넣어보았습니다! = 가영
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                base.WeaponSwap(0, 1.0f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                base.WeaponSwap(3, 1.0f);
            }
        }

    }
    public class MeleeAttackStrategy : AttackStrategy
    {
        public MeleeAttackStrategy(object owner) : base(owner)
        {
        }
        public override void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
            }
        }
    }
    public class GranadeAttackStrategy : AttackStrategy
    {
        public GranadeAttackStrategy(object owner) : base(owner)
        {
        }
        public override void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

            }
            
        }
    }
    #endregion
    #region PlayerState_Class
    public class Job
    {
        public string name;
        public float maxHp;
        public float moveSpeed;
    }
    public class PlayerData
    {
        public Job job;
        public float DefaultMoveSpeed => 3;
        public float DefaultMaxHp => 200;
        float moveSpeed;
        public float MoveSpeed
        {
            get { return moveSpeed; } 
            set 
            { 
                moveSpeed = value;
            }
        }
        float maxHp;
        public float MaxHp 
        {
            get { return maxHp; }
            set { maxHp = value; }
        }
        float hp;
        public float Hp
        {
            get { return hp; }
            set 
            { 
                hp = value;
                if (hp <= 0)
                    hp = 0;
                if(hp > MaxHp)
                    hp = MaxHp;
            }
        }
        public PlayerData(Job job = null)
        {
            if (job == null)
            {
                this.job = null;
                moveSpeed = DefaultMoveSpeed;
                MaxHp = DefaultMaxHp;
            }
            else
            {
                this.job = job;
                moveSpeed = job.moveSpeed;
                MaxHp = job.maxHp;
            }
        }
    }
    #endregion

    public class Player : MonoBehaviour, IHitAble , IDieable, IAttackAble
    {
        public PlayerData state = null;                                   //player의 기본state
        public GameObject[] weaponObjSlot = new GameObject[4];        //현재 들고있는 weaponSlot
        public IUseable currentWeapon = null;                         //현재 들고있는 weapon

        public PlayerMoveState moveState;                           //현재 move전략
        public ItemType attackState;                                //현재 attack전력

        Dictionary<PlayerMoveState, IMoveStrategy> moveDic;         //move 전략 dictionary
        Dictionary<ItemType, IAttackStrategy> attackDic;            //attack 전략 dictionary

        public Camera mainCamera;
        public AimComponent Aim;

        public Animator animator;
        public bool isGrounded = true;

        public Transform weaponHand;
        public GameObject weapon;
        public int weaponIndex;

        public CharacterData Data => throw new NotImplementedException();

        CharacterData IHitAble.Data => throw new NotImplementedException();

        void Start()
        {
            state = new PlayerData();

            moveDic = new Dictionary<PlayerMoveState, IMoveStrategy>();
            moveDic.Add(PlayerMoveState.idle, new Idle(this));
            moveDic.Add(PlayerMoveState.walk, new Walk(this));
            moveDic.Add(PlayerMoveState.run, new Run(this));
            moveDic.Add(PlayerMoveState.jump, new Jump(this));

            attackDic = new Dictionary<ItemType, IAttackStrategy>();
            attackDic.Add(ItemType.rifle, new RifleAttackStrategy(this));
            attackDic.Add(ItemType.shotgun, new ShotgunAttackStrategy(this));
            attackDic.Add(ItemType.Handgun, new HandgunAttackStrategy(this));
            attackDic.Add(ItemType.Melee, new MeleeAttackStrategy(this));
            attackDic.Add(ItemType.Grenade, new GranadeAttackStrategy(this));

            moveState = PlayerMoveState.idle;

            //weaponSlot[0] = new Rifle(new WeaponData("", null, 1, 1, 1, 1, 1, null, PlayerAttackState.Rifle, null));
            currentWeapon = weapon.GetComponent<IUseable>();
            attackState = currentWeapon.ItemType;
            weaponIndex = 0;
            Aim = mainCamera.GetComponent<AimComponent>();
            WeaponChange(); // 아무것도 안들고 있는 것
        }
        void Update()
        {
            weapon.transform.position = weaponHand.position;
            weapon.transform.rotation = weaponHand.rotation;

            moveDic[moveState]?.Moving();
            attackDic[attackState]?.Attack();

         

            //무기 교환 메서드! 애니메이션은 공격전략에 들어가있음! = 가영
            //if (Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    WeaponChange(0);
            //}
            //if (Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    WeaponChange(1);
            //}
            //if (Input.GetKeyDown(KeyCode.Alpha3))
            //{
            //    WeaponChange(2);
            //}
            //if (Input.GetKeyDown(KeyCode.Alpha4))
            //{
            //    WeaponChange(3);
            //}
        }

        public void ItemUseEffect() //Animation Event 함수(아이템 사용)
        {
            currentWeapon.Use();
        }
        public void ItemUseReload()//Animation Event 함수(재장전)
        {
            currentWeapon.Reload();
        }
        public void WeaponChange() // 무기 교환 메서드!
        {
            if (weaponObjSlot[weaponIndex] == null)
            { 
                Debug.Log("무기가 없다");
                return;
            }

            // if (currentWeapon != null)
            // {
            Debug.Log(weapon.name + "이게 꺼졌음");
            GameObject tempObj = weapon;
            weapon.SetActive(false);
           // }
            weapon = weaponObjSlot[weaponIndex];
            Debug.Log(weapon.name + "이게 켜졌음");
            weapon.SetActive(true);
            currentWeapon = weapon.GetComponent<IUseable>();
            attackState = currentWeapon.ItemType;
            tempObj.SetActive(false);
            Debug.Log("무기교체완");
           // return index;
            // 애니메이션 이벤트 부분에서 켜지는 상태가 되어야 한다 지금 좀 이상..?
            //  weapon = weaponObjSlot[index];
            //  weapon.SetActive(true);
            // 이게 이벤트 함수에서 발생해야 하는 이벤트이다 어떻게 하지...?
            // 함수를 따로 빼고 해야 하나?
            // 진호야 모르겠어 ㅠ

        }

        public virtual void Hit(float damage, IAttackAble attacker)
        {
        }
        public void Attack()
        {
            currentWeapon.Use();
        }
        public GameObject GetAttacker()
        {
            return this.gameObject;
        }
        public void Die()
        {
            throw new NotImplementedException();
        }

    }   
}
