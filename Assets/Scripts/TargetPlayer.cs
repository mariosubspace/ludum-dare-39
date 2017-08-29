using UnityEngine;
using UnityEngine.AI;

public class TargetPlayer : MonoBehaviour
{
    public float attackStrength = 5f;

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
            // Stop the ghost if colliding with player.
            navMeshAgent.destination = transform.position;
            navMeshAgent.isStopped = true;
            followPlayer = false;

            // Attack the player.
            player.HurtPlayer(attackStrength);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == playerCollider)
        {
            // Start the ghost following player again.
            navMeshAgent.destination = transform.position;
            navMeshAgent.isStopped = false;
            followPlayer = true;
        }
    }
}
