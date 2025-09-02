using System;
using UnityEngine;

public enum TowerType { Hero, Enemy }

public class Tower : MonoBehaviour, IDamageable, IHealthSubject
{
    [Header("Tower Type")]
    public TowerType towerType;

    [Header("Health")]
    public float currentHP = 1000f;
    public float maxHP = 1000f;

    private bool isLive;

    // IDamageable
    public bool IsLive => isLive;

    // IHealthSubject
    public float CurrentHP => currentHP;

    public float MaxHP => maxHP;

    public event Action<float, float> OnHealthChanged;

    private void OnEnable()
    {
        isLive = true;
        currentHP = maxHP;

        UpdateHealthChanged();
    }

    public void TakeDamage(float damage)
    {
        if (!isLive) return;

        currentHP -= damage;

        UpdateHealthChanged();

        if (currentHP <= 0f)
        {
            currentHP = 0f;
            Die();
        }
    }

    private void Die()
    {
        isLive = false;

        if (towerType == TowerType.Hero)
        {
            GameController.Instance.GameOver();
        }
        else if (towerType == TowerType.Enemy)
        {
            GameController.Instance.GameVictory();
        }

        UpdateHealthChanged();
    }

    private void UpdateHealthChanged()
    {
        OnHealthChanged?.Invoke(currentHP, maxHP);
    }
}
