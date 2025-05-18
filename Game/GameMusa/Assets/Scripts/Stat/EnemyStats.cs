using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy; // ��

    [Header("���� ����")]
    [SerializeField] private int level = 1; // ����
    [Range(0f, 1f)][SerializeField] private float modiPer = 0.4f; // ���� �ۼ�Ƽ��

    protected override void Start()
    {
        ApplyLevel(); // ���� ����

        base.Start();

        enemy = GetComponent<Enemy>();
    }

    // ���� ���� �Լ�
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

    // ���� ���� �Լ�
    private void ModifyStat(Stat _stat)
    {
        for (int i = 1; i < level; i++) // ����
        {
            float modifier = _stat.GetValue() * modiPer;

            _stat.AddModifier(Mathf.RoundToInt(modifier)); // ���� ������ �߰�
        }
    }

    // ������ �ǰ� �Լ�
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        // �� ������ ����Ʈ
        enemy.DamageEffect();
    }

    // ��� �Լ�
    protected override void Die()
    {
        base.Die();

        // �� ���
        enemy.Die();

        // �� ������ ���
        GetComponent<EntityDrop>()?.GenerateDrops();

        // �����ð� �� �� ����
        Destroy(enemy.gameObject, 3);
    }
}