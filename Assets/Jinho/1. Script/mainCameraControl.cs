using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class mainCameraControl : MonoBehaviour
{
    PhotonView view;
    void Start()
    {
        view = transform.root.GetComponent<PhotonView>();
        if(view.IsMine == false )   
            gameObject.SetActive(false);
    }
}
