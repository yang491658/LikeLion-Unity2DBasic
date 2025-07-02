using UnityEngine;

public class LobbyManager : Singleton<LobbyManager>
{
    public LobbyUIController LobbyUIController { get; private set; } // 로비 UI 컨트롤러

    private bool m_IsLoadingInGame; // 인게임 로딩 중 여부

    // 초기화 함수 (상속)
    protected override void Init()
    {
        m_IsDestroyOnLoad = true; // 씬 전환 시 삭제
        m_IsLoadingInGame = false; // 인게임 로딩 중 아님

        base.Init(); // 초기화 (상속)
    }

    private void Start()
    {
        LobbyUIController = FindAnyObjectByType<LobbyUIController>();

        if (!LobbyUIController) // 로비 UI 컨트롤러 없음
        {
            // 오류 로그 출력 : 로비 UI 컨트롤러 없음
            Logger.LogError("LobbyUIController Does Not Exist.");
            return; // 종료
        }

        LobbyUIController.Init(); // 로비 UI 컨트롤러 초기화
        AudioManager.Instance.PlayBGM(BGM.lobby); // 로비 배경음 재생
    }

    // 인게임 시작 함수
    public void StartInGame()
    {
        if (m_IsLoadingInGame) // 인게임 로딩 중
        {
            return; // 종료
        }

        m_IsDestroyOnLoad = true; // 씬 전환 시 삭제

        // 페이드 인
        UIManager.Instance.Fade(Color.black, 0f, 1f, 0.5f, 0f, false, () =>
        {
            UIManager.Instance.CloseAllOpenUI(); // 모든 열린 UI 닫기
            SceneLoader.Instance.LoadScene(SceneType.InGame); // 인게임 씬 불러오기
        });
    }
}