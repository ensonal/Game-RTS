using System.Collections;
using System.Collections.Generic;
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
            GetComponent<MoverWoodCutter>().MoveTo(targetObject.position);
        }
        else
        {
            AttackMethod();
            GetComponent<MoverWoodCutter>().Cancel();
        }
    }
    void Hit()
    {
        HealthTree health = targetObject.GetComponent<HealthTree>();
        health.TakeDamage(axDamage);
    }

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
