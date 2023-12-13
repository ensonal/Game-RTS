using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public new string name;
    [SerializeField] public int cost;
    [SerializeField] public Sprite sprite;
    void Start()
    {
        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }
}
