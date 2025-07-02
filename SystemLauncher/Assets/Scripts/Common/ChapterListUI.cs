using Gpm.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChapterListUI : BaseUI
{
    public InfiniteScroll ChapterScrollList; // 챕터 스크롤 리스트
    public GameObject SelectedChapterName; // 선택된 챕터 이름
    public TextMeshProUGUI SelectedChapterNameTxt; // 선택된 챕터 이름 텍스트
    public Button SelectBtn; // 선택 버튼

    private int SelectedChapter; // 선택된 챕터

    // 정보 설정 함수 (상속)
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData); // 정보 설정 (상속)

        // 유저 플레이 데이터 가져오기
        var userPlayData = UserDataManager.Instance.GetUserData<UserPlayData>();
        if (userPlayData == null) // 유저 플레이 데이터 없음
        {
            return; // 종료
        }

        // 선택된 챕터 설정
        SelectedChapter = userPlayData.SelectedChapter;

        SetSelectedChapter(); // 선택된 챕터 설정
        SetChapterScrollList(); // 챕터 스크롤 리스트 설정

        // 챕터 스크롤 리스트 이동
        ChapterScrollList.MoveTo(SelectedChapter - 1, InfiniteScroll.MoveToType.MOVE_TO_CENTER);
        ChapterScrollList.OnSnap = (currentSnappedIndex) =>
        {
            // 활성화 챕터 리스트 UI 가져오기
            var chapterListUI = UIManager.Instance.GetActiveUI<ChapterListUI>() as ChapterListUI;
            if (chapterListUI != null) // 챕터 리스트 UI 있음
            {
                chapterListUI.OnSnap(currentSnappedIndex + 1);
            }
        };
    }

    // 선택된 챕터 설정 함수
    private void SetSelectedChapter()
    {
        if (SelectedChapter <= GlobalDefine.MAX_CHAPTER) // 최대 챕터 이하
        {
            // 오브젝트 활성화
            SelectedChapterName.SetActive(true); // 선택된 챕터 이름
            SelectBtn.gameObject.SetActive(true); // 선택 버튼

            // 챕터 데이터 가져오기
            var itemData = DataTableManager.Instance.GetChapterData(SelectedChapter);
            if (itemData != null) // 아이템 데이터
            {
                // 선택된 챕터 이름 텍스트 설정
                SelectedChapterNameTxt.text = itemData.ChapterName;
            }
        }
        else // 최대 챕터 초과
        {
            // 오브젝트 비활성화
            SelectedChapterName.SetActive(false); // 선택된 챕터 이름
            SelectBtn.gameObject.SetActive(false); // 선택 버튼
        }
    }

    // 챕터 스크롤 리스트 설정 함수
    private void SetChapterScrollList()
    {
        ChapterScrollList.Clear(); // 챕터 스크롤 리스트 초기화

        for (int i = 1; i <= GlobalDefine.MAX_CHAPTER + 1; i++) // 최대 챕터 내
        {
            // 챕터 아이템 데이터 초기화 및 설정
            var chapterItemData = new ChapterScrollItemData(); // 초기화
            chapterItemData.ChapterNo = i; // 챕터 번호
            ChapterScrollList.InsertData(chapterItemData); // 데이터 입력
        }
    }

    // 스냅 함수
    public void OnSnap(int _selectedChapter)
    {
        // 선택된 챕터 설정
        SelectedChapter = _selectedChapter;
        SetSelectedChapter();
    }

    // 선택 클릭 함수
    public void OnClickSelect()
    {
        // 유저 플레이 데이터 가져오기
        var userPlayData = UserDataManager.Instance.GetUserData<UserPlayData>();
        if (userPlayData == null) // 유저 플레이어 데이터 없음
        {
            // 오류 로그 출력 : 존재하지 않는 데이터
            Logger.LogError("UserPlayData Does Not Exist.");
            return; // 종료
        }

        if (SelectedChapter <= userPlayData.MaxClearedChapter + 1)
            // 선택된 챕터가 최대 클리어 챕터 이하
        {
            // 유저 플레이 데이터 선택된 챕터 설정
            userPlayData.SelectedChapter = SelectedChapter;

            // 현재 챕터 설정
            LobbyManager.Instance.LobbyUIController.SetCurrentChapter();

            CloseUI(); // UI 닫기
        }
    }
}