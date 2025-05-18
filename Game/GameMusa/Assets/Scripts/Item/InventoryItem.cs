using System;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int amount;

    // ������
    public InventoryItem(ItemData _itemData)
    {
        data = _itemData;

        AddStack(); // ���� �߰�
    }

    // ���� �߰� �Լ�
    public void AddStack() => amount++;

    // ���� ���� �Լ�
    public void RemoveStack() => amount--;
}