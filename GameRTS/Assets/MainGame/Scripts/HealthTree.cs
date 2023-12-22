using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTree : MonoBehaviourPunCallbacks
{
    [SerializeField] public float health;



    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
    }
}
