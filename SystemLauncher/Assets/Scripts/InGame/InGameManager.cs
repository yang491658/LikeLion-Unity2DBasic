using UnityEngine;

public class InGameManager : Singleton<InGameManager>
{
    public InGameUIController InGameUIController { get; private set; } // 인게임 UI 컨트롤러

    private const string STAGE_PATH = "Stages/"; // 스테이지 경로
    private SpriteRenderer m_Background; // 배경
    private Transform m_Stage; // 스테이지
    private int m_CurrStage; // 현재 스테이지
    private int m_SelectedChapter; // 선택된 챕터

    public bool IsPaused { get; private set; } // 일시정지 여부

    // 초기화 함수 (상속)
    protected override void Init()
    {
        m_IsDestroyOnLoad = true; // 씬 전환 시 삭제

        base.Init(); // 초기화 (상속)

        // 페이드 아웃
        UIManager.Instance.Fade(Color.black, 1f, 0f, 0.5f, 0f, true);
    }

    private void Start()
    {
        InGameUIController = FindAnyObjectByType<InGameUIController>();

        if (!InGameUIController) // 인게임 UI 컨트롤러 없음
        {
            // 오류 로그 출력 : 인게임 UI 컨트롤러 없음
            Logger.LogError("InGameUIContoller Does Not Exist");
            return; // 종류
        }

        InGameUIController.Init(); // 인게임 UI 컨트롤러 초기화
    }

    // 변수 초기화 함수
    private void InitVariables()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::InitVariables");

        // 변수 초기화
        m_Background = GameObject.Find("Background").GetComponent<SpriteRenderer>(); // 배경
        m_Stage = GameObject.Find("Stage").transform; // 스테이지
        m_CurrStage = 1; // 현재 스테이지

        // 유저 플레이 데이터 가져오기
        var userPlayData = UserDataManager.Instance.GetUserData<UserPlayData>();
        if (userPlayData == null) // 유저 플레이 데이터 없음
        {
            // 오류 로그 출력 : 존재하지 않는 데이터
            Logger.LogError("UserPlayData Does Not Exist.");
            return; // 종료
        }

        // 변수 초기화
        m_SelectedChapter = userPlayData.SelectedChapter; // 선택된 챕터
    }

    // 스테이지 불러오기 함수
    private void LoadStage()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::LoadStage");

        // 일반 로그 출력 : 챕터 및 스테이지
        Logger.Log($"Chapter : {m_SelectedChapter} / Stage : {m_CurrStage}");
    }

    // 게임 일시정지 함수
    public void PauseGame()
    {
        IsPaused = true; // 일시정지
    }

    // 게임 재개 함수
    public void ResumeGame()
    {
        IsPaused = false; // 일시정지 해제
    }
}