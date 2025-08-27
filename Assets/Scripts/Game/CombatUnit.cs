using UnityEngine;

public enum UnitSide { Hero, Enemy }            // 어느 편인지
public enum UnitClass { Warrior, Mage, Archer } // 전사 / 마법사 / 궁수
public enum UnitRank { Normal, Rare, Epic, Legendary, Mythic, Boss }             // 기본 유닛 / 보스

public class CombatUnit : MonoBehaviour, IDamageable
{
    [Header("Identity")]
    public UnitSide side = UnitSide.Enemy;
    public UnitClass unitClass = UnitClass.Warrior;
    public UnitRank unitRank = UnitRank.Normal;

    [Header("Movement")]
    public float speed = 5f; // 전사 5, 법사 3, 궁수 7, 신화 +2, 보스 -2  

    [Header("Health")]
    public float currentHP = 100f;
    public float maxHP = 100f;
    /*
    Warrior : 150 / 195 / 255 / 330 / 450 / '2250'
    Mage : 80 / 104 / 136 / 176 / 240 / '1200'
    Archer : 110 / 143 / 187 / 242 / 330 / '1650'
    */
    public HealthBar hpBar;

    [Header("Attack Common")]
    public float attackDamage = 10;
    /*
    Warrior : 9 / 11 / 14 / 17 / 23 / '69'
    Mage : 33 / 40 / 50 / 63 / 83 / '249'
    Archer : 20 / 24 / 30 / 38 / 50 / '150'
    */
    public float attackInterval = 1.0f; // 전사 1, 법사 3, 궁수 2
    public float attackRange = 2f;      // 전사 2(신화는 3), 법사 10, 궁수 15
    public Transform firePoint;         // 투사체 발사 위치(원거리용)

    [Header("Projectile (for Archer/Mage)")]
    public Projectile projectilePrefab; // 투사체 프리팹(아처/메이지용)

    // 내부 상태
    private bool isLive, isCombat;
    public Transform currentTarget; // 공격 타겟
    private float nextAttackTime;

    // IDamageable
    public bool IsAlive => isLive;

    // 캐시
    private Rigidbody2D rigid;
    private Collider2D coll;
    private Animator anim;
    private Scanner scanner;

    // 타겟 태그(상대편)
    private string TargetTag => (side == UnitSide.Hero) ? "Enemy" : "Hero";

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    private void OnEnable()
    {
        isLive = true;
        isCombat = false;
        currentTarget = null;
        nextAttackTime = 0f;

        rigid.simulated = true;
        coll.enabled = true;

        anim?.ResetTrigger("2_Attack");
        anim?.SetBool("4_Death", false);
        anim?.SetTrigger("1_Move");

        currentHP = maxHP;

        // 한 프레임 뒤에 HPBar 세팅 (HealthBar 초기화 보장)
        Invoke(nameof(InitHPBar), 0f);
    }

    private void InitHPBar()
    {
        hpBar?.SetHealth(currentHP, maxHP);
    }

    private void FixedUpdate()
    {
        if (!GameController.Instance.isPlay || !isLive) return;
        if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        if (scanner == null || scanner.nearestTarget == null)
        {
            // 타겟을 아직 못 찾은 첫 프레임들
            return;
        }

        // 사거리 체크 → 사거리 내면 전투 진입
        TryEnterCombatByRange();

        if (isCombat) // 전투 로직
        {
            rigid.linearVelocity = Vector2.zero;

            // 현재 타겟이 없거나 달라지면 전투 종료
            if (currentTarget == null || currentTarget != scanner.nearestTarget)
            {
                ExitCombat();
                return;
            }

            if (Time.time >= nextAttackTime)
            {
                anim?.SetTrigger("2_Attack");
                nextAttackTime = Time.time + attackInterval;
            }
            return;
        }

        // 이동 로직(X축만) – 스캐너가 잡아준 가장 가까운 타겟쪽으로
        if (scanner != null && scanner.nearestTarget != null)
        {
            float dirX = scanner.nearestTarget.position.x - rigid.position.x;
            if (Mathf.Abs(dirX) > 0.1f)
            {
                Vector2 nextVec = new Vector2(Mathf.Sign(dirX), 0f) * speed * Time.fixedDeltaTime;
                rigid.MovePosition(rigid.position + nextVec);
            }
            else
            {
                rigid.linearVelocity = Vector2.zero;
            }
        }
    }

