using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private Camera _camera;
    // Start is called before the first frame update
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _healthbarSprite.fillAmount = currentHealth / maxHealth;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }
}
