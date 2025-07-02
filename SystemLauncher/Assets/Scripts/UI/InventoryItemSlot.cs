using Gpm.Ui;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

// 인벤토리 아이템 슬롯 데이터
public class InventoryItemSlotData : InfiniteScrollData
{
    public long SerialNumber; // 시리얼 번호
    public int ItemId; // 아이디
}

// 인벤토리 아이템 슬롯
public class InventoryItemSlot : InfiniteScrollItem
{
    public Image ItemGradeBg; // 등급 배경
    public Image ItemIcon; // 아이콘

    private InventoryItemSlotData m_InventoryItemSlotData; // 인벤토리 슬롯 아이템 데이터

    // 데이터 업데이트 함수 (상속)
    public override void UpdateData(InfiniteScrollData _scrollData)
    {
        base.UpdateData(_scrollData); // 데이터 업데이터 (상속)

        // 인벤토리 아이템 슬롯 데이터 초기화
        m_InventoryItemSlotData = _scrollData as InventoryItemSlotData;
        if (m_InventoryItemSlotData == null) // 인벤토리 아이템 슬롯 없음
        {
            // 오류 로그 출력 : 인벤토리 아이템 슬롯 잘못됨
            Logger.LogError("InventoryItemSlotData Is Invalid.");
            return; // 종료
        }

        // 아이템 등급 설정
        var itemGrade = (ItemGrade)((m_InventoryItemSlotData.ItemId / 1000) % 10);

        // 등급에 따른 텍스쳐
        var gradeBgTexture = Resources.Load<Texture2D>($"Textures/{itemGrade}");
        if (gradeBgTexture != null) // 텍스쳐 없음
        {
            // 텍스쳐를 스프라이트로 변환 및 배경 설정
            ItemGradeBg.sprite
                = Sprite.Create(
                    gradeBgTexture,
                    new Rect(0, 0, gradeBgTexture.width, gradeBgTexture.height),
                    new Vector2(1f, 1f));
        }

        // 아이디 문자열을 StringBuilder로 변환
        StringBuilder sb = new StringBuilder(m_InventoryItemSlotData.ItemId.ToString());
        sb[1] = '1'; // 아이콘 이름 규칙 적용
        var itemIconName = sb.ToString(); // 변경된 문자열을 아이콘 이름으로 적용

        // 아이콘 텍스쳐 불러오기
        var itemIconTexture = Resources.Load<Texture2D>($"Textures/{itemIconName}");
        if (itemIconTexture != null) // 아이콘 텍스쳐 없음
        {
            // 텍스쳐를 스프라이트로 변환 및 아이콘 설정
            ItemIcon.sprite
                = Sprite.Create(
                    itemIconTexture,
                    new Rect(0, 0, itemIconTexture.width, itemIconTexture.height),
                    new Vector2(1f, 1f));
        }
    }

    // 인벤토리 아이템 슬롯 클릭 함수
    public void OnClickInventoryItemSlot()
    {
        // UI 데이터 초기화 및 설정
        var uiData = new EquipmentUIData();
        uiData.SerialNumber = m_InventoryItemSlotData.SerialNumber; // 시리얼 번호
        uiData.ItemId = m_InventoryItemSlotData.ItemId; // 아이디

        UIManager.Instance.OpenUI<EquipmentUI>(uiData); // 장비 UI 열기
    }
}