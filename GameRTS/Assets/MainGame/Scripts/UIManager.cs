using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuButton;
    [SerializeField] private GameObject castleButton;
    [SerializeField] private GameObject unitButton;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject buildingButton;

    
    // Start is called before the first frame update
    void Start()
    {
        ShowMenuButton();
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void ShowMenuButton()
    {
        menuButton.SetActive(true);
        castleButton.SetActive(false);
        unitButton.SetActive(false);
        closeButton.SetActive(false);
        buildingButton.SetActive(false);
    }
    
    public void HideMenuButton()
    {
        menuButton.SetActive(false);
    }
    
    public void ShowMenuOptions()
    {
        menuButton.SetActive(false);
        castleButton.SetActive(true);
        unitButton.SetActive(true);
        closeButton.SetActive(true);
        buildingButton.SetActive(true);
    }
}
