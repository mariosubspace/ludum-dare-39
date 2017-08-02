using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetPlayer : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Player player;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        navMeshAgent.destination = player.transform.position;
    }
}
