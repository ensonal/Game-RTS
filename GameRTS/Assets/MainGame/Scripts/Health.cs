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

    private void Start()
    {
        maxHealth = health;
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
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