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

        // 플레이어 아이템 드랍
        GetComponent<PlayerDrop>()?.GenerateDrops();
    }

    // 체력 감소 함수 (상속)
    protected override void DecreaseHealth(int _damage)
    {
        base.DecreaseHealth(_damage);

        // 플레이어 방어구
        EquipmentData armor = Inventory.instance.GetEquipmentType(EquipmentType.Armor);
        if (armor != null) // 플레이어 방어구 있음
        {
            armor.DoItemEffect(player.transform); // 방어구 아이템 효과 실행
        }
    }
}