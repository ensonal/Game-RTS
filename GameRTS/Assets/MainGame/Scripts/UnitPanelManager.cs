using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MainGame.Scripts
{
    public class UnitPanelManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject createUnitPanel;
        [SerializeField] private TextMeshProUGUI[] nameTexts;
        [SerializeField] private TextMeshProUGUI[] costTexts;
        [SerializeField] private GameObject swatPrefab;
        [SerializeField] private GameObject archerPrefab;
        [SerializeField] private GameObject woodCutterPrefab;
        [SerializeField] private Image[] images;
        [SerializeField] private GameObject user;
        [SerializeField] private TextMeshProUGUI swatCount;
        [SerializeField] private TextMeshProUGUI archerCount;
        [SerializeField] private TextMeshProUGUI woodCutterCount;
        
        private List<Unit> units;
        private int buyAttemptCount = 0;
        private Vector3 addedPosition2 = new Vector3(0, 0, 1);
        private int coroutineCountToStart = 0;
        
        Vector3 addedPosition = new Vector3(1, 0, 0);
        Vector3 archerInstantiatePosition = new Vector3(0, 0, 0);
        Vector3 swatInstantiatePosition = new Vector3(0, 0, 0);
        Vector3 woodInstantiatePosition = new Vector3(0, 0, 0);
        Quaternion rotation = Quaternion.Euler(0, 90, 0);
        int playerID = 0;

        void Start()
        {
            playerID = PhotonNetwork.LocalPlayer.ActorNumber;
            SetSpawnPosition();
        }

        void SetSpawnPosition()
        {
            if (playerID == 1)
            {
                Debug.Log("Team A");
                archerInstantiatePosition = new Vector3(54.86f, 10.99f, 51.37f);
                swatInstantiatePosition = new Vector3(72.85f, 10.99f, 64.42f);
                woodInstantiatePosition = new Vector3(54.86f, 10.99f, 77.33f);
                rotation = Quaternion.Euler(0, -90, 0);
            }

            if (playerID == 2)
            {
                Debug.Log("Team B");
                archerInstantiatePosition = new Vector3(15.5f, 10.99f, 51.4f);
                swatInstantiatePosition = new Vector3(2.8f, 10.99f, 64.4f);
                woodInstantiatePosition = new Vector3(14.7f, 10.99f, 77.33f);
            }
        }

        private void BuySelectedUnit(string prefabName, int selectedUnitCost)
        {
            Debug.Log("BuySelectedUnit method is called.");
            
            if (prefabName.Contains("Archer"))
            {
                archerPrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                
                for(int i = 0; i < Int32.Parse(archerCount.text); i++)
                {
                    
                    PhotonNetwork.Instantiate(archerPrefab.name, archerInstantiatePosition, rotation);
                    archerInstantiatePosition -= addedPosition;
                }
                
                user.gameObject.GetComponent<User>().coin -= selectedUnitCost;
                
                HideUnitPanel();
                Debug.Log("Archer was bought.");
            }

            if (prefabName.Contains("Swat"))
            {
                swatPrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                
                for(int i = 0; i < Int32.Parse(swatCount.text); i++)
                {
                    Debug.Log("Swat was bought. check");
                    PhotonNetwork.Instantiate(swatPrefab.name, swatInstantiatePosition, rotation);
                    swatInstantiatePosition -= addedPosition;
                }
                
                user.gameObject.GetComponent<User>().coin -= selectedUnitCost;
                HideUnitPanel();
                Debug.Log("Swat was bought.");
            }

            if (prefabName.Contains("Wood"))
            {
                woodCutterPrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                if (buyAttemptCount > 0)
                {
                    woodInstantiatePosition += addedPosition2;
                    addedPosition2 += addedPosition2;
                }
                
                for(int i = 0; i < Int32.Parse(woodCutterCount.text); i++)
                {
                    if (buyAttemptCount > 0)
                    {
                        PhotonNetwork.Instantiate(woodCutterPrefab.name, woodInstantiatePosition, rotation);
                        woodInstantiatePosition -= addedPosition;
                    }
                    else
                    {
                        PhotonNetwork.Instantiate(woodCutterPrefab.name, woodInstantiatePosition, rotation);
                        woodInstantiatePosition -= addedPosition;
                    }
                }
                
                user.gameObject.GetComponent<User>().coin -= selectedUnitCost;
                
                buyAttemptCount++;

                HideUnitPanel();
                Debug.Log("Wood Cutter was bought.");
            }
        }

        public void CheckBalanceIsEnough(GameObject unitToBuy)
        {
            var selectedUnitCost = unitToBuy.GetComponent<Unit>().cost;
            
            if(unitToBuy.name.Contains("Wood"))
            {
                selectedUnitCost = unitToBuy.GetComponent<Unit>().cost * Int32.Parse(woodCutterCount.text);
            }

            if (unitToBuy.name.Contains("Archer"))
            {
                selectedUnitCost = unitToBuy.GetComponent<Unit>().cost * Int32.Parse(archerCount.text);
            }
            
            if (unitToBuy.name.Contains("Swat"))
            {
                selectedUnitCost = unitToBuy.GetComponent<Unit>().cost * Int32.Parse(swatCount.text);
            }
            
            Debug.Log("Total cost: " + selectedUnitCost);
            if (user.gameObject.GetComponent<User>().coin >= selectedUnitCost)
            {
                Debug.Log("Balance is enough.");
                BuySelectedUnit(unitToBuy.name, selectedUnitCost);
            }
            else
            {
                Debug.Log("Balance is not enough.");
            }
        }
        
        private void SetTagForUnit(int playerID, GameObject unit)
        {
            if (playerID == 1)
            {
                unit.tag = "TeamA";
            }
            else
            {
                unit.tag = "TeamB";
            }
        }

        public void ShowUnitPanel()
        {
            Debug.Log(createUnitPanel.name);
            createUnitPanel.SetActive(true);
            PopulateUnitPanel();
        }

        public void HideUnitPanel()
        {
            createUnitPanel.SetActive(false);
            swatCount.text = "0";
            archerCount.text = "0";
            woodCutterCount.text = "0";
        }

        private void PopulateUnitPanel()
        {
            Debug.Log("PopulateUpgradePanel method is called.");
            
            nameTexts[2].text = swatPrefab.GetComponent<Unit>().name;
            nameTexts[1].text = archerPrefab.GetComponent<Unit>().name;
            nameTexts[0].text = woodCutterPrefab.GetComponent<Unit>().name;
            
            costTexts[2].text = swatPrefab.GetComponent<Unit>().cost.ToString();
            costTexts[1].text = archerPrefab.GetComponent<Unit>().cost.ToString();
            costTexts[0].text = woodCutterPrefab.GetComponent<Unit>().cost.ToString();
            
            images[2].sprite = swatPrefab.GetComponent<Unit>().sprite;
            images[1].sprite = archerPrefab.GetComponent<Unit>().sprite;
            images[0].sprite = woodCutterPrefab.GetComponent<Unit>().sprite;
        }
        
        public void IncreaseSwatCount()
        {
            var currentSwatCount = Int32.Parse(swatCount.text);
            swatCount.text = (currentSwatCount + 1).ToString();
        }
        
        public void IncreaseArcherCount()
        {
            var currentArcherCount = Int32.Parse(archerCount.text);
            archerCount.text = (currentArcherCount + 1).ToString();
        }
        
        public void IncreaseWoodCutterCount()
        {
            Debug.Log("IncreaseWoodCutterCount method is called.");
            var currentWoodCutterCount = Int32.Parse(woodCutterCount.text);
            woodCutterCount.text = (currentWoodCutterCount + 1).ToString();
        }
        
        public void DecreaseSwatCount()
        {
            if (swatCount.text != "0")
            {
                swatCount.text = (Int32.Parse(swatCount.text) - 1).ToString();

            }
        }
        
        public void DecreaseArcherCount()
        {
            if (archerCount.text != "0")
            {
                archerCount.text = (Int32.Parse(archerCount.text) - 1).ToString();
            }
        }
        
        public void DecreaseWoodCutterCount()
        {
            if (woodCutterCount.text != "0")
            {
                woodCutterCount.text = (Int32.Parse(woodCutterCount.text) - 1).ToString();
            }
        }
    }

    
}