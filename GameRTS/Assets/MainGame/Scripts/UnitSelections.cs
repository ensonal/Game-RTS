using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();

    private static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }

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
        DeselectAll();
        unitSelected.Add(unitToAdd);
        unitToAdd.GetComponent<Mover>().enabled = true;
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            unitToAdd.GetComponent<Mover>().enabled = true;
        }
        else
        {
            unitToAdd.GetComponent<Mover>().enabled = false;
            unitSelected.Remove(unitToAdd);
        }
    }

   

    public void DeselectAll()
    {
        foreach (var unit in unitSelected)
        {
            unit.GetComponent<Mover>().enabled = false;
        }
        
        unitSelected.Clear();

    }

    
}
