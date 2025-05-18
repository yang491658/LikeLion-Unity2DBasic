using UnityEngine;

public enum StatType
{
    strength, // 근력
    agility, // 민첩
    intelligence, // 지능
    vitality, // 활력
    damage, // 데미지
    critical, // 치명타 피해량
    criticalChance, // 치명타 확률
    maxHealth, // 최대 체력
    evasion, // 회피력
    armor, // 방어력
    resistance, // 저항력
    fireDamage, // 화염 데미지
    iceDamage, // 얼음 데미지
    lightingDamage // 번개 데미지
}

[CreateAssetMenu(fileName = "BuffEffect", menuName = "Data/Effect/Buff")]
public class BuffEffect : ItemEffect
{
    private PlayerStats playerStats; // 플레이어 스탯
    [SerializeField] private StatType buffType; // 버프 타입
    [SerializeField] private int buffAmount; // 버프량
    [SerializeField] private float buffDuration; // 버프 지속시간

    // 효과 실행 함수 (상속)
    public override void DoEffect(Transform _enemy)
    {
        // 플레이어 스탯
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        // 플레이어 스탯 버프
        playerStats.BuffStat(GetStat(), buffAmount, buffDuration);
    }

    // 스탯 읽기 함수
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
