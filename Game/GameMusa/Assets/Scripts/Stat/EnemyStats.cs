public class EnemyStats : CharacterStats
{
    private Enemy enemy; // 적

    protected override void Start()
    {
        base.Start();

        enemy = GetComponent<Enemy>();
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
    }
}