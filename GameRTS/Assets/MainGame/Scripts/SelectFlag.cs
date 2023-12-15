using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFlag : MonoBehaviour
{
    public bool selectedFlag;

    private void Start()
    {
        selectedFlag = false;
    }

    private void OnMouseDown()
    {
        selectedFlag = true;
    }
}
