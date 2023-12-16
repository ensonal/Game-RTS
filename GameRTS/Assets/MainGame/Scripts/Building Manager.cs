using System.Collections;
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
    [SerializeField] private TextMeshProUGUI[] nameTexts;
    [SerializeField] private TextMeshProUGUI[] costTexts;
    [SerializeField] private Image[] images;
    [SerializeField] private GameObject castlePrefabA;
    [SerializeField] private GameObject lumbermillPrefabA;
    [SerializeField] private GameObject archeryardPrefabA;
    [SerializeField] private GameObject castlePrefabB;
    [SerializeField] private GameObject lumbermillPrefabB;
    [SerializeField] private GameObject archeryardPrefabB;
    [SerializeField] private GameObject user;

    private List<GameObject> buildingInScene = new();
    private GameObject selectedBuilding;
    private List<Building> buildings;
    private int coroutineCountToStart = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        castlePrefabA.transform.localScale = new Vector3(4, 4, 4);
        Instantiate(castlePrefabA, new Vector3(63.7f, 9.99f, 61.1f), Quaternion.Euler(0, -90, 0));
        lumbermillPrefabA.transform.localScale = new Vector3(3, 3, 3);
        Instantiate(lumbermillPrefabA, new Vector3(75.47f, 9.99f, 74.1f), Quaternion.Euler(0, -90, 0));
        archeryardPrefabA.transform.localScale = new Vector3(3, 3, 3);
        Instantiate(archeryardPrefabA, new Vector3(75.47f, 9.99f, 66.94f), Quaternion.Euler(0, -90, 0));

        castlePrefabB.transform.localScale = new Vector3(4, 4, 4);
        Instantiate(castlePrefabB, new Vector3(2.9f, 9.99f, 65.4f), Quaternion.Euler(0, 90, 0));
        lumbermillPrefabB.transform.localScale = new Vector3(3, 3, 3);
        Instantiate(lumbermillPrefabB, new Vector3(-10.8f, 9.99f, 74.1f), Quaternion.Euler(0, 90, 0));
        archeryardPrefabB.transform.localScale = new Vector3(3, 3, 3);
        Instantiate(archeryardPrefabB, new Vector3(-12.3f, 9.99f, 58.9f), Quaternion.Euler(0, 90, 0));

        buildingInScene.Add(castlePrefabA);
        buildingInScene.Add(lumbermillPrefabA);
        buildingInScene.Add(archeryardPrefabA);
        buildingInScene.Add(castlePrefabB);
        buildingInScene.Add(lumbermillPrefabB);
        buildingInScene.Add(archeryardPrefabB);
    }

    void Update()
    {
        if (coroutineCountToStart == 0)
        {
            StartCoroutine(WaitForStartGame());
        }
    }

    public void ShowUpgradePanel()
    {
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
        buildings = new List<Building>(Resources.LoadAll<Building>("Buildings"));

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
            GameObject archeryard = GameObject.FindGameObjectWithTag("archeryrangeA");
            archeryard.gameObject.GetComponent<Health>().health = 300;
            user.gameObject.GetComponent<User>().wood -= archeryardPrefabA.GetComponent<Building>().cost;
            HideUpgradePanel();
        }

        if (prefabName.Contains("lumbermill"))
        {
            Debug.Log("Lumbermill is upgraded.");
            GameObject lumbermill = GameObject.FindGameObjectWithTag("lumbermillA");
            lumbermill.gameObject.GetComponent<Health>().health = 300;
            user.gameObject.GetComponent<User>().wood -= lumbermillPrefabA.GetComponent<Building>().cost;
            HideUpgradePanel();
        }

        if (prefabName.Contains("castle"))
        {
            Debug.Log("Castle is upgraded.");
            GameObject castle = GameObject.FindGameObjectWithTag("castleA");
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

    private IEnumerator WaitForStartGame()
    {
        yield return new WaitForSeconds(2);
        coroutineCountToStart++;
    }
}