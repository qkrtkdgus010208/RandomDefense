using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform healthBar; // HP바 자식 Transform (SpriteRenderer 붙어있는 오브젝트)
    [SerializeField] private IHealthSubject subject;  // 관측 대상
    private float _maxWidth;                 // 원래 길이

    private void Awake()
    {
        if (healthBar != null)
        {
            if (healthBar != null) _maxWidth = healthBar.localScale.x;
            if (subject == null) subject = GetComponentInParent<IHealthSubject>();
        }
    }

    private void OnEnable()
    {
        if (subject != null) subject.OnHealthChanged += UpdateUnitHealthChange;
    }

    private void OnDisable()
    {
        if (subject != null) subject.OnHealthChanged -= UpdateUnitHealthChange;
    }

    // 외부에서 현재 HP / Max HP 넘겨주면 반영
    public void UpdateUnitHealthChange(float current, float max)
    {
        if (healthBar == null) return;

        float ratio = Mathf.Clamp01(current / max);
        healthBar.localScale = new Vector3(_maxWidth * ratio, healthBar.localScale.y, healthBar.localScale.z);
    }
}
