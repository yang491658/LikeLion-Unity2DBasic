using System;
using TMPro;
using UnityEngine.UI;

// 확인 타입
public enum ConfirmType
{
    OK, // 수락
    CANCEL // 취소
}

// 확인 UI 데이터 (상속)
public class ConfirmUIData : BaseUIData
{
    public ConfirmType ConfirmType; // 확인 타입
    public string TitleTxt; // 타이틀 텍스트
    public string DescTxt; // 설명 텍스트
    public string OKBtnTxt; // 수락 버튼 텍스트
    public Action OnClickOKBtn; // 수락 버튼 클릭 액션
    public string CancelBtnTxt; // 취소 버튼 텍스트
    public Action OnClickCancelBtn; // 취소 버튼 클릭 액션
}

public class ConfirmUI : BaseUI
{
    public TextMeshProUGUI TitleTxt = null; // 타이틀 텍스트
    public TextMeshProUGUI DescTxt = null; // 설명 텍스트
    public Button OKBtn = null; // 수락 버튼
    public Button CancelBtn = null; // 취소버튼
    public TextMeshProUGUI OKBtnTxt = null; // 수락 버튼 텍스트
    public TextMeshProUGUI CancelBtnTxt = null; // 취소 버튼 텍스트

    private ConfirmUIData m_ConfirmUIData = null; // 확인 UI 데이터
    private Action m_OnClickOKBtn = null; // 수락 버튼 클릭 액션
    private Action m_OnClickCancelBtn = null; // 취소 버튼 클릭 액션

    // 정보 설정 함수 (상속)
    public override void SetInfo(BaseUIData _uiData)
    {
        base.SetInfo(_uiData); // 정보 설정 (상속)

        m_ConfirmUIData = _uiData as ConfirmUIData;

        // 텍스트 및 액션 설정
        TitleTxt.text = m_ConfirmUIData.TitleTxt;
        DescTxt.text = m_ConfirmUIData.DescTxt;
        OKBtnTxt.text = m_ConfirmUIData.OKBtnTxt;
        m_OnClickOKBtn = m_ConfirmUIData.OnClickOKBtn;
        CancelBtnTxt.text = m_ConfirmUIData.CancelBtnTxt;
        m_OnClickCancelBtn = m_ConfirmUIData.OnClickCancelBtn;

        // 수락 및 취소 버튼 활성화
        OKBtn.gameObject.SetActive(true);
        CancelBtn.gameObject.SetActive(m_ConfirmUIData.ConfirmType == ConfirmType.CANCEL);
    }

    // 수락 버튼 클릭 함수
    public void OnClickOKBtn()
    {
        m_OnClickOKBtn?.Invoke(); // 수락 버튼 클릭 액션 실행
        CloseUI(); // UI 닫기
    }

    // 취소 버튼 클릭 함수
    public void OnClickCancelBtn()
    {
        m_OnClickCancelBtn?.Invoke(); // 취소 버튼 클릭 액션 실행
        CloseUI(); // UI 닫기 
    }
}