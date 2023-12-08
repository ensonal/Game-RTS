using Photon.Pun;
using UnityEngine;

public class User : MonoBehaviourPun
{
    [SerializeField] public int balance;

    void Start()
    {
        balance = 1000;
    }

    void Update()
    {
        // You can add more logic here if needed
    }
    
}