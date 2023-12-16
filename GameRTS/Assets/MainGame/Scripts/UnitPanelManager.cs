using System;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainGame.Scripts
{
    public class UnitPanelManager : MonoBehaviour
    {
        [SerializeField] private GameObject createUnitPanel;
        [SerializeField] private TextMeshProUGUI[] nameTexts;
        [SerializeField] private TextMeshProUGUI[] costTexts;
        [SerializeField] private GameObject swatPrefabA;
        [SerializeField] private GameObject archerPrefabA;
        [SerializeField] private GameObject woodCutterPrefabA;
        [SerializeField] private Image[] images;
        [SerializeField] private GameObject user;
        [SerializeField] private TextMeshProUGUI swatCount;
        [SerializeField] private TextMeshProUGUI archerCount;
        [SerializeField] private TextMeshProUGUI woodCutterCount;
        
        private List<Unit> units;
        private int buyAttemptCount = 0;
        private Vector3 addedPosition2 = new Vector3(0, 0, 1);

        void Start()
        {

        }

        void Update()
        {
        }

        private void BuySelectedUnit(string prefabName)
        {
            Debug.Log("BuySelectedUnit method is called.");
            //PhotonNetwork.Instantiate(selectedBuilding.name, new Vector3(2.73f,4.68f,65.04f), Quaternion.Euler(0, 90, 0));
            Vector3 addedPosition = new Vector3(1, 0, 0);
            
            if (prefabName.Contains("Archer"))
            {
                Vector3 archerInstantiatePosition = new Vector3(71.22f, 10.99f, 67.57f);
                archerPrefabA.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                
                for(int i = 0; i < Int32.Parse(archerCount.text); i++)
                {
                    PhotonNetwork.Instantiate(archerPrefabA.name, archerInstantiatePosition, Quaternion.Euler(0, -90, 0));
                    user.gameObject.GetComponent<User>().coin -= archerPrefabA.GetComponent<Unit>().cost;
                    archerInstantiatePosition -= addedPosition;
                }
                
                HideUnitPanel();
                Debug.Log("Archer was bought.");
            }

            if (prefabName.Contains("Swat"))
            {
                Vector3 swatInstantiatePosition = new Vector3(55.86f, 10.99f, 61.09f);
                swatPrefabA.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                
                for(int i = 0; i < Int32.Parse(swatCount.text); i++)
                {
                    PhotonNetwork.Instantiate(swatPrefabA.name, swatInstantiatePosition, Quaternion.Euler(0, -90, 0));
                    user.gameObject.GetComponent<User>().coin -= archerPrefabA.GetComponent<Unit>().cost;
                    swatInstantiatePosition -= addedPosition;
                }
                
                HideUnitPanel();
                Debug.Log("Swat was bought.");
            }

            if (prefabName.Contains("Wood"))
            {
                Vector3 woodInstantiatePosition = new Vector3(71.91f, 10.99f, 74.4f);
                woodCutterPrefabA.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                if (buyAttemptCount > 0)
                {
                    woodInstantiatePosition += addedPosition2;
                    user.gameObject.GetComponent<User>().coin -= archerPrefabA.GetComponent<Unit>().cost;
                    addedPosition2 += addedPosition2;
                }
                
                for(int i = 0; i < Int32.Parse(woodCutterCount.text); i++)
                {
                    if (buyAttemptCount > 0)
                    {
                        PhotonNetwork.Instantiate(woodCutterPrefabA.name, woodInstantiatePosition, Quaternion.Euler(0, -90, 0));
                        woodInstantiatePosition -= addedPosition;
                    }
                    else
                    {
                        PhotonNetwork.Instantiate(woodCutterPrefabA.name, woodInstantiatePosition, Quaternion.Euler(0, -90, 0));
                        woodInstantiatePosition -= addedPosition;
                    }
                    

                }
                
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
                BuySelectedUnit(unitToBuy.name);
            }
            else
            {
                Debug.Log("Balance is not enough.");
            }
        }

        public void ShowUnitPanel()
        {
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
            units = new List<Unit>(Resources.LoadAll<Unit>("Units"));
            
            units.Sort((x, y) => x.cost.CompareTo(y.cost));

            for (int i = 0; i < Mathf.Min(units.Count, nameTexts.Length); i++)
            {
                Debug.Log("units.Count: " + units[i]);
                nameTexts[i].text = units[i].name;
                costTexts[i].text = units[i].cost.ToString();
                images[i].sprite = units[i].sprite;
            }
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