using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomoptions = new RoomOptions();
        roomoptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(null, roomoptions);

        Debug.Log("¹æ »ý¼º");
    }

}
