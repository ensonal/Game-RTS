using System.Collections;
using System.Collections.Generic;
using System.Text;
using Photon.Pun;
using UnityEngine;

public class RTSGameManager : MonoBehaviour
{
    [SerializeField] GameObject cameraParent;
    private int count = 0;

    Vector3 team1SpawnPoint = new (62.48f, 28.52f, 31.99f);
    Vector3 team2SpawnPoint = new (10.12f, 28.52f, 31.99f);

    void Start()
    {
        SpawnPlayer();
        
    }
    
    void SpawnPlayer()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
            if (PhotonNetwork.LocalPlayer.ActorNumber == 1)
            {
                PhotonNetwork.Instantiate(cameraParent.name, team1SpawnPoint, Quaternion.identity);
                cameraParent.transform.GetChild(3).gameObject.tag = "CanvasA";
                cameraParent.transform.GetChild(4).gameObject.tag = "UserA";
            }
            
            if(PhotonNetwork.LocalPlayer.ActorNumber == 2)
            {
                PhotonNetwork.Instantiate(cameraParent.name, team2SpawnPoint, Quaternion.identity);
                cameraParent.transform.GetChild(3).gameObject.tag = "CanvasB";
                cameraParent.transform.GetChild(4).gameObject.tag = "UserB";
            }
        }
        else
        {
            Debug.Log("PhotonNetwork.IsConnectedAndReady == false");
        }
    }
    

}