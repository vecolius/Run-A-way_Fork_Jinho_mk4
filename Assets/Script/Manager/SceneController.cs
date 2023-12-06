using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class SceneController : DontDestroySingle<SceneController>
{
    [SerializeField] GameObject titleUi;
    [SerializeField] GameObject mainUi;
    [SerializeField] GameObject lobbyUi;
    [SerializeField] GameObject gameUi;
    [SerializeField] GameObject loadingUi;

    // [SerializeField] private GameObject lobbyMultiUi; //  멀티 일 때 사용 예정
    private new void Awake()
    {
        base.Awake();
        
    }

   
    private void Start()
    {
        LoadScene("TitleScene");

        //titleUi = GetComponent<Image>();
        //mainUi = GetComponent<SceneController>().mainUi;
        //lobbyUi = GetComponent<SceneController>().lobbyUi;
        //gameUi = GetComponent<SceneController>().gameUi;
        //loadingUi = GetComponent<SceneController>().loadingUi;

    }

    //SceneController()
    //{
    //    titleUi = GetComponent<SceneController>().titleUi;
    //    mainUi = GetComponent<SceneController>().mainUi;
    //    lobbyUi = GetComponent<SceneController>().lobbyUi;
    //    gameUi = GetComponent<SceneController>().gameUi;
    //    loadingUi = GetComponent<SceneController>().loadingUi;
    //}


    public void ChangeScene(string sceneName)
    {

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

   

}
