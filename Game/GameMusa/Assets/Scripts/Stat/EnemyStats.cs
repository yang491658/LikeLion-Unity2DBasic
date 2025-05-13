public class EnemyStats : CharacterStats
{
    private Enemy enemy; // ��

    protected override void Start()
    {
        base.Start();

        enemy = GetComponent<Enemy>();
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
    }
}