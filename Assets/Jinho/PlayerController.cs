using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    #region Player_interface
    interface IMoveStrategy
    {
        void Moving();
    }
    interface IAttackStrategy
    {
        void Attack();
    }
    public enum PlayerMoveState
    {
        idle,
        walk,
        run,
        dead,
    }
    public enum PlayerAttackState
    {
        gun,
        melee,
        sub,
        heal,
        granade,
    }
    #endregion
    public class Job
    {
        public string name;
        public float maxHp;
        public float moveSpeed;
    }
    #region MoveStrategy_Class
    public class Idle : IMoveStrategy
    {
        PlayerController player = null;
        public Idle(object owner)
        { 
            player = (PlayerController)owner;

        }
        public void Moving()
        {
            player.animator.SetFloat("Blend", 0f);
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                player.moveState = PlayerMoveState.walk;
        }
    }
    public class Walk : IMoveStrategy
    {
        PlayerController player = null;
        public Walk(object owner) 
        {
            player = (PlayerController)owner;
        }
        public void Moving()
        {
            Vector3 vec = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
                player.moveState = PlayerMoveState.run;

            if (Input.GetKey(KeyCode.A))
            {
                vec += Vector3.left;
                player.animator.SetFloat("Blend", 0.2f);

            }
            if (Input.GetKey(KeyCode.W))
            {
                vec += Vector3.forward;
                player.animator.SetFloat("Blend", 0.8f);
            }
            player.animator.SetBool("Front", true);
            if (Input.GetKey(KeyCode.D))
            {
                vec += Vector3.right;
                player.animator.SetFloat("Blend", 0.4f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                vec += Vector3.back;
                player.animator.SetFloat("Blend", 0.6f);

            }

            if(vec == Vector3.zero)
                player.moveState = PlayerMoveState.idle;
            player.transform.Translate(vec.normalized * player.state.MoveSpeed * Time.deltaTime);
        }
    }
    public class Run : IMoveStrategy
    {
        PlayerController player = null;
        public Run(object owner)
        {
            player = (PlayerController)owner;
        }
        public void Moving()
        {
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                player.transform.Translate(Vector3.forward * (player.state.MoveSpeed * 1.2f) * Time.deltaTime);
                player.animator.SetFloat("Blend", 1f);
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
                player.moveState= PlayerMoveState.walk;
        }
    }
    public class Jump : IMoveStrategy
    {
        PlayerController player = null;
        public Jump(object owner)
        {
            player = ( PlayerController)owner;
        }
        public void Moving()
        {

        }
    }
    #endregion
    #region AttackStrategy_class
    public class AttackStrategy : IAttackStrategy
    {
        protected PlayerController player = null;
        protected KeyCode keycode;
        public AttackStrategy(object owner)
        {
            player = (PlayerController)owner;
        }

        public virtual void Attack()
        {
            
        }
        protected void WeaponChange()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                keycode = KeyCode.Alpha1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                keycode = KeyCode.Alpha2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                keycode = KeyCode.Alpha3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                keycode = KeyCode.Alpha4;
            }
            if (player.currentWeapon == player.weaponSlot[player.SlotGetToKey(keycode)]) 
                return;

            //무기 교체 애니
            player.currentWeapon = player.weaponSlot[player.SlotGetToKey(keycode)];
            player.attackState = player.currentWeapon.attackState;
        }
    }
    public class GunAttackStrategy : AttackStrategy
    {
        public GunAttackStrategy(object owner) : base(owner)
        {
        }
        public override void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                player.currentWeapon.Fire();
                //주무기 발사 애니
            }
            WeaponChange();
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
                player.currentWeapon.Fire();
                //근접 공격 애니
            }
            WeaponChange();
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
                player.currentWeapon.Fire();
                //수류탄 추척 애니
            }
            WeaponChange();
        }
    }
    #endregion
    public class PlayerState
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
        public PlayerState(Job job = null)
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
    public class PlayerController : MonoBehaviour
    {
        public PlayerState state;                                   //player의 기본state
        public Weapon[] weaponSlot = new Weapon[4];                 //weapon slot
        public Weapon currentWeapon = null;                         //현재 들고있는 weapon

        public PlayerMoveState moveState;                           //현재 move전략
        public PlayerAttackState attackState;                       //현재 attack전력
        Dictionary<PlayerMoveState, IMoveStrategy> moveDic;         //move 전략 dictionary
        Dictionary<PlayerAttackState, IAttackStrategy> attackDic;   //attack 전략 dictionary
        Dictionary<KeyCode, int> weaponSlotDic;                     //입력한 KeyCode에 따라 slot을 반환하는 dic

        public Animator animator;
        void Start()
        {
            state = new PlayerState();

            moveDic = new Dictionary<PlayerMoveState, IMoveStrategy>();
            moveDic.Add(PlayerMoveState.idle, new Idle(this));
            moveDic.Add(PlayerMoveState.walk, new Walk(this));
            moveDic.Add(PlayerMoveState.run, new Run(this));

            attackDic = new Dictionary<PlayerAttackState, IAttackStrategy>();
            attackDic.Add(PlayerAttackState.gun, new GunAttackStrategy(this));
            attackDic.Add(PlayerAttackState.melee, new MeleeAttackStrategy(this));
            attackDic.Add(PlayerAttackState.granade, new GranadeAttackStrategy(this));

            SetSlotDic();
            currentWeapon = weaponSlot[0];

            moveState = PlayerMoveState.idle;
            //attackState = currentWeapon.attackState;
        }

        void Update()
        {
            moveDic[moveState]?.Moving();
            if (Input.GetKey(KeyCode.Mouse0) && currentWeapon != null)
            {
                //currentWeapon?.Fire();
                attackDic[attackState]?.Attack();
            }
        }
        public int SlotGetToKey(KeyCode keycode)
        {
            return weaponSlotDic[keycode];
        }
        void SetSlotDic()   //weaponSlotDic을 입력하는 함수
        {
            weaponSlotDic = new Dictionary<KeyCode, int>();
            weaponSlotDic.Add(KeyCode.Alpha1, 0);
            weaponSlotDic.Add(KeyCode.Alpha2, 1);
            weaponSlotDic.Add(KeyCode.Alpha3, 2);
            weaponSlotDic.Add(KeyCode.Alpha4, 3);
        }

    }
}
