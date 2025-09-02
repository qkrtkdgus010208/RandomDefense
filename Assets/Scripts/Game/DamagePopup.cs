using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmp;

    public void Init(float damage)
    {
        tmp.text = damage.ToString("F0");
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
