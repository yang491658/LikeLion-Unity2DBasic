using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // 싱글 톤
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    public List<ItemData> startItems; // 시작 아이템 목록

    // 인벤토리 목록
    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> invenDic;

    // 재료 목록
    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDic;

    // 장비 목록
    public List<InventoryItem> equipment;
    public Dictionary<EquipmentData, InventoryItem> equipDic;

    [Header("인벤토리 UI")]
    [SerializeField] private Transform invenTrans;
    [SerializeField] private Transform stashTrans;
    [SerializeField] private Transform equipTrans;
    private ItemSlotUI[] invenSlots; // 인벤토리 슬롯 목록
    private ItemSlotUI[] stashSlots; // 재료 슬롯 목록
    private EquipmentSlotUI[] equipSlots; // 장비 슬롯 목록

    [Header("아이템 쿨다운")]
    private float armorCooldown; // 방어구 쿨다운
    private float flaskCooldown; // 플라스크 쿨다운
    private float lastUseArmor; // 마지막 방어구 사용
    private float lastUseFlask; // 마지막 플라스크 사용

    // 읽기 함수
    public List<InventoryItem> GetEquip() => equipment;
    public List<InventoryItem> GetStash() => stash;

    private void Start()
    {
        // 인벤토리 목록
        inventory = new List<InventoryItem>();
        invenDic = new Dictionary<ItemData, InventoryItem>();

        // 재료 목록
        stash = new List<InventoryItem>();
        stashDic = new Dictionary<ItemData, InventoryItem>();

        // 장비 목록
        equipment = new List<InventoryItem>();
        equipDic = new Dictionary<EquipmentData, InventoryItem>();

        // 인벤토리 UI
        invenSlots = invenTrans.GetComponentsInChildren<ItemSlotUI>();
        stashSlots = stashTrans.GetComponentsInChildren<ItemSlotUI>();
        equipSlots = equipTrans.GetComponentsInChildren<EquipmentSlotUI>();

        AddStartItems(); // 시작 아이템 추가
    }

    // 시작 아이템 추가 함수
    private void AddStartItems()
    {
        for (int i = 0; i < startItems.Count; i++) // 시작 아이템 목록
        {
            AddItem(startItems[i]); // 아이템 추가
        }
    }

    // 아이템 추가 함수
    public void AddItem(ItemData _item)
    {
        if (_item.itemType == ItemType.Equipment) // 아이템 타입 = 장비 타입
        {
            if (invenDic.TryGetValue(_item, out InventoryItem _InvenItem)) // 인벤토리 목록에 아이템 있음
            {
                _InvenItem.AddStack(); // 스택 추가
            }
            else
            {
                // 인벤토리 항목 추가
                InventoryItem newItem = new InventoryItem(_item);
                inventory.Add(newItem);
                invenDic.Add(_item, newItem);
            }
        }
        else if (_item.itemType == ItemType.Material) // 아이템 타입 = 재료 타입
        {
            if (stashDic.TryGetValue(_item, out InventoryItem _stashItem)) // 재료 목록에 아이템 있음
            {
                _stashItem.AddStack(); // 스택 추가
            }
            else
            {
                // 재료 항목 추가
                InventoryItem newItem = new InventoryItem(_item);
                stash.Add(newItem);
                stashDic.Add(_item, newItem);
            }
        }

        UpdateSlot(); // 슬롯 업데이트
    }

    // 아이템 제거 함수
    public void RemoveItem(ItemData _item)
    {
        if (invenDic.TryGetValue(_item, out InventoryItem _invenItem)) // 인벤토리 목록에 아이템 있음
        {
            if (_invenItem.amount > 1) // 아이템 1개 초과
            {
                _invenItem.RemoveStack(); // 스택 제거
            }
            else // 아이템 1개 이하
            {
                // 인벤토리 항목 제거
                inventory.Remove(_invenItem);
                invenDic.Remove(_item);
            }
        }

        if (stashDic.TryGetValue(_item, out InventoryItem _stashItem)) // 재료 목록에 아이템 있음
        {
            if (_stashItem.amount > 1) // 아이템 1개 초과
            {
                _stashItem.RemoveStack(); // 스택 제거
            }
            else // 아이템 1개 이하
            {
                // 재료 항목 제거
                stash.Remove(_stashItem);
                stashDic.Remove(_item);
            }
        }

        UpdateSlot(); // 슬롯 업데이트
    }

    // 슬롯 업데이트 함수
    private void UpdateSlot()
    {
        for (int i = 0; i < invenSlots.Length; i++) // 인벤토리 슬롯 목록
        {
            invenSlots[i].ClearSlot(); // 인벤토리 슬롯 비우기
        }

        for (int i = 0; i < inventory.Count; i++) // 인벤토리 목록
        {
            invenSlots[i].UpdateSlot(inventory[i]); // 인벤토리 슬롯 업데이트
        }

        for (int i = 0; i < stashSlots.Length; i++) // 재료 슬롯 목록
        {
            stashSlots[i].ClearSlot(); // 재료 슬롯 비우기
        }

        for (int i = 0; i < stash.Count; i++) // 재료 목록
        {
            stashSlots[i].UpdateSlot(stash[i]); // 재료 슬롯 업데이트
        }

        for (int i = 0; i < equipSlots.Length; i++) // 장비 슬롯 목록
        {
            foreach (KeyValuePair<EquipmentData, InventoryItem> item in equipDic) // 장비 목록
            {
                if (equipSlots[i].equipmentType == item.Key.equipmentType) // 슬롯 장비 타입 일치
                {
                    equipSlots[i].UpdateSlot(item.Value); // 장비 슬롯 업데이트
                }
            }
        }
    }

    // 아이템 장착 함수
    public void EquipItem(ItemData _item)
    {
        EquipmentData oldEquip = null;
        EquipmentData newEquip = _item as EquipmentData;
        InventoryItem newItem = new InventoryItem(newEquip);

        foreach (KeyValuePair<EquipmentData, InventoryItem> item in equipDic) // 장비 목록
        {
            if (newEquip.equipmentType == item.Key.equipmentType) // 장비 타입 일치
            {
                oldEquip = item.Key; // 기존 장비 저장
            }
        }

        if (oldEquip != null) // 기존 장비 있음
        {
            UnequipItem(oldEquip); // 기존 장비 해제
        }

        // 장비 항목 추가 및 변경자 추가
        equipment.Add(newItem);
        equipDic.Add(newEquip, newItem);
        newEquip.AddModifiers();

        RemoveItem(_item); // 인벤토리 아이템 제거

        UpdateSlot(); // 슬롯 업데이트
    }

    // 아이템 해제 함수
    public void UnequipItem(EquipmentData _equip)
    {
        if (equipDic.TryGetValue(_equip, out InventoryItem _equipItem)) // 장비 목록에 아이템 있음
        {
            // 장비 항목 제거 및 변경자 제거
            equipment.Remove(_equipItem);
            equipDic.Remove(_equip);
            _equip.RemoveModifiers();

            foreach (var slot in equipSlots) // 장비 슬롯 목록
            {
                if (_equip.equipmentType == slot.equipmentType) // 장비 타입 일치
                {
                    slot.ClearSlot(); // 장비 슬롯 비우기
                }
            }

            AddItem(_equip); // 인벤토리 아이템 추가

            UpdateSlot(); // 슬롯 업데이트
        }
    }

    // 아이템 제작 함수
    public void CreateItem(EquipmentData _equip, List<InventoryItem> _required)
    {
        List<InventoryItem> used = new List<InventoryItem>(); // 사용 재료 목록

        for (int i = 0; i < _required.Count; i++) // 필요 재료 목록
        {
            if (stashDic.TryGetValue(_required[i].data, out InventoryItem _stashItem) &&
                // 재료 목록에 아이템 있음
                _required[i].amount <= _stashItem.amount) // 재료 충분
            {
                used.Add(_stashItem); // 사용 재료 항목 추가
            }
            else // 재료 목록에 아이템 없음 또는 재료 부족
            {
                return; // 제작 실패
            }
        }

        for (int i = 0; i < used.Count; i++) // 사용 재료 목록
        {
            RemoveItem(used[i].data); // 재료 아이템 제거
        }

        AddItem(_equip); // 인벤토리 아이템 추가
    }

    // 방어구 사용 함수
    public bool UseArmor()
    {
        EquipmentData currentArmor = GetEquipmentType(EquipmentType.Armor);

        if (Time.time > lastUseArmor + armorCooldown) // 방어구 쿨다운 종료
        {
            armorCooldown = currentArmor.itemCooldown; // 방어구 쿨다운 초기화 = 아이템 쿨다운
            lastUseArmor = Time.time; // 마지막 방어구 사용 저장

            return true; // 사용 성공
        }

        return false; // 사용 실패
    }

    // 플라스크 사용 함수
    public void UseFlask()
    {
        EquipmentData currentFlask = GetEquipmentType(EquipmentType.Flask);

        if (currentFlask != null && // 플라스크 착용 중
            Time.time > lastUseFlask + flaskCooldown) // 플라스크 쿨다운 종료
        {
            flaskCooldown = currentFlask.itemCooldown; // 플라스크 쿨다운 초기화 = 아이템 쿨다운
            lastUseFlask = Time.time; // 마지막 플라스크 사용 저장

            currentFlask?.DoItemEffect(null); // 플라스크 아이템 효과 실행

            // 플라스크 장착 해제 및 제거
            UnequipItem(currentFlask);
            RemoveItem(currentFlask);
        }
    }

    // 장비 타입 함수
    public EquipmentData GetEquipmentType(EquipmentType _type)
    {
        foreach (KeyValuePair<EquipmentData, InventoryItem> item in equipDic) // 장비 목록
        {
            if (_type == item.Key.equipmentType) // 장비 타입 일치
            {
                return item.Key; // 장비 타입 반환
            }
        }

        return null;
    }
}