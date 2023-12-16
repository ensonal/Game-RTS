using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RTSGameManager : MonoBehaviour
{
    [SerializeField] GameObject cameraParent;
    private int count = 0;

    public Vector3 team1SpawnPoint = new Vector3(75.47f, 19.99f, 74.1f);
    public Vector3 team2SpawnPoint = new Vector3(65.47f, 19.99f, 74.1f);

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber % 2 == 0)
            {
                PhotonNetwork.Instantiate(cameraParent.name, team2SpawnPoint, Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate(cameraParent.name, team1SpawnPoint, Quaternion.identity);
            }
        }
        else
        {
            Debug.Log("PhotonNetwork.IsConnectedAndReady == false");
        }
    }

}