    private void TryEnterCombatByRange()
    {
        float distX = Mathf.Abs(scanner.nearestTarget.position.x - transform.position.x);
        bool inRange = distX <= attackRange;
        if (inRange)
        {
            Transform target = scanner.nearestTarget;
            if (target == null || !target.CompareTag(TargetTag)) return;
            EnterCombat(target);
        }
    }

    private bool IsTargetAlive(Transform t)
    {
        if (t == null) return false;
        IDamageable dmg = t.GetComponent<IDamageable>();
        return (dmg != null && dmg.IsAlive);
    }

    private void EnterCombat(Transform target)
    {
        isCombat = true;
        currentTarget = target;
        rigid.linearVelocity = Vector2.zero;
        anim?.ResetTrigger("1_Move");
    }

    private void ExitCombat()
    {
        isCombat = false;
        currentTarget = null;
        anim?.ResetTrigger("2_Attack");
        anim?.SetTrigger("1_Move");
    }

    // IDamageable
    public void TakeDamage(float damage)
    {
        if (!isLive) return;

        currentHP -= damage;

        hpBar?.SetHealth(currentHP, maxHP);

        if (currentHP <= 0f)
        {
            currentHP = 0f;
            Die();
        }
    }

    // 애니메이션 이벤트(타격 프레임)에서 호출
    // Warrior: 직접 히트 / Archer/Mage: 투사체 발사
    private void ApplyHit()
    {
        if (!isCombat || currentTarget == null) return;

        switch (unitClass)
        {
            case UnitClass.Warrior:
                {
                    IDamageable dmg = currentTarget.GetComponent<IDamageable>();
                    if (dmg != null && dmg.IsAlive) dmg.TakeDamage(attackDamage);
                    break;
                }
            case UnitClass.Archer:
                {
                    if (projectilePrefab == null || firePoint == null) return;

                    Vector3 targetPos = currentTarget.position;
                    targetPos.y += 1f; // 타겟 위치를 몸통 중앙으로 조정
                    Vector3 dir = (targetPos - firePoint.position).normalized;

                    Transform projectile = GameController.Instance.projectilePool.Get(0).transform;
                    projectile.position = firePoint.position;
                    projectile.GetComponent<Projectile>().Launch(attackDamage, TargetTag, dir);
                    break;
                }

            case UnitClass.Mage:
                {
                    if (projectilePrefab == null || firePoint == null) return;

                    Vector3 targetPos = currentTarget.position;
                    targetPos.y += 1f; // 타겟 위치를 몸통 중앙으로 조정
                    Vector3 dir = (targetPos - firePoint.position).normalized;

                    Transform projectile = GameController.Instance.projectilePool.Get(1).transform;
                    projectile.position = firePoint.position;
                    projectile.GetComponent<Projectile>().Launch(attackDamage, TargetTag, dir);
                    break;
                }
        }
    }

    private void Die()
    {
        isLive = false;
        rigid.simulated = false;
        coll.enabled = false;
        anim?.SetBool("4_Death", true);

        if (side == UnitSide.Enemy)
        {
            GameController.Instance.gold += 50;
            GameController.Instance.coin += 1;
        }
        else if (unitRank == UnitRank.Boss)
        {
            GameController.Instance.gold += 500;
            GameController.Instance.coin += 10;
        }
    }

    // 애니 클립 끝 이벤트
    private void Dead()
    {
        anim?.Rebind(); // 오브젝트 풀을 이용하면 발생하는 스펌 애니메이션 문제 제거
        transform.parent.gameObject.SetActive(false);
    }
}
