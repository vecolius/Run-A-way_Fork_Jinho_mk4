using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;


public class PlayManager : MonoBehaviourPunCallbacks, IPunObservable
{

    public int playerCount = 0;

    public bool PlayerCondition 
    {
        get
        {
            return playerCount == PhotonNetwork.PlayerList.Length;
        }
    }

    public void Update()
    {
        Debug.Log(playerCount);
        Debug.Log(PhotonNetwork.PlayerList.Length);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerCount);
        }
        else if (stream.IsReading)
        {
            playerCount = (int)stream.ReceiveNext();
        }
    }

    [PunRPC]
    void InsertPlayer()
    {
        playerCount++;

        Debug.Log("insert");
    }

    public void Broad()
    {
        photonView.RPC("InsertPlayer", RpcTarget.All);
    }

}
