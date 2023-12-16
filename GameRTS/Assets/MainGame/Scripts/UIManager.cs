using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject unitButton;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject buildingButton;
    [SerializeField] private GameObject user;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject woodBar;
    [SerializeField] private GameObject coinBar;

    
    // Start is called before the first frame update
    void Start()
    {
        ShowMenuButton();
        FillTopDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        FillTopDisplay();
    }
    
    public void ShowMenuButton()
    {
        menuButton.SetActive(true);
        unitButton.SetActive(false);
        closeButton.SetActive(false);
        buildingButton.SetActive(false);
    }
    
    public void HideMenuButton()
    {
        menuButton.SetActive(false);
        unitButton.SetActive(false);
        closeButton.SetActive(false);
        buildingButton.SetActive(false);
    }
    
    public void ShowMenuOptions()
    {
        menuButton.SetActive(false);
        unitButton.SetActive(true);
        closeButton.SetActive(true);
        buildingButton.SetActive(true);
    }
    
    public void FillTopDisplay()
    {
        GameObject castle = GameObject.FindGameObjectWithTag("castleA");
        healthBar.gameObject.GetComponent<TextMeshProUGUI>().text =
            castle.gameObject.GetComponent<Health>().GetHealth().ToString();
        woodBar.gameObject.GetComponent<TextMeshProUGUI>().text = user.gameObject.GetComponent<User>().wood.ToString();
        coinBar.gameObject.GetComponent<TextMeshProUGUI>().text = user.gameObject.GetComponent<User>().coin.ToString();
    }
}
