public class PlayerStats : CharacterStats
{
    private Player player; // 플레이어

    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    // 데미지 피격 함수
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        // 플레이어 데미지 이펙트
        player.DamageEffect();
    }

    // 사망 함수
    protected override void Die()
    {
        base.Die();

        // 플레이어 사망
        player.Die();
    }
}