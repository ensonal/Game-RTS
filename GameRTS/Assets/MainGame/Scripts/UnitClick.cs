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
                    UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }
    }
}