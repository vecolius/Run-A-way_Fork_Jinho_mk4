//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;


//namespace Hojun
//{

//    //public interface IStateMachine
//    //{
//    //    object GetOwner();
//    //    void SetState(string stateName);
//    //}

//    //public class StateMachine<T> : IStateMachine where T : class
//    //{
//    //    public T owner = null;
//    //    public State curState;
//    //    public Dictionary<string, State> stateDic;


//    //    public StateMachine(T owner)
//    //    {
//    //        this.owner = owner;
//    //        stateDic = new Dictionary<string, State>();
//    //    }

//    //    public void AddState(string stateName, State state)
//    //    {
//    //        if (stateDic.ContainsKey(stateName))
//    //            return;
//    //        stateDic.Add(stateName, state);
//    //        state.Init(this);

//    //    }

//    //    public object GetOwner()
//    //    {
//    //        return owner;
//    //    }

//    //    public void SetState(string stateName)
//    //    {
//    //        if (stateDic.ContainsKey(stateName))
//    //        {
//    //            if (curState != null)
//    //            {
//    //                curState.Exit();
//    //            }
//    //            curState = stateDic[stateName];
//    //            curState.Enter();

//    //        }
//    //    }
//    //    public void Update()
//    //    {
//    //        curState?.Update();
//    //    }

//    //}

//    //public class State
//    //{
//    //    public IStateMachine sm = null;

//    //    public virtual void Init(IStateMachine sm)
//    //    {
//    //        this.sm = sm;
//    //    }

//    //    public virtual void Enter()
//    //    {
//    //        Debug.Log(GetType().Name + "상태 진입");
//    //    }
//    //    public virtual void Update()
//    //    {

//    //    }
//    //    public virtual void Exit()
//    //    {
//    //        Debug.Log(GetType().Name + "상태 빠져나옴");
//    //    }
//    //}
//    public class GMState : State
//    {
//        public GameManager gm;
//        public event Action onEnter;

//        public override void Enter()
//        {
//            base.Enter();
//            if (onEnter != null) onEnter();
//        }
//        public override void Init(IStateMachine sm)
//        {
//            this.sm = sm;
//            gm = (GameManager)sm.GetOwner();
//        }
//    }

//    public class DefaultState : GMState
//    {
//        public override void Update()
//        {
//            base.Update();
//            if (Input.GetKeyDown(KeyCode.Space))
//            {
//                Debug.Log("게임 시작합니다.");
//                sm.SetState("Loading");//= new LoadingState();
//            }
//        }
//    }
//    public class PauseState : GMState
//    {
//        public override void Update()
//        {
//            base.Update();
//            if (Input.GetKeyDown(KeyCode.Q))
//            {
//                Debug.Log("일시정지를 해제합니다.");
//                sm.SetState("Play");//= new PlayState();
//            }
//        }
//    }
//    public class PlayState : GMState
//    {
//        public override void Update()
//        {
//            base.Update();
//            if (Input.GetKeyDown(KeyCode.Q))
//            {
//                Debug.Log("일시정지합니다.");
//                sm.SetState("Pause");
//            }
//        }
//    }

//    public class LoadingState : GMState
//    {
//        float targetTime = 3f;
//        float curTime = 0;
//        public override void Update()
//        {
//            base.Update();
//            curTime += Time.deltaTime;
//            if (targetTime < curTime)
//            {
//                sm.SetState("Play");
//                curTime = 0;
//            }
//            Debug.Log("로딩중..." + curTime);
//        }
//    }

//    public class GameOverState : GMState
//    {
//        public override void Update()
//        {

//        }
//    }

//    public class GameManager : MonoBehaviour
//    {
//        public static GameManager instance = null;
//        public StateMachine<GameManager> sm;

//        private void Awake()
//        {
//            if (instance == null)
//            {
//                instance = this;
//                DontDestroyOnLoad(gameObject);
//            }
//            else
//                Destroy(gameObject);
//        }
//        public GameObject obj;

//        void Start()
//        {
//            sm = new StateMachine<GameManager>(this);
//            PlayState ps = new PlayState();
//            ps.onEnter += () => { obj.SetActive(true); };

//            sm.AddState("Default", new DefaultState());
//            sm.AddState("Play", ps);
//            sm.AddState("Loading", new LoadingState());
//            sm.AddState("Pause", new PauseState());
//            sm.SetState("Default");
//        }

//        // Update is called once per frame
//        void Update()
//        {
//            sm.Update();
//        }
//    }

//}