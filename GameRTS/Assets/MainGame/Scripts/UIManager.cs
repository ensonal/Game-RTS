using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviourPunCallbacks
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
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Wait());
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
        GameObject castle = null;
        int playerID = PhotonNetwork.LocalPlayer.ActorNumber;
        if (playerID == 1)
        {
             castle = GameObject.FindGameObjectWithTag("castleA");
        }
        
        if (playerID == 2)
        {
             castle = GameObject.FindGameObjectWithTag("castleB");
        }

        //Debug.Log(healthBar.gameObject.GetComponent<TextMeshProUGUI>().text);
        
        healthBar.gameObject.GetComponent<TextMeshProUGUI>().text =
            castle.gameObject.GetComponent<Health>().health.ToString();
        woodBar.gameObject.GetComponent<TextMeshProUGUI>().text = user.gameObject.GetComponent<User>().wood.ToString();
        coinBar.gameObject.GetComponent<TextMeshProUGUI>().text = user.gameObject.GetComponent<User>().coin.ToString();
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
    }
}
