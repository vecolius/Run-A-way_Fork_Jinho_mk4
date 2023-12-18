using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class Loading : MonoBehaviour
{

    public Slider progressBar;
    public Image barColor;
    public TextMeshProUGUI loadtext;
    //private float completeTime = 1f;
    //private float uncompleteTime = 0.9f;
   
   
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        // LoadSceneAsync() 비동기로드.
        // Scene을 불러오면 완료까지 다른 작업을 수행하지 않음.
        AsyncOperation operation = SceneManager.LoadSceneAsync("GameScene");
        // 로딩이 끝나도 멈춤.
        operation.allowSceneActivation = false;


        while (!operation.isDone)
        {
            yield return null;

            //Color sliderColor = Color.Lerp(Color.white, Color.blue, completeTime);
            //barColor.color = sliderColor;

            if (progressBar.value < 0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 0.9f, Time.deltaTime);
            }
            else if (operation.progress >= 0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.deltaTime);
            }

            if (progressBar.value >= 1f)
                loadtext.text = "Press SpaceBar";

            if (Input.GetKeyDown(KeyCode.Space) &&
                progressBar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
