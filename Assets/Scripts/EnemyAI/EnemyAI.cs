using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField]private LayerMask whatIsGround, whatIsPlayer;

    // Patrolling
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] float sightRange;
    private int currentIndex = 0;
    private bool playerInSightRange;
    //chasing
    [SerializeField] private float chaseRange;
    private bool playerInChaseRange;

    //hearing
    [SerializeField] private float hearingRange;
    private bool playerInHearingRange;


    // States
    private enum State
    {
        Patrolling,
        Chase
    }
    private State state;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.Patrolling; 
        MoveToNextWaypoint();
    }

    private void Update()
    {

        lookForPlayer();
        switch (state)
        {
            case State.Patrolling:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
        }
    }

    private void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 1.0f)
        {
            MoveToNextWaypoint();
        }

    }

    private void lookForPlayer() 
    {
        //get if player is in range for being spotted and chas
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, whatIsPlayer);
        playerInHearingRange = Physics.CheckSphere(transform.position, hearingRange, whatIsPlayer);


        if (playerInHearingRange && GameManager.Instance.IsPlayerRunning)
        {
            state = State.Chase;
        }
        else if (playerInSightRange)
        {
            state = State.Chase;
        }
        else if (!playerInChaseRange && state == State.Chase && !playerInHearingRange)
        {
            state = State.Patrolling;
            MoveToNextWaypoint();
        }
    }

    private void MoveToNextWaypoint()
    {
        if (wayPoints.Length == 0)
            return;

        agent.SetDestination(wayPoints[currentIndex].position);
        currentIndex = (currentIndex + 1) % wayPoints.Length;
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
    }
}
