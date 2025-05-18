using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy; // 적

    [Header("레벨 정보")]
    [SerializeField] private int level = 1; // 레벨
    [Range(0f, 1f)][SerializeField] private float modiPer = 0.4f; // 수정 퍼센티지

    protected override void Start()
    {
        ApplyLevel(); // 레벨 적용

        base.Start();

        enemy = GetComponent<Enemy>();
    }

    // 레벨 적용 함수
    private void ApplyLevel()
    {
        ModifyStat(strength);
        ModifyStat(agility);
        ModifyStat(intelligence);
        ModifyStat(vitality);

        ModifyStat(damage);
        ModifyStat(critical);
        ModifyStat(criticalChance);

        ModifyStat(maxHealth);
        ModifyStat(evasion);
        ModifyStat(armor);
        ModifyStat(resistance);

        ModifyStat(fireDamage);
        ModifyStat(iceDamage);
        ModifyStat(lightingDamage);
    }

    // 스탯 수정 함수
    private void ModifyStat(Stat _stat)
    {
        for (int i = 1; i < level; i++) // 레벨
        {
            float modifier = _stat.GetValue() * modiPer;

            _stat.AddModifier(Mathf.RoundToInt(modifier)); // 스탯 변경자 추가
        }
    }

    // 데미지 피격 함수
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        // 적 데미지 이펙트
        enemy.DamageEffect();
    }

    // 사망 함수
    protected override void Die()
    {
        base.Die();

        // 적 사망
        enemy.Die();

        // 적 아이템 드랍
        GetComponent<EntityDrop>()?.GenerateDrops();

        // 일정시간 후 적 제거
        Destroy(enemy.gameObject, 3);
    }
}