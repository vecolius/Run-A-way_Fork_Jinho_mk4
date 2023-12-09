using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;


// 플레이어 공격 부분도 같이 만들어야 하기 떄문에 구조 생각하고 연결 가능하게 만들어서 

public class SceneController : DontDestroySingle<SceneController>
{
    [SerializeField] GameObject titleUi;
    [SerializeField] GameObject mainUi;
    [SerializeField] GameObject lobbyUi;
    [SerializeField] GameObject gameUi;
    [SerializeField] GameObject loadingUi;

    //[SerializeField] G

    List<GameObject> totalUi= new List<GameObject>(); 

    // [SerializeField] private GameObject lobbyMultiUi; //  멀티 일 때 사용 예정
    private new void Awake()
    {
        base.Awake();
    }

    // 일단 구조적으로 연결가능하게 고민해 보고 힘들것 같으면 triggerEvnet 클릭을 만들어서
    // 사용해도 됨 그게 좀 더 간단 하긴 함.
    public void Option(string optionUi) // 메인 화면 옵션
    {
        
        //OptionImage
        //Button option;
        //option = mainUi.GetComponentInChildren<Button>();

        //optionUi

        //if (optionUi
        //    optionUi.enabled = true;
        //else
        //    optionUi.enabled = false;

    }

    public void Explanation() // 메인 화면 설명창
    {
        // 이것 또한 마찬가지 
        Button explanation;
        explanation = mainUi.GetComponentInChildren<Button>();


    }


    private void Start()
    {
        LoadScene("TitleScene");

        totalUi.Add(titleUi);
        totalUi.Add(mainUi);
        totalUi.Add(lobbyUi);
        totalUi.Add(gameUi);
        totalUi.Add(loadingUi);

    }

   
    public void ChangeScene(string sceneName)
    {
        foreach (GameObject ui in totalUi) 
        {
            ui.SetActive(false);
        }

        // 리팩토링 해야할 듯 코드 무슨일...?
        switch (sceneName)
        {
            case "TitleScene":
                titleUi.SetActive(true);
                break;

            case "MainScene":
                mainUi.SetActive(true);
                break;

            case "LobbySceneSingle":
                lobbyUi.SetActive(true);
                break;

            case "GameScene":
                gameUi.SetActive(true);
                break;

            case "LoadingScene":
                loadingUi.SetActive(true);
                break;
            //case "LobbyMultiScene":
            //  멀티 일 때 사용 예정
            //    break;
        }

    }


    //button click component [SerializeField] use
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        instance.ChangeScene(sceneName);
   
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
        //Debug.Log("게임 종료"); 작동 확인
    }


    private void Update()
    {
        Option("optionUi");
    }

}

