using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] public new string name;
    [SerializeField] public int cost;
    [SerializeField] public Sprite sprite;
    [SerializeField] public Sprite selectedRing;
    void Start()
    {
        
    }

    private void OnDestroy()
    {
    
    }
    
}
