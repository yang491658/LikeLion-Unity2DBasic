using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Animation LogoAnim; // 로고 애니메이션
    public TextMeshProUGUI LogoTxt; // 로고 텍스트

    public GameObject Title; // 타이틀
    public Slider LoadingSlider; // 로딩 슬라이더
    public TextMeshProUGUI LoadingProgressTxt; // 로딩 텍스트

    //https://docs.unity3d.com/ScriptReference/AsyncOperation.html
    private AsyncOperation AsyncOp; // 비동기 작업

    private void Awake()
    {
        LogoAnim.gameObject.SetActive(true); // 애니메이션 활성화
        Title.SetActive(false); // 타이틀 비활성화
    }

    private void Start()
    {
        // 유저 데이터 불러오기
        UserDataManager.Instance.LoadUserData();

        if (!UserDataManager.Instance.IsExistData) // 유저 데이터 있음
        {
            UserDataManager.Instance.SetDefaultUserData(); // 기본 유저 데이터 설정
            UserDataManager.Instance.SaveUserData(); // 유저 데이터 저장
        }

        // 챕터 데이터 가져오기
        ChapterData chapter01 = DataTableManager.Instance.GetChapterData(10);
        ChapterData chapter02 = DataTableManager.Instance.GetChapterData(50);

        //// 확인 UI 데이터 초기화 및 설정
        //var confirmUIData = new ConfirmUIData();
        //confirmUIData.ConfirmType = ConfirmType.CANCEL;
        //confirmUIData.TitleTxt = "UI Test";
        //confirmUIData.DescTxt = "This Is UI Text.";
        //confirmUIData.OKBtnTxt = "확인";
        //confirmUIData.CancelBtnTxt = "취소";

        //// 확인 UI 열기
        //UIManager.Instance.OpenUI<ConfirmUI>(confirmUIData);

        AudioManager.Instance.OnLoadUserData(); // 유저 데이터 불러오기

        UIManager.Instance.EnableGoodsUI(false); // 재화 UI 비활성화

        StartCoroutine(LoadGame()); // 게임 불러오기
    }

    // 게임 불러오기 코루틴
    private IEnumerator LoadGame()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::LoadGame");

        //// 로비 배경음 재생 및 대기
        //AudioManager.Instance.PlayBGM(BGM.lobby);
        //yield return new WaitForSeconds(5); // 대기

        //// 배경음 일시정지 및 대기
        //AudioManager.Instance.PauseBGM();
        //yield return new WaitForSeconds(5); // 대기

        //// 배경음 재개 및 대기
        //AudioManager.Instance.ResumeBGM();
        //yield return new WaitForSeconds(5); // 대기

        //// 배경음 정지
        //AudioManager.Instance.StopBGM();

        // 애니메이션 재생 및 대기
        LogoAnim.Play(); // 재생
        yield return new WaitForSeconds(LogoAnim.clip.length); // 대기

        LogoAnim.gameObject.SetActive(false); // 애니메이션 비활성화
        Title.SetActive(true); // 타이틀 활성화

        // 비동기 작업 설정 = 씬 비동기 불러오기
        AsyncOp = SceneLoader.Instance.LoadSceneAsync(SceneType.Lobby);
        if (AsyncOp == null) // 비동기 작업 없음
        {
            // 오류 로그 출력 : 비동기 작업 오류
            Logger.LogError("Lobby Async Loading Error.");
            yield break; // 종료
        }

        // 비동기 작업 중지
        AsyncOp.allowSceneActivation = false;

        // 로딩 슬라이더 및 텍스트 설정 및 대기
        LoadingSlider.value = 0.5f; // 슬라이더
        LoadingProgressTxt.text = $"{(int)(LoadingSlider.value * 100)}%"; // 텍스트
        yield return new WaitForSeconds(0.5f); // 대기

        while (!AsyncOp.isDone) // 로딩 진행 중
        {
            // 로딩 슬라이더 및 텍스트 업데이트
            LoadingSlider.value = AsyncOp.progress < 0.5f ? 0.5f : AsyncOp.progress;
            LoadingProgressTxt.text = $"{(int)(LoadingSlider.value * 100)}%";

            if (AsyncOp.progress >= 0.9f) // 비동기 프로세스 90% 완료
            {
                // 비동기 작업 시작
                AsyncOp.allowSceneActivation = true; 
                yield break;
            }

            yield return null;
        }
    }
}