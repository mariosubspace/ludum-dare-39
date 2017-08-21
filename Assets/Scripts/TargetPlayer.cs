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

        if (player != null)
            playerCollider = player.GetComponentInChildren<CapsuleCollider>();
        else
            Debug.LogWarning("TargetPlayer:: Could not find player, will not target player!");
    }

    private void Update()
    {
        if (!followPlayer || player == null) return;
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
