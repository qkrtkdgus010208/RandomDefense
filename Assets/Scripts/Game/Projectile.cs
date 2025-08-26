using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Projectile : MonoBehaviour
{
    public float speed;

    private int damage;
    private string targetTag;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Launch(int damage, string targetTag, Vector3 dir)
    {
        this.damage = damage;
        this.targetTag = targetTag;

        rigid.linearVelocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(targetTag)) return;

        IDamageable dmg = other.GetComponentInParent<IDamageable>();
        if (dmg != null && dmg.IsAlive)
        {
            dmg.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()  // 화면 밖 나가면 정리(선택)
    {
        gameObject.SetActive(false);
    }
}
