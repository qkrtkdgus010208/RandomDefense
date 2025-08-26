using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform healthBar; // HP바 자식 Transform (SpriteRenderer 붙어있는 오브젝트)
    private float maxWidth;                 // 원래 길이

    private void Awake()
    {
        if (healthBar != null)
        {
            maxWidth = healthBar.localScale.x;
        }
    }

    // 외부에서 현재 HP / Max HP 넘겨주면 반영
    public void SetHealth(float current, float max)
    {
        if (healthBar == null) return;

        float ratio = Mathf.Clamp01(current / max);
        healthBar.localScale = new Vector3(maxWidth * ratio, healthBar.localScale.y, healthBar.localScale.z);
    }
}
