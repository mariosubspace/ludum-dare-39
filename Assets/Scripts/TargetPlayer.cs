using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetPlayer : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Player player;
    Collider playerCollider;

    public bool followPlayer = true;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        playerCollider = player.GetComponentInChildren<CapsuleCollider>();
    }

    private void Update()
    {
        if (!followPlayer) return;
        navMeshAgent.destination = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider)
        {
            navMeshAgent.destination = transform.position;
            navMeshAgent.isStopped = true;
            followPlayer = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == playerCollider)
        {
            navMeshAgent.destination = transform.position;
            navMeshAgent.isStopped = false;
            followPlayer = true;
        }
    }
}
