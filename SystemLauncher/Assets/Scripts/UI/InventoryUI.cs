using Gpm.Ui;
using TMPro;

// 인벤토리 정렬 타입
public enum InventorySortType
{
    ItemGrade, // 등급 기준
    ItemType, // 타입 기준
}

public class InventoryUI : BaseUI
{
    // 장비류 슬롯
    public EquippedItemSlot WeaponSlot; // 무기 슬롯
    public EquippedItemSlot ShieldSlot; // 방패 슬롯
    public EquippedItemSlot ChestArmorSlot; // 흉갑 슬롯
    public EquippedItemSlot BootsSlot; // 신발 슬롯
    public EquippedItemSlot GlovesSlot; // 장갑 슬롯
    public EquippedItemSlot AccessorySlot; // 장신구 슬롯

    public InfiniteScroll InventoryScrollList; // 인벤토리 스크롤 배열
    public TextMeshProUGUI SortBtnTxt; // 정렬 버튼 텍스트

    // 인벤토리 정렬 타입 (기본값 : 아이템 등급)
    private InventorySortType m_InventorySortType = InventorySortType.ItemGrade;

    public TextMeshProUGUI AttackPowerAmountTxt; // 공격력 텍스트
    public TextMeshProUGUI DefenseAmountTxt; // 방어력 텍스트

    // 정보 설정 함수 (상속)
    public override void SetInfo(BaseUIData _uiData)
    {
        base.SetInfo(_uiData); // 정보 설정 (상속)

        SetInventory(); // 인벤토리 설정
        SortInventory(); // 인벤토리 정렬
        SetEquippedItems(); // 장착 아이템 설정
        SetUserStats(); // 유저 스탯 설정
    }

    // 인벤토리 설정 함수
    private void SetInventory()
    {
        // 인벤토리 스크롤 리스트 초기화
        InventoryScrollList.Clear();

        // 유저 인벤토리 데이터 가져오기
        var userInventoryData = UserDataManager.Instance.GetUserData<UserInventoryData>();
        if (userInventoryData != null) // 유저 인벤토리 데이터가 있음
        {
            // 인벤토리 아이템 데이터 리스트 순회
            foreach (var itemData in userInventoryData.InventoryDatas)
            {
                if (userInventoryData.IsEquipped(itemData.SerialNumber)) // 장착 중
                {
                    Logger.Log(itemData.SerialNumber + "");
                    continue; // 건너뛰기
                }

                // 아이템 슬롯 데이터 초기화 및 설정
                var itemSlotData = new InventoryItemSlotData();
                itemSlotData.SerialNumber = itemData.SerialNumber; // 시리얼 번호
                itemSlotData.ItemId = itemData.ItemId; // 아이디

                // 아이템 데이터 추가
                InventoryScrollList.InsertData(itemSlotData);
            }
        }
    }

    // 인벤토리 정렬 함수
    private void SortInventory()
    {
        switch (m_InventorySortType) // 인벤토리 정렬 
        {
            case InventorySortType.ItemGrade: // 아이템 등급
                SortBtnTxt.text = "GRADE"; // 정렬 버튼 텍스트

                InventoryScrollList.SortDataList((a, b) => // 데이터 리스트 정렬
                {
                    // 데이터를 인벤토리 아이템 슬롯 데이터로 캐스팅
                    var itemA = a.data as InventoryItemSlotData;
                    var itemB = b.data as InventoryItemSlotData;

                    // 아이템 등급으로 정렬
                    int compareResult = ((itemB.ItemId / 1000) % 10).CompareTo((itemA.ItemId / 1000) % 10);

                    if (compareResult == 0) // 동일한 등급
                    {
                        // 아이템 아이디를 문자열로 변환 및 비교값 생성
                        var itemAIdStr = itemA.ItemId.ToString(); // 문자열 변환
                        var itemAComp = itemAIdStr.Substring(0, 1) + itemAIdStr.Substring(2, 3); // 비교값 생성

                        // 아이템 아이디를 문자열로 변환 및 비교값 생성
                        var itemBIdStr = itemB.ItemId.ToString(); // 문자열 변환
                        var itemBComp = itemBIdStr.Substring(0, 1) + itemBIdStr.Substring(2, 3); // 비교값 생성

                        // 아이템 타입으로 비교
                        compareResult = itemAComp.CompareTo(itemBComp);
                    }

                    return compareResult; // 비교 결과 반환
                });
                break;
            case InventorySortType.ItemType: // 아이템 타입
                SortBtnTxt.text = "TYPE"; // 정렬 버튼 텍스트

                InventoryScrollList.SortDataList((a, b) => // 데이터 리스트 정렬
                {
                    // 데이터를 인벤토리 아이템 슬롯 데이터로 캐스팅
                    var itemA = a.data as InventoryItemSlotData;
                    var itemB = b.data as InventoryItemSlotData;

                    // 아이템 아이디를 문자열로 변환 및 비교값 생성
                    var itemAIdStr = itemA.ItemId.ToString();
                    var itemAComp = itemAIdStr.Substring(0, 1) + itemAIdStr.Substring(2, 3);

                    // 아이템 아이디를 문자열로 변환 및 비교값 생성
                    var itemBIdStr = itemB.ItemId.ToString();
                    var itemBComp = itemBIdStr.Substring(0, 1) + itemBIdStr.Substring(2, 3);

                    // 아이템 타입으로 비교
                    int compareResult = itemAComp.CompareTo(itemBComp);

                    if (compareResult == 0) // 동일한 타입
                    {
                        // 아이템 등급으로 정렬
                        compareResult = ((itemB.ItemId / 1000) % 10).CompareTo((itemA.ItemId / 1000) % 10);
                    }

                    return compareResult; // 비교 결과 반환
                });
                break;
            default:
                break;
        }
    }

