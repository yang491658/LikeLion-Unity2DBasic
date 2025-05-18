using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler
{
    public InventoryItem item; // ������
    [SerializeField] private Image itemImage; // ������ �̹���
    [SerializeField] private TextMeshProUGUI itemText; // ������ �ؽ�Ʈ

    // ���� ���� �Լ�
    public void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    // ���� ������Ʈ �Լ�
    public void UpdateSlot(InventoryItem _item)
    {
        // ���� ����
        item = _item;
        itemImage.color = Color.white;

        if (item != null) // ������ ����
        {
            itemImage.sprite = item.data.itemIcon; // ������ ������ ����

            if (item.amount > 1) // ������ 1�� �ʰ�
            {
                itemText.text = item.amount.ToString(); // ������ �ؽ�Ʈ = ������ ����
            }
            else // ������ 1�� ����
            {
                itemText.text = ""; // ������ �ؽ�Ʈ ǥ�� �� ��
            }
        }
    }

    // Ŭ�� �Է� �Լ�
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftControl)) // ���� ��Ʈ�� �Է� ����
        {
            Inventory.instance.RemoveItem(item.data); // �κ��丮 ������ ����

            return; // ����
        }

        if (item.data.itemType == ItemType.Equipment) // ������ Ÿ�� = ��� Ÿ��
        {
            Inventory.instance.EquipItem(item.data); // �κ��丮 ������ ����
        }
    }
}