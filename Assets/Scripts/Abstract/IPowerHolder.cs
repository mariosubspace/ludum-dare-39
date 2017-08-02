
public interface IPowerHolder
{
    void GivePower(float amount);
    float TakePower(float amount);
    float GetAmountDepleted();
    float GetCurrentPower();
    float GetMaxPower();
}
