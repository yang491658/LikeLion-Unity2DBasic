using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon, // ����
    Armor, // ��
    Amulet, // ��ű�
    Flask // �ö�ũ
}

[CreateAssetMenu(fileName = "EquipmentData", menuName = "Data/Equipment")]
public class EquipmentData : ItemData
{
    [Header("��� ������")]
    public EquipmentType equipmentType; // ��� Ÿ��
    public float itemCooldown; // ������ ��ٿ�
    public ItemEffect[] itemEffects; // ������ ȿ�� ���

    [Header("�⺻ ����")]
    public int strength; // �ٷ�
    public int agility; // ��ø
    public int intelligence; // ����
    public int vitality; // Ȱ��

    [Header("���� ����")]
    public int damage; // ������
    public int critical; // ġ��Ÿ ���ط�
    public int criticalChance; // ġ��Ÿ Ȯ��

    [Header("��� ����")]
    public int maxHealth; // �ִ� ü��
    public int evasion; // ȸ�Ƿ�
    public int armor; // ����
    public int resistance; // ���׷�

    [Header("���� ����")]
    public int fireDamage; // ȭ�� ������
    public int iceDamage; // ���� ������
    public int lightingDamage; // ���� ������

    [Header("���� ���")]
    public List<InventoryItem> materials; // ��� ���

    // ������ ȿ�� ���� �Լ�
    public void DoItemEffect(Transform _enemy)
    {
        foreach (var item in itemEffects) // ������ ȿ�� ���
        {
            item.DoEffect(_enemy); // ������ ȿ�� ����
        }
    }

    // ������ �߰� �Լ�
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

    // ������ ���� �Լ�
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