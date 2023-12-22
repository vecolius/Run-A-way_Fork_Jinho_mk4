using Hojun;
using System;
using System.Collections.Generic;
using UnityEngine;
using Gayoung;
using Yeseul;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using Jaeyoung;

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

    public interface IReLoadAble
    {
        void ReLoad();
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
            /*
            if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
            {
                player.animator.SetTrigger("Jump");
                player.animator.SetFloat("JumpType", 0f);   //그냥 여기서 짬푸
                player.moveState = PlayerMoveState.jump;
            }
            */
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
            /*
            if (Input.GetKeyDown(KeyCode.Space) && player.isGrounded)
            {
                player.moveState = PlayerMoveState.jump;
                player.animator.SetTrigger("Jump");
                player.animator.SetFloat("JumpType", 1f);   
            }
            */

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
                player.transform.Translate(Vector3.forward * (player.state.MoveSpeed * 1.7f) * Time.deltaTime);
                player.animator.SetFloat("WalkType", 1f);
                /*
                if (Input.GetKey(KeyCode.Space) && player.isGrounded)
                {
                    player.moveState = PlayerMoveState.jump;
                    player.animator.SetTrigger("Jump");
                    player.animator.SetFloat("JumpType", 1f);
                }
                */
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
                player.moveState= PlayerMoveState.walk;

        }
    }
    /*
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
    */
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
                {
                    hp = 0;

                }
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
            hp = maxHp;
        }
    }
    #endregion

    public class Player : MonoBehaviourPun, IHitAble , IDieable
    {
        PhotonView view = null;
        public PlayerData state = null;                                   //player의 기본state
        public GameObject defaultWeapon;
        public event Action initList;

        public float Hp
        {
            get => state.Hp;
            set
            {
                state.Hp = value;
                if(state.Hp <= 0)
                {
                    Die();
                }
                if(state.Hp > state.MaxHp)
                    state.Hp = state.MaxHp;

                initList();
            }
        }

        public GameObject[] weaponObjSlot = new GameObject[4];        //현재 들고있는 weaponSlot
        
        public IUseable currentItem = null;                         //현재 들고있는 weapon
        public GameObject currentItemObj;


        public PlayerMoveState moveState;                           //현재 move전략
        public ItemType attackState;                                //현재 attack전력
        public IAttackStrategy attackStrategy = null;

        Dictionary<PlayerMoveState, IMoveStrategy> moveDic;         //move 전략 dictionary
        Dictionary<ItemType, AttackStrategy> attackDic;            //attack 전략 dictionary
        
        public Camera mainCamera;
        public AimComponent Aim;

        public Animator animator;
        public bool isGrounded = true;

        public Transform weaponHand;
        public int weaponIndex;

        public SoundComponent soundComponent;
        
        public event Action onWeaponChange;
        public event Action attackAction;

        public bool IsDead { get=>isDead; }
        bool isDead = false;

        public int WeaponIndex
        {
            get { return weaponIndex; }
            set { 
                
                weaponIndex = value;
                WeaponChange();
            }
        }

        public CharacterData Data => Data;

        CharacterData IHitAble.Data => throw new NotImplementedException();


        void Start()
        {
            view = GetComponent<PhotonView>();
            soundComponent = GetComponent<SoundComponent>();
            state = new PlayerData();
            GamePlayManager.instance.players.Add(this);
            moveDic = new Dictionary<PlayerMoveState, IMoveStrategy>();
            moveDic.Add(PlayerMoveState.idle, new Idle(this));
            moveDic.Add(PlayerMoveState.walk, new Walk(this));
            moveDic.Add(PlayerMoveState.run, new Run(this));
            //moveDic.Add(PlayerMoveState.jump, new Jump(this));
            /*
            attackDic = new Dictionary<ItemType, AttackStrategy>();
            attackDic.Add(ItemType.Rifle, new RifleAttackStrategy(this));
            attackDic.Add(ItemType.Shotgun, new ShotGunStregy(this));
            attackDic.Add(ItemType.Handgun, new HandgunAttackStrategy(this));
            attackDic.Add(ItemType.Melee, new MeleeAttackStrategy(this));
            attackDic.Add(ItemType.Grenade, new GranadeAttackStrategy(this));
            */

            moveState = PlayerMoveState.idle;

            //GameObject weapon = Instantiate(defaultWeapon);
            GameObject weapon = PhotonNetwork.Instantiate("Handgun_Prototype", transform.position, Quaternion.identity);
            weaponObjSlot[1] = weapon;
            weaponObjSlot[1].GetComponent<IAttackItemable>().Player = this;
            weaponObjSlot[1].GetComponent<Collider>().enabled = false;
            
            weaponIndex = 1;
            currentItemObj = weaponObjSlot[weaponIndex];
            currentItem = currentItemObj.GetComponent<IUseable>();
            attackStrategy = currentItem.AttackStrategy;
            Aim = mainCamera.GetComponent<AimComponent>();

            //attackStrategy = attackDic[ItemType.Handgun];
            //WeaponChange(); // 아무것도 안들고 있는 것

            UIManager.instance.PlayerEnter(this);
        }



        void Update()
        {
            //weapon.SetActive(true);
            if (view.IsMine == false)
                return;

            if(currentItemObj != null)
            {
                currentItemObj.transform.position = weaponHand.position;
                currentItemObj.transform.rotation = weaponHand.rotation;
            }


            moveDic[moveState]?.Moving();

            if ( Input.GetKey(KeyCode.Mouse0))//마우스 클릭시 공격이 나가는 부분
            {
                this.animator.SetBool("Shot", true);
                currentItem?.Use();
            }
            else
            {
                this.animator.SetBool("Shot", false);
            }

            if ( Input.GetKeyDown(KeyCode.R) ) // 재장전 부분
            {
                this.animator.SetTrigger("Reload");
                if(currentItem is IAttackItemable)
                    ((IAttackItemable)currentItem).Reloading();
                /*
                if (currentItem is IReLoadAble)
                    ((IReLoadAble)currentItem).ReLoad();
                */
            }

            //전략부분으로 넣어줘서 무기교체와 애니메이션 실행 바로 됩니다!
            if (Input.GetKeyDown(KeyCode.Alpha1)) // 무기 정보 [주무기]
            {
                WeaponIndex = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // 무기 정보 [보조무기]
            {
                WeaponIndex = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) // 무기 정보 [힐]
            {
                WeaponIndex = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) // 무기 정보 [수류탄]
            {
                WeaponIndex = 3;
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IAttackItemable>() != null)  //IAttackItemable => return,
                return;
            if (other.TryGetComponent(out IAttackAble attacker))
            {
                Debug.Log("공격당함");
                Hit(attacker.GetDamage());
            }

        }

        public void ItemUseEffect() //Animation Event 함수(아이템 사용)
        {
            currentItem.UseEffect();
        }


        public void ItemUseReload()//Animation Event 함수(재장전)
        {
            if (currentItem is IAttackItemable)
                ((IAttackItemable)currentItem).ReloadEffect();
        }


        public void WeaponChange() // 무기 교체(실제 프리팹과 무기정보가 교체된다.)
        {
            if (weaponObjSlot[WeaponIndex] == null)
            { 
                Debug.Log("무기가 없다");
                return;
            }

            if(currentItemObj != null)
                currentItemObj.SetActive(false);
            
            currentItemObj = weaponObjSlot[WeaponIndex];
            currentItemObj.SetActive(true);
            
            currentItem = currentItemObj.GetComponent<IUseable>();
            
            if (currentItem.AttackStrategy != null)
                attackStrategy = currentItem.AttackStrategy;

            attackState = currentItem.ItemType;
     
            Debug.Log("무기교체완");
           
        }

       
        public virtual void Hit(float damage, IAttackAble attacker)
        {
            Hp -= damage;
        }
        public float Attack()
        {
            currentItem.Use();
            return 0f;
        }

        public GameObject GetAttacker()
        {
            return this.gameObject;
        }

        public void Die()
        {
            animator.SetTrigger("Die");
        }

        [PunRPC]
        public void Dead()
        {
            gameObject.SetActive(false);
            isDead = true;
        }

        public void Hit(float damage)
        {
            Hp -= damage;
        }
    }   
}
