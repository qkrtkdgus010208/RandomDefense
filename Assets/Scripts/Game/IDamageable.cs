public interface IDamageable
{
    bool IsLive { get; }
    void TakeDamage(float damage);
}
