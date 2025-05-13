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
    }
}