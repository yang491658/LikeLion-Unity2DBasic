using UnityEngine;

public enum ItemType
{
    Material, // 재료
    Equipment // 장비
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    [Header("아이템 데이터")]
    public ItemType itemType; // 아이템 타입
    public Sprite itemIcon; // 아이템 아이콘
    [Range(0, 100)] public float dropChance = 10; // 드랍 확률
}