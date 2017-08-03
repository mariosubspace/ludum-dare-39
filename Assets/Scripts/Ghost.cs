
public class Ghost : PowerHolder
{
    void Update()
    {
        // Destroy when the ghost get's 'charged' to max power.
        if (currentPower >= maxPower)
        {
            Destroy(gameObject);
        }
    }
}
