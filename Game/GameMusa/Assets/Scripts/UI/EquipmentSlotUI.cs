using UnityEngine.EventSystems;

public class EquipmentSlotUI : ItemSlotUI
{
    public EquipmentType equipmentType; // ��� Ÿ��

    // ��ȿ�� Ȯ�� �Լ�
    private void OnValidate()
    {
        gameObject.name = equipmentType.ToString(); // ���� �̸� = ��� Ÿ��
    }

    // Ŭ�� �Է� �Լ� (���)
    public override void OnPointerDown(PointerEventData eventData)
    {
        Inventory.instance.UnequipItem(item.data as EquipmentData); // �κ��丮 ������ ����
    }
}