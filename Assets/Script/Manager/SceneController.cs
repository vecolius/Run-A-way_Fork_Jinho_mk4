using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
using System.Linq;


// 플레이어 공격 부분도 같이 만들어야 하기 떄문에 구조 생각하고 연결 가능하게 만들어서 

public class SceneController : DontDestroySingle<SceneController>
{
    // 씬 연결 ui
    [SerializeField] GameObject titleUi;
    [SerializeField] GameObject mainUi;
    [SerializeField] GameObject lobbyUi;
    [SerializeField] GameObject gameUi;
    [SerializeField] GameObject loadingUi;

    //버튼 눌렀을 시 활성화 되는 창
    [SerializeField] GameObject mainOptionImage;
    [SerializeField] GameObject mainExplanationImage;
    [SerializeField] GameObject lobbyChacterChoiceImage;
    [SerializeField] GameObject lobbyProfessionChoiceButton;


    List<GameObject> totalUi= new List<GameObject>(); 

    // [SerializeField] private GameObject lobbyMultiUi; //  멀티 일 때 사용 예정
    private new void Awake()
    {
        base.Awake();
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
    
    public void OnClick(string buttonName) 
    {
        // 다른 버튼을 누르기 위해서는 다른 버튼이 true 일 때는 다른 버튼 활성화 금지!
        // forech로 돌려도 나쁘지 않을 듯
        switch (buttonName)
        {
            case "Option":
                mainOptionImage.SetActive(!mainOptionImage.activeSelf);
                break;
            case "Explanation":
                mainExplanationImage.SetActive(!mainExplanationImage.activeSelf);
                break;
            case "ChacterChoiceButton":
                lobbyChacterChoiceImage.SetActive(!lobbyChacterChoiceImage.activeSelf);
                break;
            case "ProfessionChoiceButton":
                lobbyProfessionChoiceButton.SetActive(!lobbyProfessionChoiceButton.activeSelf);
                break; 
        }

            Debug.Log("버튼 눌림");
    }

  
    public void ChangeScene(string sceneName)
    {
        foreach (GameObject ui in totalUi) 
        {
            ui.SetActive(false);
        }

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
        //Option("optionUi");
    }

}

