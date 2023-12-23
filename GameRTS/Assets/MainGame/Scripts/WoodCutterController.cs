using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class WoodCutterController : MonoBehaviourPunCallbacks
{
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (InteractWithCombat() == true)
            {
                return;
            }
            if (InteractWithMovement() == true)
            {
                return;
            }
            //Debug.Log("Nothing");

        }

    }
    private bool InteractWithCombat()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits)
        {
            Tree target = hit.transform.GetComponent<Tree>();
            if (target == null)
            {
                continue;
            }

            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Cutter>().Attack(target);
            }
            return true;
        }
        return false;
    }
    private bool InteractWithMovement()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
        if (hasHit)
        {
            if (Input.GetMouseButton(0))
            {
                GetComponent<MoverWoodCutter>().gameObject.GetComponent<PhotonView>().RPC("StartMoveAction", RpcTarget.All, hit.point);
                //Debug.Log("Move");
            }
            return true;

        }
        return false;
    }
    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