    // 장착 아이템 설정 함수
    private void SetEquippedItems()
    {
        // 유저 인벤토리 데이터 가져오기
        var userInventoryData = UserDataManager.Instance.GetUserData<UserInventoryData>();
        if (userInventoryData == null) // 유저 인벤토리 데이터가 없음
        {
            // 오류 로그 출력 : 존재하지 않는 데이터
            Logger.LogError("UserInventoryData Does Not Exist.");
            return; // 종료
        }

        if (userInventoryData.EquippedWeaponData != null) // 무기 장착 중
            WeaponSlot.SetItem(userInventoryData.EquippedWeaponData); // 아이템 설정
        else
            WeaponSlot.ClearItem(); // 아이템 초기화

        if (userInventoryData.EquippedShieldData != null) // 방패 장착 중
            ShieldSlot.SetItem(userInventoryData.EquippedShieldData); // 아이템 설정
        else
            ShieldSlot.ClearItem(); // 아이템 초기화

        if (userInventoryData.EquippedChestArmorData != null) // 흉갑 장착 중
            ChestArmorSlot.SetItem(userInventoryData.EquippedChestArmorData); // 아이템 설정
        else
            ChestArmorSlot.ClearItem(); // 아이템 초기화

        if (userInventoryData.EquippedBootsData != null) // 신발 장착 중
            BootsSlot.SetItem(userInventoryData.EquippedBootsData); // 아이템 설정
        else
            BootsSlot.ClearItem(); // 아이템 초기화

        if (userInventoryData.EquippedGlovesData != null) // 장갑 장착 중
            GlovesSlot.SetItem(userInventoryData.EquippedGlovesData); // 아이템 설정
        else
            GlovesSlot.ClearItem(); // 아이템 초기화

        if (userInventoryData.EquippedAccessoryData != null) // 장신구 장착 중
            AccessorySlot.SetItem(userInventoryData.EquippedAccessoryData); // 아이템 설정
        else
            AccessorySlot.ClearItem(); // 아이템 초기화
    }

    // 유저 스탯 설정 함수
    private void SetUserStats()
    {
        // 유저 인벤토리 데이터 가져오기
        var userInventoryData = UserDataManager.Instance.GetUserData<UserInventoryData>();
        if (userInventoryData == null) // 유저 인벤토리 없음
        {
            // 오류 로그 출력 : 존재하지 않는 데이터
            Logger.LogError("UserInventoryData Does Not Exist.");
            return; // 종료
        }

        // 유저 총 아이템 스탯
        var userTotalItemStats = userInventoryData.GetUserTotalItemStats();
        AttackPowerAmountTxt.text = $"+{userTotalItemStats.AttackPower.ToString("N0")}"; // 공격력
        DefenseAmountTxt.text = $"+{userTotalItemStats.Defense.ToString("N0")}"; // 방어력
    }

    //  정렬 버튼 클릭 함수
    public void OnClickSortBtn()
    {
        switch (m_InventorySortType) // 인벤토리 정렬 
        {
            case InventorySortType.ItemGrade: // 아이템 등급
                m_InventorySortType = InventorySortType.ItemType;
                break;
            case InventorySortType.ItemType: // 아이템 타입
                m_InventorySortType = InventorySortType.ItemGrade;
                break;
            default:
                break;
        }

        SortInventory(); // 인벤토리 정렬
    }

    // 아이템 장착 상태 함수
    public void OnEquipItem(int _itemId)
    {
        // 유저 인벤토리 데이터 가져오기
        var userInventoryData = UserDataManager.Instance.GetUserData<UserInventoryData>();
        if (userInventoryData == null) // 유저 인벤토리 데이터 없음
        {
            // 오류 로그 출력 : 존재하지 않는 데이터
            Logger.LogError("UserInventoryData Does Not Exist.");
            return; // 종료
        }

        // 아이템 타입 설정
        var itemType = (ItemType)(_itemId / 10000);
        switch (itemType) // 아이템 타입
        {
            // 아이템 타입에 따라 아이템 설정
            case ItemType.Weapon: WeaponSlot.SetItem(userInventoryData.EquippedWeaponData); break;
            case ItemType.Shield: ShieldSlot.SetItem(userInventoryData.EquippedShieldData); break;
            case ItemType.ChestArmor: ChestArmorSlot.SetItem(userInventoryData.EquippedChestArmorData); break;
            case ItemType.Gloves: GlovesSlot.SetItem(userInventoryData.EquippedGlovesData); break;
            case ItemType.Boots: BootsSlot.SetItem(userInventoryData.EquippedBootsData); break;
            case ItemType.Accessory: AccessorySlot.SetItem(userInventoryData.EquippedAccessoryData); break;
            default: break;
        }

        SetInventory(); // 인벤토리 설정
        SortInventory(); // 인벤토리 정렬
        SetUserStats(); // 유저 스탯 설정
    }

    // 아이템 장착 해제 상태 함수
    public void OnUnequipItem(int itemId)
    {
        // 아이템 타입 설정
        var itemType = (ItemType)(itemId / 10000);
        switch (itemType) // 아이템 타입
        {
            case ItemType.Weapon: WeaponSlot.ClearItem(); break;
            case ItemType.Shield: ShieldSlot.ClearItem(); break;
            case ItemType.ChestArmor: ChestArmorSlot.ClearItem(); break;
            case ItemType.Gloves: GlovesSlot.ClearItem(); break;
            case ItemType.Boots: BootsSlot.ClearItem(); break;
            case ItemType.Accessory: AccessorySlot.ClearItem(); break;
            default: break;
        }

        SetInventory(); // 인벤토리 설정
        SortInventory(); // 인벤토리 정렬
        SetUserStats(); // 유저 스탯 설정
    }
}