using UnityEngine;

public enum StatType
{
    strength, // �ٷ�
    agility, // ��ø
    intelligence, // ����
    vitality, // Ȱ��
    damage, // ������
    critical, // ġ��Ÿ ���ط�
    criticalChance, // ġ��Ÿ Ȯ��
    maxHealth, // �ִ� ü��
    evasion, // ȸ�Ƿ�
    armor, // ����
    resistance, // ���׷�
    fireDamage, // ȭ�� ������
    iceDamage, // ���� ������
    lightingDamage // ���� ������
}

[CreateAssetMenu(fileName = "BuffEffect", menuName = "Data/Effect/Buff")]
public class BuffEffect : ItemEffect
{
    private PlayerStats playerStats; // �÷��̾� ����
    [SerializeField] private StatType buffType; // ���� Ÿ��
    [SerializeField] private int buffAmount; // ������
    [SerializeField] private float buffDuration; // ���� ���ӽð�

    // ȿ�� ���� �Լ� (���)
    public override void DoEffect(Transform _enemy)
    {
        // �÷��̾� ����
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        // �÷��̾� ���� ����
        playerStats.BuffStat(GetStat(), buffAmount, buffDuration);
    }

    // ���� �б� �Լ�
    private Stat GetStat()
    {
        if (buffType == StatType.strength) return playerStats.strength;
        else if (buffType == StatType.agility) return playerStats.agility;
        else if (buffType == StatType.intelligence) return playerStats.intelligence;
        else if (buffType == StatType.vitality) return playerStats.vitality;
        else if (buffType == StatType.damage) return playerStats.damage;
        else if (buffType == StatType.critical) return playerStats.critical;
        else if (buffType == StatType.criticalChance) return playerStats.criticalChance;
        else if (buffType == StatType.maxHealth) return playerStats.maxHealth;
        else if (buffType == StatType.armor) return playerStats.armor;
        else if (buffType == StatType.evasion) return playerStats.evasion;
        else if (buffType == StatType.resistance) return playerStats.resistance;
        else if (buffType == StatType.fireDamage) return playerStats.fireDamage;
        else if (buffType == StatType.iceDamage) return playerStats.iceDamage;
        else if (buffType == StatType.lightingDamage) return playerStats.lightingDamage;

        return null;
    }
}
