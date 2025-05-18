using System.Collections.Generic;
using UnityEngine;

public class PlayerDrop : EntityDrop
{
    [Header("플레이어 드랍")]
    [SerializeField] private float equipDropChance; // 장비 드랍 확률
    [SerializeField] private float matDropChance; // 재료 드랍 확률

    // 드랍 생성 함수 (상속)
    public override void GenerateDrops()
    {
        Inventory inventory = Inventory.instance; // 인벤토리

        List<InventoryItem> equipDrops = new List<InventoryItem>(); // 장비 드랍 목록

        foreach (InventoryItem item in inventory.GetEquip()) // 인베토리 장비 목록
        {
            if (Random.Range(0, 100) < equipDropChance) // 장비 드랍 확률
            {
                DropItem(item.data); // 아이템 드랍

                equipDrops.Add(item); // 장비 드랍 항목 추가
            }
        }

        for (int i = 0; i < equipDrops.Count; i++) // 장비 드랍 목록
        {
            inventory.UnequipItem(equipDrops[i].data as EquipmentData); // 인벤토리 장비 해제

            inventory.RemoveItem(equipDrops[i].data); // 인벤토리 아이템 제거
        }

        List<InventoryItem> matDrops = new List<InventoryItem>(); // 재료 드랍 목록

        foreach (InventoryItem item in inventory.GetStash()) // 인벤토리 재료 목록
        {
            if (Random.Range(0, 100) < matDropChance) // 재료 드랍 확률
            {
                DropItem(item.data); // 아이템 드랍

                matDrops.Add(item); // 재료 드랍 항목 추가
            }
        }

        for (int i = 0; i < matDrops.Count; i++) // 재료 드랍 목록
        {
            inventory.RemoveItem(matDrops[i].data); // 인벤토리 아이템 제거
        }
    }
}