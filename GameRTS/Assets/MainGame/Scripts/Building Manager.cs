using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private TextMeshProUGUI[] nameTexts;
    [SerializeField] private TextMeshProUGUI[] costTexts;
    [SerializeField] private Image[] images;
    [SerializeField] private GameObject castlePrefabA;
    [SerializeField] private GameObject lumbermillPrefabA;
    [SerializeField] private GameObject archeryardPrefabA;
    [SerializeField] private GameObject user;

    private GameObject selectedBuilding;
    private List<Building> buildings;
    private string teamTag;
    void Start()
    {
        buildings = new List<Building>();
        buildings.Add(lumbermillPrefabA.GetComponent<Building>());
        buildings.Add(archeryardPrefabA.GetComponent<Building>());
        buildings.Add(castlePrefabA.GetComponent<Building>());
        SetTeamTag();
    }

    void SetTeamTag()
    {
        if(PhotonNetwork.LocalPlayer.ActorNumber == 1)
        {
            teamTag = "A";
        }
        else
        {
            teamTag = "B";
        }
    }
    
    public void ShowUpgradePanel()
    {
        Debug.Log(upgradePanel.name + " is activated.");
        upgradePanel.SetActive(true);
        PopulateUpgradePanel();
    }

    public void HideUpgradePanel()
    {
        upgradePanel.SetActive(false);
    }

    private void PopulateUpgradePanel()
    {
        Debug.Log("PopulateUpgradePanel method is called.");

        for (int i = 0; i < Mathf.Min(buildings.Count, nameTexts.Length); i++)
        {
            nameTexts[i].text = buildings[i].name;
            costTexts[i].text = buildings[i].cost.ToString();
            images[i].sprite = buildings[i].sprite;
        }
    }

    private void UpgradeSelectedBuilding(string prefabName)
    {
        Debug.Log("UpgradeSelectedBuilding method is called.");
        Debug.Log("Selected building name: " + prefabName);
        //PhotonNetwork.Instantiate(selectedBuilding.name, new Vector3(2.73f,4.68f,65.04f), Quaternion.Euler(0, 90, 0));
        if (prefabName.Contains("archer"))
        {
            Debug.Log("Archeryrange is upgraded.");
            GameObject archeryard = GameObject.Find("archeryrange" + teamTag);
            archeryard.gameObject.GetComponent<Health>().health = 300;
            user.gameObject.GetComponent<User>().wood -= archeryardPrefabA.GetComponent<Building>().cost;
            HideUpgradePanel();
        }

        if (prefabName.Contains("lumbermill"))
        {
            Debug.Log("Lumbermill is upgraded.");
            GameObject lumbermill = GameObject.Find("lumbermill" + teamTag);
            lumbermill.gameObject.GetComponent<Health>().health = 300;
            user.gameObject.GetComponent<User>().wood -= lumbermillPrefabA.GetComponent<Building>().cost;
            HideUpgradePanel();
        }

        if (prefabName.Contains("castle"))
        {
            Debug.Log("Castle is upgraded.");
            GameObject castle = GameObject.Find("castle" + teamTag);
            castle.gameObject.GetComponent<Health>().health = 500;
            user.gameObject.GetComponent<User>().wood -= castlePrefabA.GetComponent<Building>().cost;
            HideUpgradePanel();
        }
    }

    public void CheckBalanceIsEnough(GameObject selectedBuilding)
    {
        if (user.gameObject.GetComponent<User>().wood >= selectedBuilding.GetComponent<Building>().cost)
        {
            Debug.Log("Balance is enough.");
            UpgradeSelectedBuilding(selectedBuilding.name);
        }
        else
        {
            Debug.Log("Balance is not enough.");
        }
    }

   
}