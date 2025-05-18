using UnityEngine.EventSystems;

public class CraftSlotUI : ItemSlotUI
{
    // 활성화 함수
    private void OnEnable()
    {
        UpdateSlot(item); // 슬롯 업데이트
    }

    // 클릭 입력 함수 (상속)
    public override void OnPointerDown(PointerEventData eventData)
    {
        EquipmentData craftData = item.data as EquipmentData; // 아이템 데이터 변환

        Inventory.instance.CreateItem(craftData, craftData.materials); // 인벤토리 아이템 제작
    }
}