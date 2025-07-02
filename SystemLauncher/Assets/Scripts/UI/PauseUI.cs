public class PauseUI : BaseUI
{
    // 재개 클릭 함수
    public void OnClickResume()
    {
        InGameManager.Instance.ResumeGame(); // 게임 재개

        CloseUI(); // UI 닫기
    }

    // 홈 클릭 함수
    public void OnClickHome()
    {
        SceneLoader.Instance.LoadScene(SceneType.Lobby); // 로비 씬 불러오기

        CloseUI(); // UI 닫기
    }
}