using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetPlayer : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Player player;

    public bool followPlayer = true;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (!followPlayer) return;
        navMeshAgent.destination = player.transform.position;
    }
}
