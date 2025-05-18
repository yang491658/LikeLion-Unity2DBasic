using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon, // 무기
    Armor, // 방어구
    Amulet, // 장신구
    Flask // 플라스크
}

[CreateAssetMenu(fileName = "EquipmentData", menuName = "Data/Equipment")]
public class EquipmentData : ItemData
{
    [Header("장비 데이터")]
    public EquipmentType equipmentType; // 장비 타입
    public float itemCooldown; // 아이템 쿨다운
    public ItemEffect[] itemEffects; // 아이템 효과 목록

    [Header("기본 스탯")]
    public int strength; // 근력
    public int agility; // 민첩
    public int intelligence; // 지능
    public int vitality; // 활력

    [Header("공격 스탯")]
    public int damage; // 데미지
    public int critical; // 치명타 피해량
    public int criticalChance; // 치명타 확률

    [Header("방어 스탯")]
    public int maxHealth; // 최대 체력
    public int evasion; // 회피력
    public int armor; // 방어력
    public int resistance; // 저항력

    [Header("마법 스탯")]
    public int fireDamage; // 화염 데미지
    public int iceDamage; // 얼음 데미지
    public int lightingDamage; // 번개 데미지

    [Header("제작 재료")]
    public List<InventoryItem> materials; // 재료 목록

    // 아이템 효과 실행 함수
    public void DoItemEffect(Transform _enemy)
    {
        foreach (var item in itemEffects) // 아이템 효과 목록
        {
            item.DoEffect(_enemy); // 아이템 효과 실행
        }
    }

    // 변경자 추가 함수
    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.AddModifier(strength);
        playerStats.agility.AddModifier(agility);
        playerStats.intelligence.AddModifier(intelligence);
        playerStats.vitality.AddModifier(vitality);

        playerStats.damage.AddModifier(damage);
        playerStats.critical.AddModifier(critical);
        playerStats.criticalChance.AddModifier(criticalChance);

        playerStats.maxHealth.AddModifier(maxHealth);
        playerStats.armor.AddModifier(armor);
        playerStats.evasion.AddModifier(evasion);
        playerStats.resistance.AddModifier(resistance);

        playerStats.fireDamage.AddModifier(fireDamage);
        playerStats.iceDamage.AddModifier(iceDamage);
        playerStats.lightingDamage.AddModifier(lightingDamage);
    }

    // 변경자 제거 함수
    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);

        playerStats.damage.RemoveModifier(damage);
        playerStats.critical.RemoveModifier(critical);
        playerStats.criticalChance.RemoveModifier(criticalChance);

        playerStats.maxHealth.RemoveModifier(maxHealth);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        playerStats.resistance.RemoveModifier(resistance);

        playerStats.fireDamage.RemoveModifier(fireDamage);
        playerStats.iceDamage.RemoveModifier(iceDamage);
        playerStats.lightingDamage.RemoveModifier(lightingDamage);
    }
}