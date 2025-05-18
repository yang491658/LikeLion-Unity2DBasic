using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // �̱� ��
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    public List<ItemData> startItems; // ���� ������ ���

    // �κ��丮 ���
    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> invenDic;

    // ��� ���
    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDic;

    // ��� ���
    public List<InventoryItem> equipment;
    public Dictionary<EquipmentData, InventoryItem> equipDic;

    [Header("�κ��丮 UI")]
    [SerializeField] private Transform invenTrans;
    [SerializeField] private Transform stashTrans;
    [SerializeField] private Transform equipTrans;
    private ItemSlotUI[] invenSlots; // �κ��丮 ���� ���
    private ItemSlotUI[] stashSlots; // ��� ���� ���
    private EquipmentSlotUI[] equipSlots; // ��� ���� ���

    [Header("������ ��ٿ�")]
    private float armorCooldown; // �� ��ٿ�
    private float flaskCooldown; // �ö�ũ ��ٿ�
    private float lastUseArmor; // ������ �� ���
    private float lastUseFlask; // ������ �ö�ũ ���

    // �б� �Լ�
    public List<InventoryItem> GetEquip() => equipment;
    public List<InventoryItem> GetStash() => stash;

    private void Start()
    {
        // �κ��丮 ���
        inventory = new List<InventoryItem>();
        invenDic = new Dictionary<ItemData, InventoryItem>();

        // ��� ���
        stash = new List<InventoryItem>();
        stashDic = new Dictionary<ItemData, InventoryItem>();

        // ��� ���
        equipment = new List<InventoryItem>();
        equipDic = new Dictionary<EquipmentData, InventoryItem>();

        // �κ��丮 UI
        invenSlots = invenTrans.GetComponentsInChildren<ItemSlotUI>();
        stashSlots = stashTrans.GetComponentsInChildren<ItemSlotUI>();
        equipSlots = equipTrans.GetComponentsInChildren<EquipmentSlotUI>();

        AddStartItems(); // ���� ������ �߰�
    }

    // ���� ������ �߰� �Լ�
    private void AddStartItems()
    {
        for (int i = 0; i < startItems.Count; i++) // ���� ������ ���
        {
            AddItem(startItems[i]); // ������ �߰�
        }
    }

    // ������ �߰� �Լ�
    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment) // ������ Ÿ�� = ��� Ÿ��
        {
            if (invenDic.TryGetValue(_item, out InventoryItem _InvenItem)) // �κ��丮 ��Ͽ� ������ ����
            {
                _InvenItem.AddStack(); // ���� �߰�
            }
            else
            {
                // �κ��丮 �׸� �߰�
                InventoryItem newItem = new InventoryItem(_item);
                inventory.Add(newItem);
                invenDic.Add(_item, newItem);
            }
        }
        else if (_item.itemType == ItemType.Material) // ������ Ÿ�� = ��� Ÿ��
        {
            if (stashDic.TryGetValue(_item, out InventoryItem _stashItem)) // ��� ��Ͽ� ������ ����
            {
                _stashItem.AddStack(); // ���� �߰�
            }
            else
            {
                // ��� �׸� �߰�
                InventoryItem newItem = new InventoryItem(_item);
                stash.Add(newItem);
                stashDic.Add(_item, newItem);
            }
        }

        UpdateSlot(); // ���� ������Ʈ
    }

    // ������ ���� �Լ�
    public void RemoveItem(ItemData _item)
    {
        if (invenDic.TryGetValue(_item, out InventoryItem _invenItem)) // �κ��丮 ��Ͽ� ������ ����
        {
            if (_invenItem.amount > 1) // ������ 1�� �ʰ�
            {
                _invenItem.RemoveStack(); // ���� ����
            }
            else // ������ 1�� ����
            {
                // �κ��丮 �׸� ����
                inventory.Remove(_invenItem);
                invenDic.Remove(_item);
            }
        }

        if (stashDic.TryGetValue(_item, out InventoryItem _stashItem)) // ��� ��Ͽ� ������ ����
        {
            if (_stashItem.amount > 1) // ������ 1�� �ʰ�
            {
                _stashItem.RemoveStack(); // ���� ����
            }
            else // ������ 1�� ����
            {
                // ��� �׸� ����
                stash.Remove(_stashItem);
                stashDic.Remove(_item);
            }
        }

        UpdateSlot(); // ���� ������Ʈ
    }

    // ���� ������Ʈ �Լ�
    private void UpdateSlot()
    {
        for (int i = 0; i < invenSlots.Length; i++) // �κ��丮 ���� ���
        {
            invenSlots[i].ClearSlot(); // �κ��丮 ���� ����
        }

        for (int i = 0; i < inventory.Count; i++) // �κ��丮 ���
        {
            invenSlots[i].UpdateSlot(inventory[i]); // �κ��丮 ���� ������Ʈ
        }

        for (int i = 0; i < stashSlots.Length; i++) // ��� ���� ���
        {
            stashSlots[i].ClearSlot(); // ��� ���� ����
        }

        for (int i = 0; i < stash.Count; i++) // ��� ���
        {
            stashSlots[i].UpdateSlot(stash[i]); // ��� ���� ������Ʈ
        }

        for (int i = 0; i < equipSlots.Length; i++) // ��� ���� ���
        {
            foreach (KeyValuePair<EquipmentData, InventoryItem> item in equipDic) // ��� ���
            {
                if (equipSlots[i].equipmentType == item.Key.equipmentType) // ���� ��� Ÿ�� ��ġ
                {
                    equipSlots[i].UpdateSlot(item.Value); // ��� ���� ������Ʈ
                }
            }
        }
    }

    // ������ ���� �Լ�
    public void EquipItem(ItemData _item)
    {
        EquipmentData oldEquip = null;
        EquipmentData newEquip = _item as EquipmentData;
        InventoryItem newItem = new InventoryItem(newEquip);

        foreach (KeyValuePair<EquipmentData, InventoryItem> item in equipDic) // ��� ���
        {
            if (newEquip.equipmentType == item.Key.equipmentType) // ��� Ÿ�� ��ġ
            {
                oldEquip = item.Key; // ���� ��� ����
            }
        }

        if (oldEquip != null) // ���� ��� ����
        {
            UnequipItem(oldEquip); // ���� ��� ����
        }

        // ��� �׸� �߰� �� ������ �߰�
        equipment.Add(newItem);
        equipDic.Add(newEquip, newItem);
        newEquip.AddModifiers();

        RemoveItem(_item); // �κ��丮 ������ ����

        UpdateSlot(); // ���� ������Ʈ
    }

    // ������ ���� �Լ�
    public void UnequipItem(EquipmentData _equip)
    {
        if (equipDic.TryGetValue(_equip, out InventoryItem _equipItem)) // ��� ��Ͽ� ������ ����
        {
            // ��� �׸� ���� �� ������ ����
            equipment.Remove(_equipItem);
            equipDic.Remove(_equip);
            _equip.RemoveModifiers();

            foreach (var slot in equipSlots) // ��� ���� ���
            {
                if (_equip.equipmentType == slot.equipmentType) // ��� Ÿ�� ��ġ
                {
                    slot.ClearSlot(); // ��� ���� ����
                }
            }

            AddItem(_equip); // �κ��丮 ������ �߰�

            UpdateSlot(); // ���� ������Ʈ
        }
    }

    // ������ ���� �Լ�
    public void CreateItem(EquipmentData _equip, List<InventoryItem> _required)
    {
        List<InventoryItem> used = new List<InventoryItem>(); // ��� ��� ���

        for (int i = 0; i < _required.Count; i++) // �ʿ� ��� ���
        {
            if (stashDic.TryGetValue(_required[i].data, out InventoryItem _stashItem) &&
                // ��� ��Ͽ� ������ ����
                _required[i].amount <= _stashItem.amount) // ��� ���
            {
                used.Add(_stashItem); // ��� ��� �׸� �߰�
            }
            else // ��� ��Ͽ� ������ ���� �Ǵ� ��� ����
            {
                return; // ���� ����
            }
        }

        for (int i = 0; i < used.Count; i++) // ��� ��� ���
        {
            RemoveItem(used[i].data); // ��� ������ ����
        }

        AddItem(_equip); // �κ��丮 ������ �߰�
    }

    // �� ��� �Լ�
    public bool UseArmor()
    {
        EquipmentData currentArmor = GetEquipmentType(EquipmentType.Armor);

        if (Time.time > lastUseArmor + armorCooldown) // �� ��ٿ� ����
        {
            armorCooldown = currentArmor.itemCooldown; // �� ��ٿ� �ʱ�ȭ = ������ ��ٿ�
            lastUseArmor = Time.time; // ������ �� ��� ����

            return true; // ��� ����
        }

        return false; // ��� ����
    }

    // �ö�ũ ��� �Լ�
    public void UseFlask()
    {
        EquipmentData currentFlask = GetEquipmentType(EquipmentType.Flask);

        if (currentFlask != null && // �ö�ũ ���� ��
            Time.time > lastUseFlask + flaskCooldown) // �ö�ũ ��ٿ� ����
        {
            flaskCooldown = currentFlask.itemCooldown; // �ö�ũ ��ٿ� �ʱ�ȭ = ������ ��ٿ�
            lastUseFlask = Time.time; // ������ �ö�ũ ��� ����

            currentFlask?.DoItemEffect(null); // �ö�ũ ������ ȿ�� ����

            // �ö�ũ ���� ���� �� ����
            UnequipItem(currentFlask);
            RemoveItem(currentFlask);
        }
    }

    // ��� Ÿ�� �Լ�
    public EquipmentData GetEquipmentType(EquipmentType _type)
    {
        foreach (KeyValuePair<EquipmentData, InventoryItem> item in equipDic) // ��� ���
        {
            if (_type == item.Key.equipmentType) // ��� Ÿ�� ��ġ
            {
                return item.Key; // ��� Ÿ�� ��ȯ
            }
        }

        return null;
    }
}