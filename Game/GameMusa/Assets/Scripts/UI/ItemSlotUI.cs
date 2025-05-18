using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler
{
    public InventoryItem item; // 아이템
    [SerializeField] private Image itemImage; // 아이템 이미지
    [SerializeField] private TextMeshProUGUI itemText; // 아이템 텍스트

    // 슬롯 비우기 함수
    public void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    // 슬롯 업데이트 함수
    public void UpdateSlot(InventoryItem _item)
    {
        // 슬롯 설정
        item = _item;
        itemImage.color = Color.white;

        if (item != null) // 아이템 있음
        {
            itemImage.sprite = item.data.itemIcon; // 아이템 아이콘 적용

            if (item.amount > 1) // 아이템 1개 초과
            {
                itemText.text = item.amount.ToString(); // 아이템 텍스트 = 아이템 개수
            }
            else // 아이템 1개 이하
            {
                itemText.text = ""; // 아이템 텍스트 표시 안 함
            }
        }
    }

    // 클릭 입력 함수
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftControl)) // 왼쪽 컨트롤 입력 유지
        {
            Inventory.instance.RemoveItem(item.data); // 인벤토리 아이템 제거

            return; // 종료
        }

        if (item.data.itemType == ItemType.Equipment) // 아이템 타입 = 장비 타입
        {
            Inventory.instance.EquipItem(item.data); // 인벤토리 아이템 장착
        }
    }
}