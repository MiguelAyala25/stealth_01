using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChasingAI : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
     
    }
    private void Update()
    {

     Chase();

    }
    private void Chase()
    {
        agent.SetDestination(player.position);
    }
}
