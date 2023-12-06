using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class CorutineRunner : MonoBehaviour
{

    private static CorutineRunner instance;
    private List<Coroutine> runingCo = new List<Coroutine>();
    

    private void Awake()
    {
        instance = this;


        Debug.Log("test");
        Debug.Log("test2");
        //fdsfds
        //fdsffdsfds
    }

    public Coroutine StartCorutine(IEnumerator coroutine)
    {
        var corutineData = instance.StartCoroutine(coroutine);
        runingCo.Add(corutineData);
        return corutineData;
    }

    public void Stop(Coroutine coroutine)
    {

        var runItem = runingCo.Find(run => run == coroutine);
        runingCo.Remove(runItem);
        instance.StopCoroutine(runItem);
    }

    public void OneFrame(Action e) 
    {
        instance.StartCoroutine(One(e));
    }

    public IEnumerator One(Action myFunc)
    {

        yield return null;
        myFunc();
    }

}
