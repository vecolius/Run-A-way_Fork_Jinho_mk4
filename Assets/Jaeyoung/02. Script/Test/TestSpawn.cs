using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestSpawn : MonoBehaviour
{
    [SerializeField] GameObject characterPrefab;

    private void Start()
    {
        PhotonNetwork.Instantiate(characterPrefab.name, transform.position, transform.rotation);
    }
}
