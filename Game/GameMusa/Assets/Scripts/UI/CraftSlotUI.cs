using UnityEngine.EventSystems;

public class CraftSlotUI : ItemSlotUI
{
    // Ȱ��ȭ �Լ�
    private void OnEnable()
    {
        UpdateSlot(item); // ���� ������Ʈ
    }

    // Ŭ�� �Է� �Լ� (���)
    public override void OnPointerDown(PointerEventData eventData)
    {
        EquipmentData craftData = item.data as EquipmentData; // ������ ������ ��ȯ

        Inventory.instance.CreateItem(craftData, craftData.materials); // �κ��丮 ������ ����
    }
}