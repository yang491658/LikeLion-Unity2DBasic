using System;
using UnityEngine;

// 기본 UI 데이터
public class BaseUIData
{
    public Action OnShow; // UI 표시 액션
    public Action OnClose; // UI 닫기 액션
}

public class BaseUI : MonoBehaviour
{
    public Animation m_UIOpenAnim; // UI 열기 애니메이션

    private Action m_OnShow; // UI 표시 액션
    private Action m_OnClose; // UI 닫기 액션

    // 초기화 함수
    public virtual void Init(Transform _anchor)
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::Init");

        // 액션 초기화
        m_OnShow = null;
        m_OnClose = null;

        // 부모 설정
        transform.SetParent(_anchor);

        var rectTransform = GetComponent<RectTransform>(); // 사각트랜스폼 컴포넌트 가져오기

        if (!rectTransform) // 사각트랜스폼 없음
        {
            // 오류 로그 출력 : 사각트랜스폼 없음
            Logger.LogError("UI Does Not Have Rectransform.");
            return; // 종료
        }

        // 사각트랜스폼 컴포넌트 설정
        rectTransform.localPosition = new Vector3(0, 0, 0); // 위치
        rectTransform.localScale = new Vector3(1, 1, 1); // 크기
        rectTransform.offsetMin = new Vector2(0, 0); // 최소 오프셋
        rectTransform.offsetMax = new Vector2(0, 0); // 최대 오프셋
    }

    // 정보 설정 함수
    public virtual void SetInfo(BaseUIData _uiData)
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::SetInfo");

        // 액션 설정
        m_OnShow = _uiData.OnShow; // 표시 액션
        m_OnClose = _uiData.OnClose; // 닫기 액션
    }


    // UI 표시 함수
    public virtual void ShowUI()
    {
        if (m_UIOpenAnim) // UI 열기 애니메이션 있음
        {
            m_UIOpenAnim.Play(); // 애니메이션 재생
        }

        // UI 표시 액션 실행 및 초기화
        m_OnShow?.Invoke(); // 실행
        m_OnShow = null; // 초기화
    }

    // UI 닫기 함수
    public virtual void CloseUI(bool _isCloseAll = false)
    {
        // UI 닫기 액션 실행 및 초기화
        if (!_isCloseAll)
        {
            m_OnClose?.Invoke(); // 실행
        }
        m_OnClose = null; // 초기화

        UIManager.Instance.CloseUI(this); // 해당 UI 닫기 
    }

    // 닫기 버튼 클릭 함수
    public virtual void OnClickCloseButton()
    {
        AudioManager.Instance.PlaySFX(SFX.ui_button_click); // 효과음 재생
        CloseUI();  // UI 닫기
    }
}