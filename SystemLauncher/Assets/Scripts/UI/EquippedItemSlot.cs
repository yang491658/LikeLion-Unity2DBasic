using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EquippedItemSlot : MonoBehaviour
{
    public Image AddIcon; // 추가 아이콘
    public Image EquippedItemGradeBg; // 장착 아이템 등급 배경
    public Image EquippedItemIcon; // 장착 아이템 아이콘

    private UserItemData m_EquippedItemData; // 장착 아이템 데이터

    // 아이템 설정 함수
    public void SetItem(UserItemData _userItemData)
    {
        // 장착 아이템 데이터 설정
        m_EquippedItemData = _userItemData;

        // 아이콘 활성화/비활성화
        AddIcon.gameObject.SetActive(false); // 추가 아이콘 비활성화
        EquippedItemGradeBg.gameObject.SetActive(true); // 장착 아이템 등급 배경 활성화
        EquippedItemIcon.gameObject.SetActive(true); // 장착 아이템 아이콘 활성화

        // 아이템 등급 설정
        var itemGrade = (ItemGrade)((m_EquippedItemData.ItemId / 1000) % 10);

        // 등급에 따른 텍스쳐
        var gradeBgTexture = Resources.Load<Texture2D>($"Textures/{itemGrade}");
        if (gradeBgTexture != null) // 텍스쳐 없음
        {
            EquippedItemGradeBg.sprite
                = Sprite.Create(
                    gradeBgTexture,
                    new Rect(0, 0, gradeBgTexture.width, gradeBgTexture.height),
                    new Vector2(1f, 1f));
        }

        // 아이디 문자열을 StringBuilder로 변환
        StringBuilder sb = new StringBuilder(m_EquippedItemData.ItemId.ToString());
        sb[1] = '1'; // 아이콘 이름 규칙 적용
        var itemIconName = sb.ToString(); // 변경된 문자열을 아이콘 이름으로 적용

        // 아이콘 텍스쳐 불러오기
        var itemIconTexture = Resources.Load<Texture2D>($"Textures/{itemIconName}");
        if (itemIconTexture != null) // 아이콘 텍스쳐 없음
        {
            // 텍스쳐를 스프라이트로 변환 및 아이콘 설정
            EquippedItemIcon.sprite
                = Sprite.Create(
                    itemIconTexture,
                    new Rect(0, 0, itemIconTexture.width, itemIconTexture.height),
                    new Vector2(1f, 1f));
        }
    }

    // 아이템 제거 함수
    public void ClearItem()
    {
        // 장착 아이템 데이터 초기화
        m_EquippedItemData = null;

        // 아이콘 활성화/비활성화
        AddIcon.gameObject.SetActive(true); // 추가 아이콘 활성화
        EquippedItemGradeBg.gameObject.SetActive(false); // 장착 아이템 등급 배경 비활성화
        EquippedItemIcon.gameObject.SetActive(false); // 장착 아이템 아이콘 비활성화
    }

    // 장착 아이템 슬롯 클릭 함수
    public void OnClickEquippedItemSlot()
    {
        // UI 데이터 초기화 및 설정
        var uiData = new EquipmentUIData();
        uiData.SerialNumber = m_EquippedItemData.SerialNumber; // 시리얼 번호
        uiData.ItemId = m_EquippedItemData.ItemId; // 아이디
        uiData.IsEquipped = true; // 장착 상태

        UIManager.Instance.OpenUI<EquipmentUI>(uiData); // 장비 UI 열기
    }
}