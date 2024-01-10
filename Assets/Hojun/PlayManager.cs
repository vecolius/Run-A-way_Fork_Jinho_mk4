using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;


public class PlayManager : MonoBehaviourPunCallbacks
{

    public int playerCount = 0;

    public bool PlayerCondition 
    {
        get
        {
            return playerCount == PhotonNetwork.PlayerList.Length;
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
