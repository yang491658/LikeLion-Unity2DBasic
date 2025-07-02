using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    // 애플리케이션 포커스 변경 시 함수
    private void OnApplicationFocus(bool focus)
    {
        if (!focus) // 포커스 해제 (백그라운드 이동)
        {
            if (!InGameManager.Instance.IsPaused) // 게임 진행 중
            {
                var uiData = new BaseUIData(); // 기본 UI 데이터

                UIManager.Instance.OpenUI<PauseUI>(uiData); // 일시정지 UI 열기

                InGameManager.Instance.PauseGame(); // 게임 일시정지
            }
        }
    }

    private void Update()
    {
        if (!InGameManager.Instance.IsPaused) // 게임 진행 중
        {
            HandleInput(); // 입력 처리
        }
    }

    // 입력 처리를 담당하는 메서드
    private void HandleInput()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) // ESC키 입력 해제
        {
            AudioManager.Instance.PlaySFX(SFX.ui_button_click); // UI 버튼 클릭 효과음 재생

            var uiData = new BaseUIData(); // 기본 UI 데이터

            UIManager.Instance.OpenUI<PauseUI>(uiData); // 일시정지 UI 열기

            InGameManager.Instance.PauseGame(); // 게임 일시정지
        }
    }

    // 초기화 함수
    public void Init()
    {
    }

    // 일시정지 버튼 클릭 함수
    public void OnClickPauseBtn()
    {
        AudioManager.Instance.PlaySFX(SFX.ui_button_click); // UI 버튼 클릭 효과음 재생

        var uiData = new BaseUIData(); // 기본 UI 데이터

        UIManager.Instance.OpenUI<PauseUI>(uiData); // 일시정지 UI 열기

        InGameManager.Instance.PauseGame(); // 게임 일시정지
    }
}