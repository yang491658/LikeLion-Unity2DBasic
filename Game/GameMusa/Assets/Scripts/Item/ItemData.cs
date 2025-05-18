using UnityEngine;

public enum ItemType
{
    Material, // ���
    Equipment // ���
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    [Header("������ ������")]
    public ItemType itemType; // ������ Ÿ��
    public Sprite itemIcon; // ������ ������
    [Range(0, 100)] public float dropChance = 10; // ��� Ȯ��
}