using System.Collections;
using UnityEngine;

public class Ghost : PowerHolder
{
    GameObject ghost;
    Transform powerSink;
    Material healthBarMat;
    GameObject healthBar;
    ParticleSystem poofSystem;

    WaitForSeconds timeToDissapearGhost;
    WaitForSeconds timeToDestroyAgent;

    public float pointToDissapearGhost = 0.1f;

    bool isDead = false;

    private void Awake()
    {
        ghost = transform.Find("Ghost").gameObject;
        powerSink = transform.Find("Power Sink");
        healthBar = transform.Find("Health Bar").gameObject;
        healthBarMat = healthBar.GetComponent<MeshRenderer>().material;
        poofSystem = transform.Find("Poof System").GetComponent<ParticleSystem>();

        timeToDissapearGhost = new WaitForSeconds(poofSystem.main.duration * pointToDissapearGhost);
        timeToDestroyAgent = new WaitForSeconds(poofSystem.main.duration * (1f - pointToDissapearGhost));
    }

    void Update()
    {
        // Don't update if the ghost is dead.
        if (isDead) return;

        healthBarMat.SetFloat("_PowerLevel", 1f - GetFillFraction());

        // Destroy when the ghost get's 'charged' to max power.
        if (currentPower >= maxPower)
        {
            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        Debug.LogFormat("Ghost:: Playing death sequence for ghost {0}.", name);
        isDead = true;

        // Play poof.
        poofSystem.Play();

        // Set ghost and health bar inactive about halfway 
        // through the poof particle system playtime.
        yield return timeToDissapearGhost;
        ghost.SetActive(false);
        healthBar.SetActive(false);

        // Wait for particle system to finish.
        yield return timeToDestroyAgent;

        // Destroy the GameObject.
        Destroy(gameObject);
    }

    public override Transform GetPowerSink()
    {
        return powerSink;
    }
}
