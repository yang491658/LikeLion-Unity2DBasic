using System;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int amount;

    // 생성자
    public InventoryItem(ItemData _itemData)
    {
        data = _itemData;

        AddStack(); // 스택 추가
    }

    // 스택 추가 함수
    public void AddStack() => amount++;

    // 스택 제거 함수
    public void RemoveStack() => amount--;
}