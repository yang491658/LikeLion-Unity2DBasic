using UnityEngine.EventSystems;

public class EquipmentSlotUI : ItemSlotUI
{
    public EquipmentType equipmentType; // 장비 타입

    // 유효성 확인 함수
    private void OnValidate()
    {
        gameObject.name = equipmentType.ToString(); // 슬롯 이름 = 장비 타입
    }

    // 클릭 입력 함수 (상속)
    public override void OnPointerDown(PointerEventData eventData)
    {
        Inventory.instance.UnequipItem(item.data as EquipmentData); // 인벤토리 아이템 해제
    }
}