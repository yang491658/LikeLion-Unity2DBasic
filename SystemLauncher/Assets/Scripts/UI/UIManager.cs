using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Transform UICanvas; // UI 캔버스
    public Transform ClosedUI; // 닫기 UI

    private BaseUI FrontUI; // 최상단 UI
    private Dictionary<System.Type, GameObject> OpenUIPool // 열기 UI 풀링
        = new Dictionary<System.Type, GameObject>();
    private Dictionary<System.Type, GameObject> ClosedUIPool // 닫기 UI 풀링
        = new Dictionary<System.Type, GameObject>();

    private GoodsUI m_GoodsUI; // 재화 UI

    public Image m_Fade; // 페이드

    // 초기화 함수 (상속)
    protected override void Init()
    {
        base.Init(); // 초기화 (상속)

        // 페이드 닫기
        m_Fade.transform.localScale = Vector3.zero;

        // 재화 UI 설정
        m_GoodsUI = FindFirstObjectByType<GoodsUI>();
        if (!m_GoodsUI) // 재화 UI 있음
        {
            // 일반 로그 출력 : UI 
            Logger.Log("No Stats UI Component Found.");
        }
    }

    // UI 가져오기 함수
    private BaseUI GetUI<T>(out bool _isOpen)
    {
        System.Type uiType = typeof(T); // 타입 변환

        // 열림 여부 및 UI 초기화
        _isOpen = false; // 열림 여부
        BaseUI ui = null; // UI

        if (OpenUIPool.ContainsKey(uiType)) // 열기 UI 풀링 내 타입 일치
        {
            ui = OpenUIPool[uiType].GetComponent<BaseUI>(); // 컴포넌트 가져오기
            _isOpen = true; // 열림
        }
        else if (ClosedUIPool.ContainsKey(uiType)) // 닫기 UI 풀링 내 타입 일치
        {
            ui = ClosedUIPool[uiType].GetComponent<BaseUI>(); // 컴포넌트 가져오기
            ClosedUIPool.Remove(uiType); // 해당 타입 제거
        }
        else // 그 외
        {
            var uiObj = Instantiate(Resources.Load($"UI/{uiType}", typeof(GameObject))) as GameObject;
            ui = uiObj.GetComponent<BaseUI>();
        }

        return ui; // UI 반환
    }

    // UI 열기 함수
    public void OpenUI<T>(BaseUIData _uiData)
    {
        System.Type uiType = typeof(T); // 타입 변환

        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::OpenUI({uiType})");

        // 열림 여부 및 UI 초기화
        bool isOpen = false; // 열림 여부
        var ui = GetUI<T>(out isOpen); // UI

        if (!ui) // UI 없음
        {
            // 오류 로그 출력 : 존재하지 않는 UI
            Logger.LogError($"{uiType} Does Not Exist.");
            return; // 종료
        }

        if (isOpen) // 열림
        {
            // 오류 로그 출력 : 이미 열림
            Logger.LogError($"{uiType} Is Already Open.");
            return; // 종료
        }

        // UI 설정
        var index = UICanvas.childCount - 1; // 인덱스 설정
        ui.Init(UICanvas); // 초기화
        ui.transform.SetSiblingIndex(index); // 형제 인덱스 설정
        ui.gameObject.SetActive(true); // 오브젝트 활성화
        ui.SetInfo(_uiData); // 정보 설정
        ui.ShowUI(); // UI 표시

        // 최상단 UI 및 열기 풀링 추가
        FrontUI = ui; // 최상단 UI
        OpenUIPool[uiType] = ui.gameObject; // 열기 풀링 추가
    }

    // UI 닫기 함수
    public void CloseUI(BaseUI ui)
    {
        System.Type uiType = ui.GetType(); // 타입 변환

        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::CloseUI({uiType})");

        // UI 설정
        ui.gameObject.SetActive(false); // 오브젝트 활성화
        OpenUIPool.Remove(uiType); // 열기 풀링 추가
        ClosedUIPool[uiType] = ui.gameObject; //
        ui.transform.SetParent(ClosedUI);

        // 최상단 UI 설정
        FrontUI = null; // 최상단 UI 초기화
        var lastChild = UICanvas.GetChild(UICanvas.childCount - 1); // UI 캔버스의 마지막 자식
        if (lastChild) // 마지막 자식 있음
        {
            // 최상단 UI 설정 = 마지막 자식
            FrontUI = lastChild.gameObject.GetComponent<BaseUI>();
        }
    }

    // 활성화 UI 가져오기 함수
    public BaseUI GetActiveUI<T>()
    {
        var uiType = typeof(T); // 타입 변환

        return OpenUIPool.ContainsKey(uiType) // 열기 UI 풀링 내 타입 일치
            ? OpenUIPool[uiType].GetComponent<BaseUI>() // UI 반환
            : null; // NULL 반환
    }

    // 열린 UI 존재 여부 함수
    public bool ExistsOpenUI() => FrontUI != null; // 최상단 UI 유무

    // 현재 최상단 UI 가져오기 함수
    public BaseUI GetCurrentFrontUI() => FrontUI; // 최상단 UI

    // 현재 최상단 UI 닫기 함수
    public void CloseCurrFrontUI() => FrontUI.CloseUI(); // 최상단 UI 닫기

    // 모든 열린 UI 닫기 함수
    public void CloseAllOpenUI()
    {
        while (FrontUI)
        {
            FrontUI.CloseUI(true);
        }
    }

    // 재화 UI 활성화 함수
    public void EnableGoodsUI(bool _value)
    {
        // 재화 UI 게임오브젝트 활성화
        m_GoodsUI.gameObject.SetActive(_value);

        if (_value)
        {
            m_GoodsUI.SetValues(); // 재화 UI 값 설정
        }
    }

    // 페이드 함수
    public void Fade(Color color, float startAlpha, float endAlpha, float duration,
        float startDelay, bool deactiveOnFinish, Action onFinish = null)
    {
        // 페이드 코루틴
        StartCoroutine(FadeCoroutine(color, startAlpha, endAlpha, duration,
            startDelay, deactiveOnFinish, onFinish));
    }

    // 페이드 코루틴
    private IEnumerator FadeCoroutine(Color color, float startAlpha, float endAlpha, float duration,
        float startDelay, bool deactiveOnFinish, Action onFinish)
    {
        yield return new WaitForSeconds(startDelay); // 초반 딜레이

        // 페이드 열기 및 색상 설정
        m_Fade.transform.localScale = Vector3.one; // 열기
        m_Fade.color = new Color(color.r, color.g, color.b, startAlpha); // 색상

        var startTime = Time.realtimeSinceStartup; // 시작 시간
        while (Time.realtimeSinceStartup - startTime < duration) // 페이드 시간 적용
        {
            // 페이드 색상 설정
            m_Fade.color = new Color(color.r, color.g, color.b,
                Mathf.Lerp(startAlpha, endAlpha, (Time.realtimeSinceStartup - startTime) / duration));
            yield return null;
        }

        // 페이드 색상 초기화
        m_Fade.color = new Color(color.r, color.g, color.b, endAlpha);

        if (deactiveOnFinish) // 종료 상태 감지
        {
            // 페이드 닫기
            m_Fade.transform.localScale = Vector3.zero;
        }

        // 종료 함수 실행
        onFinish?.Invoke();
    }

    // 페이드 취소 함수
    public void CancelFade()
    {
        // 페이드 닫기
        m_Fade.transform.localScale = Vector3.zero;
    }
}
