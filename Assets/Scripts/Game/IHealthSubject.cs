using System;

public interface IHealthSubject
{
    event Action<float, float> OnHealthChanged; // (current, max)
    float CurrentHP { get; }
    float MaxHP { get; }
}
