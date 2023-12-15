using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject missionManager;

    private void Start()
    {
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = "TEST";
    }

    private void Update()
    {
        if (PhotonNetwork.IsConnected == false)
            return;

        Debug.Log("최대 인원 수" + PhotonNetwork.CurrentRoom.MaxPlayers);
        Debug.Log("현재 인원 수" + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            missionManager.SetActive(true);
        }
    }


    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("조인룸 누름");
            PhotonNetwork.JoinRandomRoom();
        }
        else
            PhotonNetwork.ConnectUsingSettings();

    }

    //MonoBehaviourPunCallbacks는 MonoBehaviour이므로 (상속관계)
    //유니티에서 제공하는 이벤트 함수 OnEnable과 OnDisable을 사용하였고, 구현해두었으니,
    //만약 MonoBehaviourPunCallbacks을 상속받았고 해당 이벤트 함수를 사용하려면 꼭 override할것.
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
        Debug.Log("OnConnectedToMaster() 호출 됨. 연결 됨");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected() 호출 됨. 연결이 끊어짐");
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방이없어요, 혹은 들어갈 수 없었어요");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("내가 방에 들어왔습니다.");
        //PhotonNetwork.Instantiate(characterPrafab.name, transform.position, transform.rotation);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + "방에 들어왔습니다.");

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + "방에서 나갔습니다.");
    }
}
