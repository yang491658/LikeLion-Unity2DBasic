using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Data/Effect/Heal")]
public class HealEffect : ItemEffect
{
    [Range(0, 100)][SerializeField] private float healPercent; // �� �ۼ�Ƽ��

    // ȿ�� ���� �Լ� (���)
    public override void DoEffect(Transform _enemy)
    {
        // �÷��̾� ����
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        // ���� = �÷��̾� �ִ� ü�� x �� �ۼ�Ƽ��
        int healAmount = Mathf.RoundToInt(playerStats.GetMaxHealth() * healPercent / 100);

        // �÷��̾� ü�� ����
        playerStats.IncreaseHealth(healAmount);
    }
}
