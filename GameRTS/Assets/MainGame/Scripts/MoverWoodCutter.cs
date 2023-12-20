using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

public class MoverWoodCutter : MonoBehaviour,IAction
{
    public bool selectedFlag;

    private void Start()
    {
        selectedFlag = false;
    }

    void Update()
    {
        UpdateAnimator();
    }
    
    [PunRPC]
    public void StartMoveAction(Vector3 hit)
    {
        if (selectedFlag == true)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Cutter>().Cancel();
            GetComponent<NavMeshAgent>().destination = hit;
            GetComponent<NavMeshAgent>().isStopped = false;
        }

    }
    
    [PunRPC]
    public void MoveTo(Vector3 hit)
    {
        if (selectedFlag == true)
        {
            GetComponent<NavMeshAgent>().destination = hit;
            GetComponent<NavMeshAgent>().isStopped = false;
        }


    }

    [PunRPC]
    public void Cancel()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
    }



    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }



}