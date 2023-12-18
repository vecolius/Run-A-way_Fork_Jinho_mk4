using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public LobbyManager instance;
    [SerializeField] GameObject characterPrafab;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] Image[] playerImage = new Image[4];
    public int playerCount;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        
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
        Debug.Log("연결 완료");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("연결 끊김");
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = playerCount;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("내가 방에 들어왔습니다.");
        SetActivePlayerImage();
        PhotonNetwork.Instantiate(characterPrafab.name, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + "방에 들어왔습니다.");
        SetActivePlayerImage();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + "방에서 나갔습니다.");
        SetActivePlayerImage();
    }

    private void SetActivePlayerImage()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            playerImage[i].gameObject.SetActive(i < PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }
}
