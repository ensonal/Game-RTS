using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();

    private static UnitSelections _instance;

    public static UnitSelections Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(_instance);
        }
        else
        {
            _instance = this;
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