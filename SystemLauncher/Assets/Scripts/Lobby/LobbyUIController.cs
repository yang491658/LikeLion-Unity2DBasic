using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{
    public TextMeshProUGUI CurrChapterNameTxt; // 현재 챕터 이름 텍스트
    public RawImage CurrChapterBg; // 현재 챕터 배경

    private void Update()
    {
        HandleInput(); // 입력 처리
    }

    // 입력 처리 함수
    private void HandleInput()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) // ESC키 입력 해제
        {
            AudioManager.Instance.PlaySFX(SFX.ui_button_click); // UI 버튼 클릭 효과음 재생

            var frontUI = UIManager.Instance.GetCurrentFrontUI(); // 현재 최상단 UI 가져오기

            if (frontUI) // 최상단 UI 있음
            {
                frontUI.CloseUI(); // 최상단 UI 닫기
            }
            else // 최상단 UI 없음
            {
                // UI 데이터 설정
                var uiData = new ConfirmUIData(); // 초기화
                uiData.ConfirmType = ConfirmType.CANCEL; // 확인 타입
                uiData.TitleTxt = "Quit"; // 제목
                uiData.DescTxt = "Do You Want To Quit Game?"; // 설명
                uiData.OKBtnTxt = "Quit"; // 확인 버튼
                uiData.CancelBtnTxt = "Cancel"; // 취소 버튼
                uiData.OnClickOKBtn = () => // 확인 버튼 클릭
                {
                    Application.Quit(); // 종료
                };
                UIManager.Instance.OpenUI<ConfirmUI>(uiData); // 확인 UI 열기
            }
        }
    }

    // 초기화 함수
    public void Init()
    {
        UIManager.Instance.EnableGoodsUI(true); // 재화 UI 활성화

        SetCurrentChapter(); // 현재 챕터 설정
    }

    // 현재 챕터 설정 함수
    public void SetCurrentChapter()
    {
        // 유저 플레이 데이터 가져오기
        var userPlayData = UserDataManager.Instance.GetUserData<UserPlayData>();
        if (userPlayData == null) // 유저 플레이 데이터 없음
        {
            // 오류 로그 출력 : 존재하지 않는 데이터
            Logger.LogError("UserPlayData Does Not Exist.");
            return; // 종료
        }

        // 현재 챕터 데이터 가져오기
        var currChapterData = DataTableManager.Instance.GetChapterData(userPlayData.SelectedChapter);
        if (currChapterData == null) // 현재 챕터 데이터 없음
        {
            // 오류 로그 출력 : 존재하지 않는 데이터
            Logger.LogError("CurrChapterData Does Not Exist.");
            return; // 종료
        }

        // 현재 챕터 텍스트 설정
        CurrChapterNameTxt.text = currChapterData.ChapterName;

        // 배경 텍스쳐 불러오기
        var bgTexture = Resources
            .Load($"ChapterBG/Background_{userPlayData.SelectedChapter.ToString("D3")}") as Texture2D;
        if (bgTexture != null) // 베경 텍스쳐 잇음
        {
            // 현재 배경 텍스쳐 설정
            CurrChapterBg.texture = bgTexture;
        }
    }

    // 세팅 버튼 클릭 함수
    public void OnClickSettingsBtn()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::OnClickSettingsBtn");

        // UI 데이터 설정
        var uiData = new BaseUIData();

        UIManager.Instance.OpenUI<SettingsUI>(uiData); // 설정 UI 열기
    }

    // 프로필 버튼 클릭 함수
    public void OnClickProfileBtn()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::OnClickProfileBtn");

        // UI 데이터 설정
        var uiData = new BaseUIData();

        UIManager.Instance.OpenUI<InventoryUI>(uiData); // 인벤토리 UI 열기
    }

    // 현재 챕터 클릭 함수
    public void OnClickCurrChapter()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::OnClickCurrChapter");

        // UI 데이터 설정
        var uiData = new BaseUIData();

        UIManager.Instance.OpenUI<ChapterListUI>(uiData); // 챕티 리스트 UI 열기
    }

    // 시작 버튼 클릭 함수
    public void OnClickStartBtn()
    {
        Logger.Log($"{GetType()}::OnClickStartBtn");

        AudioManager.Instance.PlaySFX(SFX.ui_button_click); // UI 버튼 클릭 효과음 재생
        AudioManager.Instance.StopBGM(); // 배경음 중지

        LobbyManager.Instance.StartInGame(); // 인게임 시작
    }
}