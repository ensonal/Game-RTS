using System;
using System.Collections.Generic;
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
        [SerializeField] private TMP_InputField swatInputField;
        [SerializeField] private TMP_InputField archerInputField;
        [SerializeField] private TMP_InputField woodCutterInputField;
        
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
                Vector3 archerInstantiatePosition = new Vector3(71.22f, 9.99f, 67.57f);
                archerPrefabA.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                
                for(int i = 0; i < 5; i++)
                {
                    Instantiate(archerPrefabA, archerInstantiatePosition, Quaternion.Euler(0, -90, 0));
                    archerInstantiatePosition -= addedPosition;
                }
                
                HideUnitPanel();
                Debug.Log("Archer was bought.");
            }

            if (prefabName.Contains("Swat"))
            {
                Vector3 swatInstantiatePosition = new Vector3(55.86f, 9.99f, 61.09f);
                swatPrefabA.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                
                for(int i = 0; i < 5; i++)
                {
                    Instantiate(swatPrefabA, swatInstantiatePosition, Quaternion.Euler(0, -90, 0));
                    swatInstantiatePosition -= addedPosition;
                }
                
                HideUnitPanel();
                Debug.Log("Swat was bought.");
            }

            if (prefabName.Contains("Wood"))
            {
                Vector3 woodInstantiatePosition = new Vector3(71.91f, 9.99f, 74.4f);
                woodCutterPrefabA.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                if (buyAttemptCount > 0)
                {
                    woodInstantiatePosition += addedPosition2;
                    addedPosition2 += addedPosition2;
                }
                
                for(int i = 0; i < 5; i++)
                {
                    if (buyAttemptCount > 0)
                    {
                        Instantiate(woodCutterPrefabA, woodInstantiatePosition, Quaternion.Euler(0, -90, 0));
                        woodInstantiatePosition -= addedPosition;
                    }
                    else
                    {
                        Instantiate(woodCutterPrefabA, woodInstantiatePosition, Quaternion.Euler(0, -90, 0));
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
            if (user.gameObject.GetComponent<User>().coin >= unitToBuy.GetComponent<Unit>().cost)
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
    }
}