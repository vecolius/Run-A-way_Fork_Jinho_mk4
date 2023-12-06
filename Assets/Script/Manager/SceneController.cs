using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

// 전체적으로 UI는 조금씩 계속 만들어주는 방향으로 가야할 듯 하다.
// 플레이어 공격 부분도 같이 만들어야 하기 떄문에 구조 생각하고 연결 가능하게 만들어서 
// 다른 곳 도 연결해야 하니까 웬만하면 [SerializeField]를 이용하자 연결하기는 이게 쉬움!
public class SceneController : DontDestroySingle<SceneController>
{
    [SerializeField] GameObject titleUi;
    [SerializeField] GameObject mainUi;
    [SerializeField] GameObject lobbyUi;
    [SerializeField] GameObject gameUi;
    [SerializeField] GameObject loadingUi;


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
        // 일단 가져온 optionUi를 끄고 키기 원하는데
        // 그걸 위해서는 다른 방식을 이용해 부르거나 Onclick 함수를 사용해
        // 보는 것도 방법 일 듯 하다 
        // 다른 방식은 밑에서 시도 하던 방법으로 해봐도 나쁘지 않을 듯 함.
        // 연결만 잘 하면 돌아갈지도?
        // 참고로 옵션 부분은 사운드 부분도 들어가고 하기 때문에 좀 더 고민하고 해보기.


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

