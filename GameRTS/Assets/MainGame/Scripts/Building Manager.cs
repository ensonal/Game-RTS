using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;

    [SerializeField] private GameObject upgradePanel; 
    [SerializeField] private TextMeshProUGUI balanceText; 
    [SerializeField] private TextMeshProUGUI[] nameTexts; 
    [SerializeField] private TextMeshProUGUI[] costTexts;
    [SerializeField] private Image[] images;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject initialGameObject;
    private GameObject selectedBuilding;
    private List<Building> buildings;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        initialGameObject.transform.localScale = new Vector3(4,4,4);
        Instantiate(initialGameObject, new Vector3(2.73f,4.68f,65.04f), Quaternion.Euler(0, 90, 0));
    }

    void Update()
    {
        
    }

    public void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
        UpdateBalanceText();
        PopulateUpgradePanel();
    }

    public void HideUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }
    
    private void PopulateUpgradePanel()
    {
        Debug.Log("PopulateUpgradePanel method is called.");
        buildings = new List<Building>(Resources.LoadAll<Building>("Buildings"));

        for (int i = 0; i < Mathf.Min(buildings.Count, nameTexts.Length); i++)
        {
            nameTexts[i].text = buildings[i].name;
            costTexts[i].text = buildings[i].cost.ToString();
            images[i].sprite = buildings[i].sprite;
        }
    }
    
    private void BuySelectedBuilding(string prefabName)
    {
        Debug.Log("BuySelectedBuilding method is called.");
        //PhotonNetwork.Instantiate(selectedBuilding.name, new Vector3(2.73f,4.68f,65.04f), Quaternion.Euler(0, 90, 0));
        Debug.Log(prefabName);
        GameObject newBuilding = Resources.Load<GameObject>("Assets/MainGame/Prefabs/Resources/Buildings/archeryrange.prefab");
        Debug.Log("newBuilding: " + newBuilding);
        Instantiate(newBuilding, new Vector3(2.73f,4.68f,65.04f), Quaternion.Euler(0, 90, 0));
    }

    private void UpdateBalanceText()
    {
        balanceText.text = "Balance: " + "100";
    }
    

}