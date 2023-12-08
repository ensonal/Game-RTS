//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Fighter : MonoBehaviour, IAction
//{
//    [SerializeField] float timeBetweenAttacks = 1f;
//    [SerializeField] float weaponRange = 2.0f;
//    [SerializeField] float weaponDamage = 10f;

//    Transform targetObject;
//    float timeSinceLastAttack;
//    private void Start()
//    {
//        weaponRange = 2.0f;
//        weaponDamage = 10f;
//        timeBetweenAttacks = 1f;
//    }
//    private void Update()
//    {
//        timeSinceLastAttack += Time.deltaTime;

//        if (targetObject == null)
//        {
//            return;
//        }
//        bool isInRange = Vector3.Distance(transform.position, targetObject.position) < weaponRange;
//        if (isInRange == false)
//        {
//            GetComponent<Mover>().MoveTo(targetObject.position);
//        }else
//        {
//            AttackMethod();
//            GetComponent<Mover>().Cancel();
//        }
//    }
//    void Hit()
//    {
//        Health health = targetObject.GetComponent<Health>();
//        health.TakeDamage(weaponDamage);
//    }

//    private void AttackMethod()
//    {
//        if(timeSinceLastAttack > timeBetweenAttacks)
//        {
//            GetComponent<Animator>().SetTrigger("attack");
//            timeSinceLastAttack = 0;
//        }
//    }

   

//    public void Attack(CombatTarget target)
//   {
//        GetComponent<ActionScheduler>().StartAction(this);
//        targetObject = target.transform;
//        //Debug.Log("Attack is done");
//   }
//    public void Cancel()
//    {
//        targetObject = null;
//    }
//}
