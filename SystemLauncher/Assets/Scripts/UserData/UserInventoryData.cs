using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // 유저 아이템 데이터 클래스
public class UserItemData
{
    public long SerialNumber; // 시리얼 번호
    public int ItemId; // 아이디

    // 생성자
    public UserItemData(long _serialNumber, int _itemId)
    {
        SerialNumber = _serialNumber;
        ItemId = _itemId;
    }
}

[Serializable] // JSON으로 파싱하기 위한 래퍼 클래스
public class UserInventoryDatasWrapper
{
    // 인벤토리 데이터 배열
    public List<UserItemData> InventoryDatas;
}

// 유저 아이템 스탯 클래스
public class UserItemStats
{
    public int AttackPower; // 공격력
    public int Defense; // 방어력

    // 생성자
    public UserItemStats(int _attackPower, int _defense)
    {
        AttackPower = _attackPower;
        Defense = _defense;
    }
}

public class UserInventoryData : UserData
{
    // 장비류
    public UserItemData EquippedWeaponData { get; set; } // 무기
    public UserItemData EquippedShieldData { get; set; } // 방패
    public UserItemData EquippedChestArmorData { get; set; } // 흉갑
    public UserItemData EquippedBootsData { get; set; } // 신발
    public UserItemData EquippedGlovesData { get; set; } // 장갑
    public UserItemData EquippedAccessoryData { get; set; } // 장신구

    // 인벤토리 데이터 배열
    public List<UserItemData> InventoryDatas { get; set; } = new List<UserItemData>();

    // 장착 아이템 배열
    public Dictionary<long, UserItemStats> EquippedItemDic { get; set; }
        = new Dictionary<long, UserItemStats>();

    // 기본값 설정 함수
    public void SetDefault()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::SetDefault");

