using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Projectile : MonoBehaviour
{
    public float speed;

    private float damage;
    private string targetTag;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Launch(float damage, string targetTag, Vector3 dir)
    {
        this.damage = damage;
        this.targetTag = targetTag;

        rigid.linearVelocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(targetTag)) return;

        IDamageable dmg = other.GetComponentInParent<IDamageable>();
        if (dmg != null && dmg.IsLive)
        {
            dmg.TakeDamage(damage);
            gameObject.SetActive(false);

            Vector3 popupPos = other.transform.position;
            popupPos.y += 3f;
            GameObject damagePopup = GameController.Instance.damagePopupPool.Get(0);
            damagePopup.GetComponent<DamagePopup>().Init(damage);
            damagePopup.transform.position = popupPos;
        }
    }

    private void OnBecameInvisible()  // 화면 밖 나가면 정리(선택)
    {
        gameObject.SetActive(false);
    }
}
