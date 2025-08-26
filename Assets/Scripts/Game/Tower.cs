using UnityEngine;

public enum TowerType { Hero, Enemy }

public class Tower : MonoBehaviour, IDamageable
{
    [Header("Tower Type")]
    public TowerType towerType;

    [Header("Health")]
    public float currentHP = 1000f;
    public float maxHP = 1000f;
    public HealthBar hpBar;

    private bool isLive;

    // IDamageable
    public bool IsAlive => isLive;

    private void Start()
    {
        isLive = true;
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        if (!isLive) return;

        currentHP -= damage;

        if (hpBar != null)
        {
            hpBar.SetHealth(currentHP, maxHP);
        }
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
    }
}
