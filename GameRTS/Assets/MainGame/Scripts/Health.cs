using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Health : MonoBehaviourPunCallbacks
{
   
    [SerializeField] public float health;
    public HealthBar healthBar;
    private float maxHealth;
    private GameObject user;

    private void Start()
    {
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBar>();
        SetUser();
    }

    void SetUser()
    {
        int playerId = PhotonNetwork.LocalPlayer.ActorNumber;
        
        if (playerId == 1)
        {
            Debug.Log("Player 1");
            user = GameObject.FindGameObjectWithTag("UserA");
            Debug.Log(user);
        }

        if (playerId == 2)
        {
            Debug.Log("Player 2");
            user = GameObject.FindGameObjectWithTag("UserB");
            Debug.Log(user);
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
            user.GetComponent<User>().coin += 100;
        }

        healthBar.UpdateHealthBar(maxHealth, health);
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        Debug.Log("Take Damage RPC worked");
        health = Mathf.Max(health - damage, 0);
    }
    
    public float GetHealth()
    {
        return health;
    }
}