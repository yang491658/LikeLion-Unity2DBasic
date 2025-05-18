public class PlayerStats : CharacterStats
{
    private Player player; // �÷��̾�

    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    // ������ �ǰ� �Լ�
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        // �÷��̾� ������ ����Ʈ
        player.DamageEffect();
    }

    // ��� �Լ�
    protected override void Die()
    {
        base.Die();

        // �÷��̾� ���
        player.Die();

        // �÷��̾� ������ ���
        GetComponent<PlayerDrop>()?.GenerateDrops();
    }

    // ü�� ���� �Լ� (���)
    protected override void DecreaseHealth(int _damage)
    {
        base.DecreaseHealth(_damage);

        // �÷��̾� ��
        EquipmentData armor = Inventory.instance.GetEquipmentType(EquipmentType.Armor);
        if (armor != null) // �÷��̾� �� ����
        {
            armor.DoItemEffect(player.transform); // �� ������ ȿ�� ����
        }
    }
}