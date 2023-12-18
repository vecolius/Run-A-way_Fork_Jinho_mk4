using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
using System.Linq;


// �÷��̾� ���� �κе� ���� ������ �ϱ� ������ ���� �����ϰ� ���� �����ϰ� ���� 

public class SceneController : DontDestroySingle<SceneController>
{
    // �� ���� ui
    [SerializeField] GameObject titleUi;
    [SerializeField] GameObject mainUi;
    [SerializeField] GameObject lobbyUi;
    [SerializeField] GameObject gameUi;
    [SerializeField] GameObject loadingUi;

    //��ư ������ �� Ȱ��ȭ �Ǵ� â
    [SerializeField] GameObject mainOptionImage;
    [SerializeField] GameObject mainExplanationImage;
    [SerializeField] GameObject lobbyChacterChoiceImage;
    [SerializeField] GameObject lobbyProfessionChoiceButton;


    List<GameObject> totalUi= new List<GameObject>();
   
    // [SerializeField] private GameObject lobbyMultiUi; //  ��Ƽ �� �� ��� ����
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
        // �ٸ� ��ư�� ������ ���ؼ��� �ٸ� ��ư�� true �� ���� �ٸ� ��ư Ȱ��ȭ ����!
        // forech�� ������ ������ ���� ��
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
       
            Debug.Log("��ư ����");
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
            //  ��Ƽ �� �� ��� ����
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
        //Debug.Log("���� ����"); �۵� Ȯ��
    }


    private void Update()
    {
      
    }

}

