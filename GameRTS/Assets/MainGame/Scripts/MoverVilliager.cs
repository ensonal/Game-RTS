using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoverVilliager : MonoBehaviour
{
    Ray myRay;


    public bool selectedFlag;

    private void Start()
    {
        selectedFlag = GetComponent<MoverVilliager>().selectedFlag;
    }



    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (selectedFlag == true)
            {
                MoveToCursor();
            }


        }

        if (selectedFlag == true)
        {
            UpdateAnimator();
        }



    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}