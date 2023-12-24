using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTree : MonoBehaviourPunCallbacks
{
    [SerializeField] public float health;
    
    private GameObject user;
    
    private void Start()
    {
        SetUser();
    }

    private void Update()
    {
        if (health <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
            user.GetComponent<User>().wood += 50;
        }
    }
    
    void SetUser()
    {
        int playerId = PhotonNetwork.LocalPlayer.ActorNumber;
        
        if (playerId == 1)
        {
            Debug.Log("Player 1");
            user = GameObject.FindGameObjectWithTag("TeamA");
            Debug.Log(user);
        }

        if (playerId == 2)
        {
            Debug.Log("Player 2");
            user = GameObject.FindGameObjectWithTag("TeamB");
            Debug.Log(user);
        }
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
    }
}
