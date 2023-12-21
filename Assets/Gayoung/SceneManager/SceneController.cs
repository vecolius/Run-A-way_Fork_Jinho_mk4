using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
using System.Linq;
using Photon.Pun;


// �÷��̾� ���� �κе� ���� ������ �ϱ� ������ ���� �����ϰ� ���� �����ϰ� ���� 

public class SceneController : DontDestroySingle<SceneController>
{
    // �� ���� ui
    [SerializeField] GameObject titleUi;
    [SerializeField] GameObject mainUi;
    [SerializeField] GameObject lobbyUi;
    [SerializeField] GameObject gameUi; 
    

    //��ư ������ �� Ȱ��ȭ �Ǵ� â
    //[SerializeField] GameObject mainOptionImage;
    [SerializeField] GameObject mainExplanationImage;
    [SerializeField] GameObject lobbyChacterChoiceImage;
    [SerializeField] GameObject lobbyProfessionChoiceButton;


    Dictionary<string, GameObject> uiObjectDict = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> buttonObjDict = new Dictionary<string, GameObject>();

    List<GameObject> totalUi= new List<GameObject>();
   
    // [SerializeField] private GameObject lobbyMultiUi; //  ��Ƽ �� �� ��� ����
    private new void Awake()
    {
        base.Awake();
    }

    private void Start()
    {

        totalUi.Add(titleUi);
        totalUi.Add(mainUi);
        totalUi.Add(lobbyUi);
        totalUi.Add(gameUi);

        //case "TitleScene":
        //    titleUi.SetActive(true);
        //    break;

        //case "MainScene":
        //    mainUi.SetActive(true);
        //    break;

        //case "LobbySceneSingle":
        //    lobbyUi.SetActive(true);
        //    break;

        ////case "GameScene":
        ////    gameUi.SetActive(true);
        ////    break;

        //case "LoadingScene":
        //    gameUi.SetActive(true);
        //    break;
        //    //case "LobbyMultiScene":
        //    //  ��Ƽ �� �� ��� ����
        //    //    break;


        uiObjectDict["TitleScene"] = titleUi;
        uiObjectDict["MainScene"] = mainUi;
        uiObjectDict["LobbySceneSingle"] = lobbyUi;
        uiObjectDict["LoadingScene"] = gameUi;

        //buttonObjDict["Option"] = mainOptionImage;
        buttonObjDict["Explanation"] = mainExplanationImage;
        buttonObjDict["ChacterChoiceButton"] = lobbyChacterChoiceImage;
        buttonObjDict["ProfessionChoiceButton"] = lobbyProfessionChoiceButton;

        LoadScene("TitleScene");
    }
    
    public void OnClick(string buttonName) 
    {
        // �ٸ� ��ư�� ������ ���ؼ��� �ٸ� ��ư�� true �� ���� �ٸ� ��ư Ȱ��ȭ ����!
        // forech�� ������ ������ ���� ��
        //switch (buttonName)
        //{
        //    case "Option":
        //        mainOptionImage.SetActive(!mainOptionImage.activeSelf);
        //        break;
        //    case "Explanation":
        //        mainExplanationImage.SetActive(!mainExplanationImage.activeSelf);
        //        break;
        //    case "ChacterChoiceButton":
        //        lobbyChacterChoiceImage.SetActive(!lobbyChacterChoiceImage.activeSelf);
        //        break;
        //    case "ProfessionChoiceButton":
        //        lobbyProfessionChoiceButton.SetActive(!lobbyProfessionChoiceButton.activeSelf);
        //        break; 
                
        //}

        buttonObjDict[buttonName].SetActive( !buttonObjDict[buttonName].activeSelf );
       
        Debug.Log("��ư ����");
    }





    
    public void ChangeScene(string sceneName)
    {
        foreach (GameObject ui in totalUi) 
        {
            ui.SetActive(false);
        }

        uiObjectDict[sceneName].SetActive(true);

    }


    //button click component [SerializeField] use
    [PunRPC]
    public void LoadScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
        instance.ChangeScene(sceneName);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void StartGame(string sceneName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GetComponent<PhotonView>().RPC("LoadScene", RpcTarget.AllBuffered, sceneName);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
            

    }

    public void LeftRoom(string sceneName)
    {
        PhotonNetwork.LeaveRoom();
        LoadScene(sceneName);
        LobbyManager.instance.DeletePlayer(gameObject.GetComponent<LobbyManager>());
        
    }



}

