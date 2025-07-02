using System;
using System.Collections.Generic;
using System.Linq;

// 챕터 데이터 클래스
public class ChapterData
{
    public int ChapterNo; // 번호
    public string ChapterName; // 이름
    public int TotalStages; // 총 스테이지 수
    public int ChapterRewardGem; // 보상 골드
    public int ChapterRewardGold; // 보상 보석
}

// 아이템 데이터 클래스
public class ItemData
{
    public int ItemId; // 아이디
    public string ItemName; // 이름
    public int AttackPower; // 공격력
    public int Defense; // 방어력
}

// 아이템 타입 열거형
public enum ItemType
{
    Weapon = 1, // 무기
    Shield, // 방패
    ChestArmor, // 흉갑
    Gloves, // 장갑
    Boots, // 부츠
    Accessory // 악세서리
}

// 아이템 등급 열거형
public enum ItemGrade
{
    Common = 1, // 일반
    Uncommon, // 고급
    Rare, // 희귀
    Epic, // 영웅
    Legendary, // 전설
}

public class DataTableManager : Singleton<DataTableManager>
{
    private const string DATA_PATH = "DataTable"; // 데이터 경로
    private const string CHAPTER_DATA_TABLE = "ChapterDataTable"; // 챕터 데이터 테이블
    private const string ITEM_DATA_TABLE = "ItemDataTable"; // 아이템 데이터 테이블

    private List<ChapterData> ChapterDatas = new List<ChapterData>(); // 챕터 데이터 배열
    private List<ItemData> ItemDatas = new List<ItemData>(); // 아이템 데이터 배열

    // 초기화 함수 (상속)
    protected override void Init()
    {
        base.Init(); // 초기화 (상속)

        LoadChapterDataTable(); // 챕터 데이터 테이블 불러오기
        LoadItemDataTable(); // 아이템 데이터 테이블 불러오기
    }

    #region 챕터 데이터
    // 챕터 데이터 테이블 불러오기 함수
    private void LoadChapterDataTable()
    {
        // 경로 내 데이터 테이블 읽기
        var dataTable = CSVReader.Read($"{DATA_PATH}/{CHAPTER_DATA_TABLE}");

        foreach (var data in dataTable) // 데이터 테이블
        {
            // 챕터 데이터 초기화
            var chapterData = new ChapterData
            {
                ChapterNo = Convert.ToInt32(data["chapter_no"]), // 번호
                ChapterName = data["chapter_name"].ToString(), // 이름
                TotalStages = Convert.ToInt32(data["total_stages"]), // 총 스테이지 수
                ChapterRewardGem = Convert.ToInt32(data["chapter_reward_gold"]), // 보상 골드
                ChapterRewardGold = Convert.ToInt32(data["chapter_reward_gem"]), // 보상 보석
            };

            // 챕터 데이터 배열 항목 추가
            ChapterDatas.Add(chapterData);
        }
    }

    // 챕터 데이터 가져오기 함수
    public ChapterData GetChapterData(int _chapterNo)
    {
        return ChapterDatas // 챕터 데이터 배열
            .Where(item => item.ChapterNo == _chapterNo) // 번호 일치
            .FirstOrDefault(); // 첫번째
    }
    #endregion

    #region 아이템 데이터
    // 아이템 데이터 테이블 불러오기 함수
    private void LoadItemDataTable()
    {
        // 경로 내 데이터 테이블 읽기
        var parsedDataTable = CSVReader.Read($"{DATA_PATH}/{ITEM_DATA_TABLE}");

        foreach (var data in parsedDataTable) // 데이터 테이블
        {
            // 아이템 데이터 초기화
            var itemData = new ItemData
            {
                ItemId = Convert.ToInt32(data["item_id"]), // 아이디
                ItemName = data["item_name"].ToString(), // 이름
                AttackPower = Convert.ToInt32(data["attack_power"]), // 공격력
                Defense = Convert.ToInt32(data["defense"]), // 방어력
            };

            // 아이템 데이터 배열 항목 추가
            ItemDatas.Add(itemData);
        }
    }

    // 아이템 데이터 가져오기 함수
    public ItemData GetItemData(int _itemId)
    {
        return ItemDatas // 아이템 데이터 배열
            .Where(item => item.ItemId == _itemId) // 아이디 일치
            .FirstOrDefault(); // 첫번째
    }
    #endregion
}