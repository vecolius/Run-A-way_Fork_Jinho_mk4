using Jinho_Weapon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho_Player
{
    interface IMoveStrategy
    {
        void Moving();
    }
    interface IAttackStractegy
    {
        void Fire();
    }
    public enum MoveState
    {
        idle,
        walk,
        run,
        dead,
    }
    public enum AttackState
    {
        fire,
        reload,
        change,
    }
    public class Job
    {
        public string name;
        public float maxHp;
        public float moveSpeed;
    }
    #region MoveStractegy_Class
    public class Idle : IMoveStrategy
    {
        PlayerController player = null;
        public Idle(Object owner)
        { 
            player = (PlayerController)owner;
        }
        public void Moving()
        {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                player.moveState = MoveState.walk;
        }
    }
    public class Walk : IMoveStrategy
    {
        PlayerController player = null;
        public Walk(Object owner) 
        {
            player = (PlayerController)owner;
        }
        public void Moving()
        {
            Vector3 vec = Vector3.zero;
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
                player.moveState = MoveState.run;

            if (Input.GetKey(KeyCode.A))
            {
                vec += Vector3.left;
                //왼쪽으로 이동 애니
            }
            if (Input.GetKey(KeyCode.W))
            {
                vec += Vector3.forward;
                //정면으로 이동 애니
            }
            if (Input.GetKey(KeyCode.D))
            {
                vec += Vector3.right;
                //오른쪽으로 이동 애니
            }
            if (Input.GetKey(KeyCode.S))
            {
                vec += Vector3.back;
                //뒤로 이동 애니
            }

            if(vec == Vector3.zero)
                player.moveState = MoveState.idle;
            player.transform.Translate(vec.normalized * player.state.MoveSpeed * Time.deltaTime);
        }
    }
    public class Run : IMoveStrategy
    {
        PlayerController player = null;
        public Run(Object owner)
        {
            player = (PlayerController)owner;
        }
        public void Moving()
        {
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                player.transform.Translate(Vector3.forward * (player.state.MoveSpeed * 1.2f) * Time.deltaTime);
            }
            if(Input.GetKeyUp(KeyCode.LeftShift))
                player.moveState= MoveState.walk;
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
        public PlayerState state;
        [SerializeField]public Weapon weapon;

        public MoveState moveState;
        public AttackState attackState;
        Dictionary<MoveState, IMoveStrategy> moveDic;
        void Start()
        {
            state = new PlayerState();

            moveDic = new Dictionary<MoveState, IMoveStrategy>();
            moveDic.Add(MoveState.idle, new Idle(this));
            moveDic.Add(MoveState.walk, new Walk(this));
            moveDic.Add(MoveState.run, new Run(this));
            moveState = MoveState.idle;
        }

        void Update()
        {
            moveDic[moveState].Moving();
            if (Input.GetKey(KeyCode.Mouse0) && weapon != null)
                weapon.Fire();
        }
    }
}
