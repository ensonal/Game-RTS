using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHit : MonoBehaviour
{
    public Health healthComponent;
    public HealthBar healthBar;
    private float maxHealth;

    void Start()
    {
        // Sağlık bileşenini al
        healthComponent = GetComponent<Health>();
        maxHealth = healthComponent.health;

        // Eğer sağlık bileşeni yoksa, hata ver ve script'i devre dışı bırak
        if (healthComponent == null)
        {
            Debug.LogError("Health component not found on this unit. UnitHit script is disabled.");
            enabled = false;
        }

        // Sağlık çubuğunu al
        healthBar = GetComponentInChildren<HealthBar>();

        // Eğer sağlık çubuğu yoksa, hata ver ve script'i devre dışı bırak
        if (healthBar == null)
        {
            Debug.LogError("HealthBar component not found on this unit. UnitHit script is disabled.");
            enabled = false;
        }
    }

    void Update()
    {
        // "x" tuşuna basıldığında
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Rastgele bir zarar miktarı belirle (örneğin, 10)
            float damageAmount = 10f;

            // Zararı alınan birime uygula
            healthComponent.TakeDamage(damageAmount);

            // Sağlık çubuğunu güncelle
            healthBar.UpdateHealthBar(maxHealth, healthComponent.health);
        }
    }
}
