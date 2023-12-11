using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] public int cost;
    [SerializeField] public int health;
    [SerializeField] public new string name;
    [SerializeField] public Sprite sprite;

    void Start()
    {

    }
    
    void Update()
    {
        
    }
    
    public void SetTextValues(string name, int cost)
    {
        // Building prefabının içindeki Text elemanlarına erişerek değerleri atayın
        TextMeshProUGUI nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI costText = transform.Find("CostText").GetComponent<TextMeshProUGUI>();

        // Eğer Text elemanlarına erişim başarılıysa, değerleri atayın
        if (nameText != null)
            nameText.text = name;

        if (costText != null)
            costText.text = "Cost: " + cost.ToString();
    }

}
