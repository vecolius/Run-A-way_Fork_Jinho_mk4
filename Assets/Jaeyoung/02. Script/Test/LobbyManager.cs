using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager instance;
    [SerializeField] GameObject characterPrafab;
    [SerializeField] GameObject sceneController;
    [SerializeField] GameObject backGround;
    [SerializeField] Image[] playerImage = new Image[4];
    public int playerCount;
    public int playerNumber;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        PhotonNetwork.ConnectUsingSettings();
    }
    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
            PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnDisable()
    {
        base.OnDisable();
    }

    public override void OnConnectedToMaster()
    {
        backGround.SetActive(false);
        Debug.Log("¿¬°á ¿Ï·á");
        sceneController.SetActive(true);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("¿¬°á ²÷±è");
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = playerCount;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        SetActivePlayerImage();
        playerNumber = PhotonNetwork.PlayerList.Length;
        Debug.Log(playerNumber);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        SetActivePlayerImage();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        SetActivePlayerImage();
    }

    private void SetActivePlayerImage()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            playerImage[i].gameObject.SetActive(i < PhotonNetwork.PlayerList.Length);
        }
    }
}
