using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIData : BaseUIData
{
    public long SerialNumber; // 시리얼 번호
    public int ItemId; // 아이디
    public bool IsEquipped; // 장착 여부
}

public class EquipmentUI : BaseUI
{
    public Image ItemGradeBg; // 등급 배경 이미지
    public Image ItemIcon; // 아이콘 이미지
    public TextMeshProUGUI ItemGradeTxt; // 등급 텍스트
    public TextMeshProUGUI ItemNameTxt; // 이름 텍스트
    public TextMeshProUGUI AttackPowerAmountTxt; // 공격력 텍스트
    public TextMeshProUGUI DefenseAmountTxt; // 방어력 텍스트
    public TextMeshProUGUI EquipBtnTxt; // 장착 버튼 텍스트

    private EquipmentUIData m_EquipmentUIData; // 장비 UI 데이터

    // 정보 설정 함수 (상속)
    public override void SetInfo(BaseUIData _uiData)
    {
        base.SetInfo(_uiData); // 정보 설정 (상속)

        // 장비 UI 데이터 설정
        m_EquipmentUIData = _uiData as EquipmentUIData;
        if (m_EquipmentUIData == null) // 장비 UI 데이터 없음
        {
            // 에러 로그 출력 : 장비 UI 데이터 잘못됨
            Logger.LogError("EquipmentUIData Is Invalid.");
            return; // 종료
        }

        // 아이템 데이터 가져오기
        var itemData = DataTableManager.Instance.GetItemData(m_EquipmentUIData.ItemId);
        if (itemData == null) // 아이템 데이터 없음
        {
            // 에러 로그 출력 : 아이템 ID 잘못됨, 아이디
            Logger.LogError($"Item data is invalid. ItemId : {m_EquipmentUIData.ItemId}");
            return; // 종료
        }

        // 아이템 등급
        var itemGrade = (ItemGrade)((m_EquipmentUIData.ItemId / 1000) % 10);

        // 등급 배경 텍스쳐
        var gradeBgTexture = Resources.Load<Texture2D>($"Textures/{itemGrade}");
        if (gradeBgTexture != null) // 등급 배경 텍스처 있음
        {
            // 텍스쳐를 스프라이트로 변환 및 배경 이미지 설정
            ItemGradeBg.sprite
                = Sprite.Create(
                    gradeBgTexture,
                    new Rect(0, 0, gradeBgTexture.width,
                    gradeBgTexture.height)
                    , new Vector2(1f, 1f));
        }

        // 아이템 등급 텍스트 설정
        ItemGradeTxt.text = itemGrade.ToString();

        var hexColor = string.Empty; // 등급별 색상 코드
        switch (itemGrade) // 아이템 등급
        {
            case ItemGrade.Common: // 일반
                hexColor = "#1AB3FF"; // 파란색
                break;
            case ItemGrade.Uncommon: // 고급
                hexColor = "#51C52C"; // 초록색
                break;
            case ItemGrade.Rare: // 희귀
                hexColor = "#EA5AFF"; // 분홍색
                break;
            case ItemGrade.Epic: // 에픽
                hexColor = "#FF9900"; // 주황색
                break;
            case ItemGrade.Legendary: // 전설
                hexColor = "#F24949"; // 빨간색
                break;
            default:
                break;
        }

        Color color;

        // Hex 색상 코드를 Color로 변환
        if (ColorUtility.TryParseHtmlString(hexColor, out color))
        {
            ItemGradeTxt.color = color; // 아이템 등급 텍스트
        }

        // 아이디 문자열을 StringBuilder로 변환
        StringBuilder sb = new StringBuilder(m_EquipmentUIData.ItemId.ToString());
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

        // 텍스트 설정
        ItemNameTxt.text = itemData.ItemName; // 아이템 이름
        AttackPowerAmountTxt.text = $"+{itemData.AttackPower}"; // 공격력
        DefenseAmountTxt.text = $"+{itemData.Defense}"; // 방어력
        EquipBtnTxt.text = m_EquipmentUIData.IsEquipped ? "Unequip" : "Equip"; // 장착 버튼
    }

    // 장착 버튼 클릭 함수
    public void OnClickEquipBtn()
    {
        // 유저 인벤토리 데이터 가져오기
        var userInventoryData = UserDataManager.Instance.GetUserData<UserInventoryData>();
        if (userInventoryData == null) // 인벤토리 데이터 없음
        {
            // 에러 로그 출력 : 존재하지 않는 데이터
            Logger.LogError("UserInventoryData Does Not Exist.");
            return;
        }

        if (m_EquipmentUIData.IsEquipped) // 장착 중
        {
            // 아이템 장착 해제
            userInventoryData.UnequipItem(m_EquipmentUIData.SerialNumber, m_EquipmentUIData.ItemId);
        }
        else // 장착 중 아님
        {
            // 아이템 장착
            userInventoryData.EquipItem(m_EquipmentUIData.SerialNumber, m_EquipmentUIData.ItemId);
        }

        // 인벤토리 데이터 저장
        userInventoryData.SaveData();

        // 인벤토리 UI 가져오기
        var inventoryUI = UIManager.Instance.GetActiveUI<InventoryUI>() as InventoryUI;
        if (inventoryUI != null) // 인벤토리 UI 있음
        {
            if (m_EquipmentUIData.IsEquipped) // 장착 중
            {
                // 아이템 장착 해제
                inventoryUI.OnUnequipItem(m_EquipmentUIData.ItemId);
            }
            else
            {
                // 아이템 장착
                inventoryUI.OnEquipItem(m_EquipmentUIData.ItemId);
            }
        }

        CloseUI(); // UI 닫기
    }
}