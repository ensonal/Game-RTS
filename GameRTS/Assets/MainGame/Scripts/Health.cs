﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
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
            Destroy(gameObject);
        }

        healthBar.UpdateHealthBar(maxHealth, health);
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health - damage, 0);
    }
    
    public float GetHealth()
    {
        return health;
    }
}