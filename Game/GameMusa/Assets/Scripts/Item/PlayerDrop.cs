using System.Collections.Generic;
using UnityEngine;

public class PlayerDrop : EntityDrop
{
    [Header("�÷��̾� ���")]
    [SerializeField] private float equipDropChance; // ��� ��� Ȯ��
    [SerializeField] private float matDropChance; // ��� ��� Ȯ��

    // ��� ���� �Լ� (���)
    public override void GenerateDrops()
    {
        Inventory inventory = Inventory.instance; // �κ��丮

        List<InventoryItem> equipDrops = new List<InventoryItem>(); // ��� ��� ���

        foreach (InventoryItem item in inventory.GetEquip()) // �κ��丮 ��� ���
        {
            if (Random.Range(0, 100) < equipDropChance) // ��� ��� Ȯ��
            {
                DropItem(item.data); // ������ ���

                equipDrops.Add(item); // ��� ��� �׸� �߰�
            }
        }

        for (int i = 0; i < equipDrops.Count; i++) // ��� ��� ���
        {
            inventory.UnequipItem(equipDrops[i].data as EquipmentData); // �κ��丮 ��� ����

            inventory.RemoveItem(equipDrops[i].data); // �κ��丮 ������ ����
        }

        List<InventoryItem> matDrops = new List<InventoryItem>(); // ��� ��� ���

        foreach (InventoryItem item in inventory.GetStash()) // �κ��丮 ��� ���
        {
            if (Random.Range(0, 100) < matDropChance) // ��� ��� Ȯ��
            {
                DropItem(item.data); // ������ ���

                matDrops.Add(item); // ��� ��� �׸� �߰�
            }
        }

        for (int i = 0; i < matDrops.Count; i++) // ��� ��� ���
        {
            inventory.RemoveItem(matDrops[i].data); // �κ��丮 ������ ����
        }
    }
}