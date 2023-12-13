using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySingle<T> : SingleTon<T> where T : DontDestroySingle<T>
{

    protected new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

    }

}