        // 인벤토리 데이터 아이템 추가 (시리얼 번호 = 현재시간 + 랜덤 4자리 숫자)
        InventoryDatas.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")
            + UnityEngine.Random.Range(0, 9999).ToString("D4")), 11001));
        InventoryDatas.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")
            + UnityEngine.Random.Range(0, 9999).ToString("D4")), 11002));
        InventoryDatas.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")
            + UnityEngine.Random.Range(0, 9999).ToString("D4")), 22001));
        InventoryDatas.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")
            + UnityEngine.Random.Range(0, 9999).ToString("D4")), 22002));
        InventoryDatas.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")
            + UnityEngine.Random.Range(0, 9999).ToString("D4")), 33001));
        InventoryDatas.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")
            + UnityEngine.Random.Range(0, 9999).ToString("D4")), 33002));
        InventoryDatas.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")
            + UnityEngine.Random.Range(0, 9999).ToString("D4")), 44001));
        InventoryDatas.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")
            + UnityEngine.Random.Range(0, 9999).ToString("D4")), 44002));
        InventoryDatas.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")
            + UnityEngine.Random.Range(0, 9999).ToString("D4")), 55001));
        InventoryDatas.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")
            + UnityEngine.Random.Range(0, 9999).ToString("D4")), 55002));
        InventoryDatas.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")
            + UnityEngine.Random.Range(0, 9999).ToString("D4")), 65001));
        InventoryDatas.Add(new UserItemData(long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")
            + UnityEngine.Random.Range(0, 9999).ToString("D4")), 65002));

        // 무기 및 방패 장착
        EquippedWeaponData = new UserItemData(InventoryDatas[0].SerialNumber, InventoryDatas[0].ItemId);
        EquippedShieldData = new UserItemData(InventoryDatas[2].SerialNumber, InventoryDatas[2].ItemId);

        SetEquippedItemDic(); // 장착 아이템 배열 설정
    }

    // 데이터 불러오기 함수
    public bool LoadData()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;

        try
        {
            // 로컬 저장소에서 JSON 문자열 가져오기 ~ 무기
            string weaponJson = PlayerPrefs.GetString("EquippedWeaponData");
            if (!string.IsNullOrEmpty(weaponJson)) // JSON 문자열 비어있지 않음
            {
                // JSON을 UserItemData 객체로 변환
                EquippedWeaponData = JsonUtility.FromJson<UserItemData>(weaponJson);
                // 일반 로그 출력 : 장착 아이템 정보
                Logger.Log($"SerialNumber : {EquippedWeaponData.SerialNumber} / " +
                    $"ItemId : {EquippedWeaponData.ItemId}");
            }

            // 로컬 저장소에서 JSON 문자열 가져오기 ~ 방패
            string shieldJson = PlayerPrefs.GetString("EquippedShieldData");
            if (!string.IsNullOrEmpty(shieldJson)) // JSON 문자열 비어있지 않음
            {
                // JSON을 UserItemData 객체로 변환
                EquippedShieldData = JsonUtility.FromJson<UserItemData>(shieldJson);
                // 일반 로그 출력 : 장착 아이템 정보
                Logger.Log($"SerialNumber : {EquippedShieldData.SerialNumber} / " +
                    $"ItemId : {EquippedShieldData.ItemId}");
            }

            // 로컬 저장소에서 JSON 문자열 가져오기 ~ 흉갑
            string chestArmorJson = PlayerPrefs.GetString("EquippedChestArmorData");
            if (!string.IsNullOrEmpty(chestArmorJson)) // JSON 문자열 비어있지 않음
            {
                // JSON을 UserItemData 객체로 변환
                EquippedChestArmorData = JsonUtility.FromJson<UserItemData>(chestArmorJson);
                // 일반 로그 출력 : 장착 아이템 정보
                Logger.Log($"SerialNumber : {EquippedChestArmorData.SerialNumber} / " +
                    $"ItemId : {EquippedChestArmorData.ItemId}");
            }

            // 로컬 저장소에서 JSON 문자열 가져오기 ~ 신발
            string bootsJson = PlayerPrefs.GetString("EquippedBootsData");
            if (!string.IsNullOrEmpty(bootsJson)) // JSON 문자열 비어있지 않음
            {
                // JSON을 UserItemData 객체로 변환
                EquippedBootsData = JsonUtility.FromJson<UserItemData>(bootsJson);
                // 일반 로그 출력 : 장착 아이템 정보
                Logger.Log($"SerialNumber : {EquippedBootsData.SerialNumber} / " +
                    $"ItemId : {EquippedBootsData.ItemId}");
            }

            // 로컬 저장소에서 JSON 문자열 가져오기 ~ 장갑
            string glovesJson = PlayerPrefs.GetString("EquippedGlovesData");
            if (!string.IsNullOrEmpty(glovesJson)) // JSON 문자열 비어있지 않음
            {
                // JSON을 UserItemData 객체로 변환
                EquippedGlovesData = JsonUtility.FromJson<UserItemData>(glovesJson);
                // 일반 로그 출력 : 장착 아이템 정보
                Logger.Log($"SerialNumber : {EquippedGlovesData.SerialNumber} / " +
                    $"ItemId : {EquippedGlovesData.ItemId}");
            }

            // 로컬 저장소에서 JSON 문자열 가져오기 ~ 장신구
            string accessoryJson = PlayerPrefs.GetString("EquippedAccessoryData");
            if (!string.IsNullOrEmpty(accessoryJson)) // JSON 문자열 비어있지 않음
            {
                // JSON을 UserItemData 객체로 변환
                EquippedAccessoryData = JsonUtility.FromJson<UserItemData>(accessoryJson);
                // 일반 로그 출력 : 장착 아이템 정보
                Logger.Log($"SerialNumber : {EquippedAccessoryData.SerialNumber} / " +
                    $"ItemId : {EquippedAccessoryData.ItemId}");
            }

            // 로컬 저장소에서 JSON 문자열 가져오기
            string inventoryDatasJson = PlayerPrefs.GetString("InventoryDatas");
            if (!string.IsNullOrEmpty(inventoryDatasJson)) // JSON 문자열 비어있지 않음
            {
                // JSON을 UserInventoryDatasWrapper 객체로 변환
                UserInventoryDatasWrapper itemDataListWrapper
                    = JsonUtility.FromJson<UserInventoryDatasWrapper>(inventoryDatasJson);

                // 실제 데이터 리스트
                InventoryDatas = itemDataListWrapper.InventoryDatas;

                // 일반 로그 출력
                Logger.Log("InventoryDatas");

                foreach (var item in InventoryDatas) // 인벤토리 데이터 배열
                {
                    // 일반 로그 출력 : 아이템 정보
                    Logger.Log($"SerialNumber : {item.SerialNumber} / ItemId : {item.ItemId}");
                }
            }

            result = true; // 결과 저장
        }
        catch (Exception e) // 오류 발생
        {
            // 오류 로그 출력 : 불러오기 실패
            Logger.Log("Load Failed (" + e.Message + ")");
        }

        return result; // 결과 반환
    }

    // 데이터 저장 함수
    public bool SaveData()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::SaveData");

        bool result = false;

        try
        {
            // 무기 데이터를 JSON으로 변환
            string weaponJson = JsonUtility.ToJson(EquippedWeaponData);
            // 로컬 저장소에서 무기 데이터 가져오기
            PlayerPrefs.SetString("EquippedWeaponData", weaponJson);
            if (EquippedWeaponData != null) // 무기 데이터 있음
            {
                // 일반 로그 출력 : 아이템 정보
                Logger.Log($"SerialNumber : {EquippedWeaponData.SerialNumber} / " +
                    $"ItemId : {EquippedWeaponData.ItemId}");
            }

            // 방패 데이터를 JSON으로 변환
            string shieldJson = JsonUtility.ToJson(EquippedShieldData);
            // 로컬 저장소에서 방패 데이터 가져오기
            PlayerPrefs.SetString("EquippedShieldData", shieldJson);
            if (EquippedShieldData != null) // 방패 데이터 있음
            {
                // 일반 로그 출력 : 아이템 정보
                Logger.Log($"SerialNumber : {EquippedShieldData.SerialNumber} / " +
                    $"ItemId : {EquippedShieldData.ItemId}");
            }

            // 흉갑 데이터를 JSON으로 변환
            string chestArmorJson = JsonUtility.ToJson(EquippedChestArmorData);
            // 로컬 저장소에서 흉갑 데이터 가져오기
            PlayerPrefs.SetString("EquippedChestArmorData", chestArmorJson);
            if (EquippedChestArmorData != null) // 흉갑 데이터 있음
            {
                // 일반 로그 출력 : 아이템 정보
                Logger.Log($"SerialNumber : {EquippedChestArmorData.SerialNumber} / " +
                    $"ItemId : {EquippedChestArmorData.ItemId}");
            }

            // 신발 데이터를 JSON으로 변환
            string bootsJson = JsonUtility.ToJson(EquippedBootsData);
            // 로컬 저장소에서 신발 데이터 가져오기
            PlayerPrefs.SetString("EquippedBootsData", bootsJson);
            if (EquippedBootsData != null) // 신발 데이터 있음
            {
                // 일반 로그 출력 : 아이템 정보
                Logger.Log($"SerialNumber : {EquippedBootsData.SerialNumber} / " +
                    $"ItemId : {EquippedBootsData.ItemId}");
            }

            // 장갑 데이터를 JSON으로 변환
            string glovesJson = JsonUtility.ToJson(EquippedGlovesData);
            // 로컬 저장소에서 장갑 데이터 가져오기
            PlayerPrefs.SetString("EquippedGlovesData", glovesJson);
            if (EquippedGlovesData != null) // 장갑 데이터 있음
            {
                // 일반 로그 출력 : 아이템 정보
                Logger.Log($"SerialNumber : {EquippedGlovesData.SerialNumber} / " +
                    $"ItemId : {EquippedGlovesData.ItemId}");
            }

            // 장신구 데이터를 JSON으로 변환
            string accessoryJson = JsonUtility.ToJson(EquippedAccessoryData);
            // 로컬 저장소에서 장신구 데이터 가져오기
            PlayerPrefs.SetString("EquippedAccessoryData", accessoryJson);
            if (EquippedAccessoryData != null) // 장신구 데이터 있음
            {
                // 일반 로그 출력 : 아이템 정보
                Logger.Log($"SerialNumber : {EquippedAccessoryData.SerialNumber} / " +
                    $"ItemId : {EquippedAccessoryData.ItemId}");
            }

            // 인벤토리 데이터 배열 래퍼 객체 생성 및 인벤토리 데이터 리스트 설정
            UserInventoryDatasWrapper inventoryDatasWrapper = new UserInventoryDatasWrapper();
            inventoryDatasWrapper.InventoryDatas = InventoryDatas;

            // 래퍼객체를 문자열로 변환
            string inventoryItemDatasJson = JsonUtility.ToJson(inventoryDatasWrapper);

            // 로컬 저장소에서 JSON 문자열 저장
            PlayerPrefs.SetString("InventoryDatas", inventoryItemDatasJson);

            // 일반 로그 출력
            Logger.Log("InventoryDatas");

            foreach (var item in InventoryDatas) // 인벤토리 데이터 배열
            {
                // 일반 로그 출력 : 아이템 정보
                Logger.Log($"SerialNumber : {item.SerialNumber} / ItemId : {item.ItemId}");
            }

            // 로컬 저장소에서 저장
            PlayerPrefs.Save();

            result = true; // 결과 저장
        }
        catch (Exception e) // 오류 발생
        {
            // 오류 로그 출력 : 저장 실패
            Logger.Log("Save Failed (" + e.Message + ")");
        }

        return result; // 결과 반환
    }

    // 장착 아이템 배열 설정 함수
    public void SetEquippedItemDic()
    {
        if (EquippedWeaponData != null) // 무기 장착 중
        {
            // 무기 아이템 데이터 가져오기
            var itemData = DataTableManager.Instance.GetItemData(EquippedWeaponData.ItemId);
            if (itemData != null) // 아이템 데이터 있음
            {
                // 장착 아이템 배열 내 무기 항목 추가
                EquippedItemDic.Add(EquippedWeaponData.SerialNumber,
                    new UserItemStats(itemData.AttackPower, itemData.Defense));
            }
        }

        if (EquippedShieldData != null) // 방패 장착 중
        {
            // 방패 아이템 데이터 가져오기
            var itemData = DataTableManager.Instance.GetItemData(EquippedShieldData.ItemId);
            if (itemData != null) // 아이템 데이터 있음
            {
                // 장착 아이템 배열 내 방패 항목 추가
                EquippedItemDic.Add(EquippedShieldData.SerialNumber,
                    new UserItemStats(itemData.AttackPower, itemData.Defense));
            }
        }

        if (EquippedChestArmorData != null) // 흉갑 장착 중
        {
            // 흉갑 아이템 데이터 가져오기
            var itemData = DataTableManager.Instance.GetItemData(EquippedChestArmorData.ItemId);
            if (itemData != null) // 아이템 데이터 있음
            {
                // 장착 아이템 배열 내 흉갑 항목 추가
                EquippedItemDic.Add(EquippedChestArmorData.SerialNumber,
                    new UserItemStats(itemData.AttackPower, itemData.Defense));
            }
        }

        if (EquippedBootsData != null) // 신발 장착 중
        {
            // 신발 아이템 데이터 가져오기
            var itemData = DataTableManager.Instance.GetItemData(EquippedBootsData.ItemId);
            if (itemData != null) // 아이템 데이터 있음
            {
                // 장착 아이템 배열 내 신발 항목 추가
                EquippedItemDic.Add(EquippedBootsData.SerialNumber,
                    new UserItemStats(itemData.AttackPower, itemData.Defense));
            }
        }

        if (EquippedGlovesData != null) // 장갑 장착 중
        {
            // 장갑 아이템 데이터 가져오기
            var itemData = DataTableManager.Instance.GetItemData(EquippedGlovesData.ItemId);
            if (itemData != null) // 아이템 데이터 있음
            {
                // 장착 아이템 배열 내 장갑 항목 추가
                EquippedItemDic.Add(EquippedGlovesData.SerialNumber,
                    new UserItemStats(itemData.AttackPower, itemData.Defense));
            }
        }

        if (EquippedAccessoryData != null) // 장신구 장착 중
        {
            // 장신구 아이템 데이터 가져오기
            var itemData = DataTableManager.Instance.GetItemData(EquippedAccessoryData.ItemId);
            if (itemData != null) // 아이템 데이터 있음
            {
                // 장착 아이템 배열 내 장신구 항목 추가
                EquippedItemDic.Add(EquippedAccessoryData.SerialNumber,
                    new UserItemStats(itemData.AttackPower, itemData.Defense));
            }
        }
    }

    // 장착 여부 함수
    public bool IsEquipped(long _serialNumber)
    {
        return EquippedItemDic // 장착 아이템 배열
            .ContainsKey(_serialNumber); // 시리얼 번호 일치
    }

    // 아이템 장착 함수
    public void EquipItem(long _serialNumber, int _itemId)
    {
        // 아이템 데이터 가져오기
        var itemData = DataTableManager.Instance.GetItemData(_itemId);
        if (itemData == null) // 아이템 데이터 없음
        {
            // 오류 로그 출력 : 존재하지 않는 데이터
            Logger.LogError($"ItemData Does Not Exist. ItemId : {_itemId}");
            return; // 종료
        }

        // 아이템 타입 설정
        var itemType = (ItemType)(_itemId / 10000);
        switch (itemType) // 아이템 타입
        {
            case ItemType.Weapon: // 무기
                if (EquippedWeaponData != null) // 무기 데이터 있음
                {
                    // 장착 아이템 배열 항목 제거 및 데이터 초기화
                    EquippedItemDic.Remove(EquippedWeaponData.SerialNumber);
                    EquippedWeaponData = null;
                }
                // 무기 데이터 설정
                EquippedWeaponData = new UserItemData(_serialNumber, _itemId);
                break;
            case ItemType.Shield: // 방패
                if (EquippedShieldData != null)
                {
                    // 장착 아이템 배열 항목 제거 및 데이터 초기화
                    EquippedItemDic.Remove(EquippedShieldData.SerialNumber);
                    EquippedShieldData = null;
                }
                // 방패 데이터 설정
                EquippedShieldData = new UserItemData(_serialNumber, _itemId);
                break;
            case ItemType.ChestArmor: // 흉갑
                if (EquippedChestArmorData != null)
                {
                    // 장착 아이템 배열 항목 제거 및 데이터 초기화
                    EquippedItemDic.Remove(EquippedChestArmorData.SerialNumber);
                    EquippedChestArmorData = null;
                }
                // 흉갑 데이터 설정
                EquippedChestArmorData = new UserItemData(_serialNumber, _itemId);
                break;
            case ItemType.Gloves: // 장갑
                if (EquippedGlovesData != null)
                {
                    // 장착 아이템 배열 항목 제거 및 데이터 초기화
                    EquippedItemDic.Remove(EquippedGlovesData.SerialNumber);
                    EquippedGlovesData = null;
                }
                // 장갑 데이터 설정
                EquippedGlovesData = new UserItemData(_serialNumber, _itemId);
                break;
            case ItemType.Boots: // 신발
                if (EquippedBootsData != null)
                {
                    // 장착 아이템 배열 항목 제거 및 데이터 초기화
                    EquippedItemDic.Remove(EquippedBootsData.SerialNumber);
                    EquippedBootsData = null;
                }
                // 신발 데이터 설정
                EquippedBootsData = new UserItemData(_serialNumber, _itemId);
                break;
            case ItemType.Accessory: // 장신구
                if (EquippedAccessoryData != null)
                {
                    // 장착 아이템 배열 항목 제거 및 데이터 초기화
                    EquippedItemDic.Remove(EquippedAccessoryData.SerialNumber);
                    EquippedAccessoryData = null;
                }
                // 장신구 데이터 설정
                EquippedAccessoryData = new UserItemData(_serialNumber, _itemId);
                break;
            default:
                break;
        }

        // 장착 아이템 배열 내 항목 추가
        EquippedItemDic.Add(_serialNumber, new UserItemStats(itemData.AttackPower, itemData.Defense));
    }

    // 아이템 장착 해제 함수
    public void UnequipItem(long _serialNumber, int _itemId)
    {
        // 아이템 타입 설정
        var itemType = (ItemType)(_itemId / 10000);
        switch (itemType) // 아이템 타입
        {
            // 타입에 따른 해당 데이터 초기화
            case ItemType.Weapon: EquippedWeaponData = null; break; // 무기
            case ItemType.Shield: EquippedShieldData = null; break; // 방패
            case ItemType.ChestArmor: EquippedChestArmorData = null; break; // 흉갑
            case ItemType.Gloves: EquippedGlovesData = null; break; // 장갑
            case ItemType.Boots: EquippedBootsData = null; break; // 신발
            case ItemType.Accessory: EquippedAccessoryData = null; break; // 장신구
            default: break;
        }

        // 장착 아이템 배열 항목 제거
        EquippedItemDic.Remove(_serialNumber);
    }

    // 유저 총 아이템 스탯 가져오기 함수
    public UserItemStats GetUserTotalItemStats()
    {
        var totalAttackPower = 0;
        var totalDefense = 0;

        foreach (var item in EquippedItemDic) // 장착 아이템 배열
        {
            // 공격력 및 방어력 추가
            totalAttackPower += item.Value.AttackPower;
            totalDefense += item.Value.Defense;
        }

        return new UserItemStats(totalAttackPower, totalDefense); // 유저 아이템 스탯
    }
}
