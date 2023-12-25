using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Cutter : MonoBehaviour, IAction
{
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] float axRange = 2.0f;
    [SerializeField] float axDamage = 10f;

    Transform targetObject;
    float timeSinceLastAttack;

    private void Start()
    {
        axRange = 2.0f;
        axDamage = 10f;
        timeBetweenAttacks = 1f;
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (targetObject == null)
        {
            return;
        }

        bool isInRange = Vector3.Distance(transform.position, targetObject.position) < axRange;
        if (isInRange == false)
        {
            GetComponent<MoverWoodCutter>().gameObject.GetComponent<PhotonView>()
                .RPC("MoveTo", RpcTarget.All, targetObject.position);
        }
        else
        {
            GetComponent<PhotonView>().RPC("AttackMethod", RpcTarget.All, null);
            GetComponent<MoverWoodCutter>().gameObject.GetComponent<PhotonView>().RPC("Cancel", RpcTarget.All, null);

        }
    }

    void Hit()
    {
        var health = targetObject.gameObject.GetComponent<HealthTree>();
        health.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, axDamage);
    }

    [PunRPC]
    private void AttackMethod()
    {
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            GetComponent<Animator>().SetTrigger("attack");
            timeSinceLastAttack = 0;
        }
    }


    public void Attack(Tree target)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        targetObject = target.transform;
        //Debug.Log("Attack is done");
    }

    public void Cancel()
    {
        targetObject = null;
    }
}