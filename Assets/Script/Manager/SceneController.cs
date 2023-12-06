using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class SceneController : DontDestroySingle<SceneController>
{
    [SerializeField] protected GameObject titleUi;
    [SerializeField] protected GameObject mainUi;
    [SerializeField] protected GameObject lobbyUi;
    [SerializeField] protected GameObject gameUi;
    [SerializeField] protected GameObject loadingUi;
    // [SerializeField] private GameObject lobbyMultiUi; //  멀티 일 때 사용 예정
    private new void Awake()
    {
        base.Awake();
    }

   
    private void Start()
    {
        SceneController.LoadScene("TitleScene");
       
    }

    SceneController()
    {
        titleUi = GetComponent<SceneController>().titleUi;
        mainUi = GetComponent<SceneController>().mainUi;
        lobbyUi = GetComponent<SceneController>().lobbyUi;
        gameUi = GetComponent<SceneController>().gameUi;
        loadingUi = GetComponent<SceneController>().loadingUi;
    }


    public void ChangeScene(string sceneName)
    {

        switch (sceneName)
        {
            case "TitleScene":
                titleUi.active = true;
                break;
            case "MainScene":
                mainUi.active = true;
                break;
            case "LobbySceneSingle":
                lobbyUi.active = true;
                break;
            case "GameScene":
                gameUi.active = true;
                break;
            case "LoadingScene":
                loadingUi.active = true;
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
    }

    private void Update()
    {
        ChangeScene("sceneName");
    }


}
