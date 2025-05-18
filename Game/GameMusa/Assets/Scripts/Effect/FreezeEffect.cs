using UnityEngine;

[CreateAssetMenu(fileName = "FreezeEffect", menuName = "Data/Effect/Freeze")]
public class FreezeEffect : ItemEffect
{
    [SerializeField] private float freezeDuration; // ���� ���ӽð�

    // ȿ�� ���� �Լ� (���)
    public override void DoEffect(Transform _transform)
    {
        // �÷��̾� ����
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (playerStats.currentHealth >= playerStats.GetMaxHealth() * 0.1f ||
            // �÷��̾� ���� ü�� = �ִ� ü���� 10% �̻�
            !Inventory.instance.UseArmor()) // �� ��� ����
        {
            return; // ����
        }

        // �ݶ��̴� ���� = ���� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 2);

        foreach (var hit in colliders)
        {
            // �� �ð� ����
            hit.GetComponent<Enemy>()?.StartCoroutine("FreezeTimeCoruntine", freezeDuration);
        }
    }
}
