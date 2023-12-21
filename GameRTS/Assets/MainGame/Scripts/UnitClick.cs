using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class UnitClick : MonoBehaviourPunCallbacks
{
    public GameObject cameraParent;

    public LayerMask clickable;
    public LayerMask ground;

    private Camera myCam;
    private PhotonView pw;
    public List<GameObject> unitSelected = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        myCam = cameraParent.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                Debug.Log("Clicked on " + hit.collider.gameObject.name);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Debug.Log("Shift click selected çalıştı");
                    ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    Debug.Log("Click selected çalıştı");
                    ClickSelect(hit.collider.gameObject);
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    DeselectAll();
                    Debug.Log("Deselecting all");
                }
            }
        }
    }
    
    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            if (unitToAdd.GetComponent<Mover>() != null)
            {
                unitSelected.Add(unitToAdd);
                unitToAdd.GetComponent<Mover>().selectedFlag = true;
                unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (unitToAdd.GetComponent<MoverWoodCutter>() != null)
            {
                unitSelected.Add(unitToAdd);
                unitToAdd.GetComponent<MoverWoodCutter>().selectedFlag = true;
                unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            if (unitToAdd.GetComponent<Mover>() != null)
            {
                unitToAdd.GetComponent<Mover>().selectedFlag = false;
                unitSelected.Remove(unitToAdd);
            }
            else if (unitToAdd.GetComponent<MoverWoodCutter>() != null)
            {
                unitToAdd.GetComponent<MoverWoodCutter>().selectedFlag = false;
                unitSelected.Remove(unitToAdd);
            }
        }
    }
    
    public void ClickSelect(GameObject unitToAdd)
    {
        if (unitToAdd.GetComponent<Mover>() != null)
        {
            DeselectAll();
            unitSelected.Add(unitToAdd);
            unitToAdd.GetComponent<Mover>().selectedFlag = true;
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (unitToAdd.GetComponent<MoverWoodCutter>() != null)
        {
            DeselectAll();
            unitSelected.Add(unitToAdd);
            unitToAdd.GetComponent<MoverWoodCutter>().selectedFlag = true;
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    
    public void DeselectAll()
    {
        foreach (var unit in unitSelected)
        {
            if (unit.GetComponent<Mover>() != null)
            {
                unit.GetComponent<Mover>().selectedFlag = false;
                unit.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (unit.GetComponent<MoverWoodCutter>() != null)
            {
                unit.GetComponent<MoverWoodCutter>().selectedFlag = false;
                unit.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        unitSelected.Clear();
    }
}