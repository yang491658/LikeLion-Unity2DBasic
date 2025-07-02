using Gpm.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 챕터 스크롤 아이템 데이터
public class ChapterScrollItemData : InfiniteScrollData
{
    public int ChapterNo; // 챕터 번호
}

// 챕터 스크롤 아이템
public class ChapterScrollItem : InfiniteScrollItem
{
    public GameObject CurrChapter; // 현재 챕터
    public RawImage CurrChapterBg; // 현재 챕터 배경
    public Image Dim; // 음영
    public Image LockIcon; // 잠금 아이콘
    public Image Round; // 고리
    public ParticleSystem ComingSoonFX; // 등장 효과음
    public TextMeshProUGUI ComingSoonTxt; // 등장 텍스트

    private ChapterScrollItemData m_ChapterScrollItemData; // 챕터 스크롤 아이템 데이터

    // 데이터 업데이트 함수 (상속)
    public override void UpdateData(InfiniteScrollData _scrollData)
    {
        base.UpdateData(_scrollData); // 데이터 업데이터 (상속)

        // 챕터 스크롤 아이템 데이터 초기화
        m_ChapterScrollItemData = _scrollData as ChapterScrollItemData;
        if (m_ChapterScrollItemData == null) // 챕터 스크롤 아이템 없음
        {
            // 오류 로그 출력 :챕터 스크롤 아이템 잘못됨
            Logger.LogError("Invalid ChapterScrollItemData.");
            return; // 종료
        }

        if (m_ChapterScrollItemData.ChapterNo > GlobalDefine.MAX_CHAPTER) // 최대 챕터 초과
        {
            // 오브젝트 비활성화 및 활성화
            CurrChapter.SetActive(false); // 현재 챕터 비활성화
            ComingSoonFX.gameObject.SetActive(true); // 등장 효과음 활성화
            ComingSoonTxt.gameObject.SetActive(true); // 등장 텍스트 활성화
        }
        else // 최대 챕터 이하
        {
            // 오브젝트 비활성화 및 활성화
            CurrChapter.SetActive(true); // 현재 챕터 활성화
            ComingSoonFX.gameObject.SetActive(false); // 등장 효과음 비활성화
            ComingSoonTxt.gameObject.SetActive(false); // 등장 텍스트 비활성화

            // 유저 플레이 데이터 가져오기
            var userPlayData = UserDataManager.Instance.GetUserData<UserPlayData>();
            if (userPlayData != null) // 유저 플레이 데이터 잇음
            {
                // 잠금 여부
                var isLocked = m_ChapterScrollItemData.ChapterNo > userPlayData.MaxClearedChapter + 1;

                // 오브젝트 비활성화 및 활성화
                Dim.gameObject.SetActive(isLocked); // 음영
                LockIcon.gameObject.SetActive(isLocked); // 잠금 아이콘
                Round.color = isLocked ? new Color(0.5f, 0.5f, 0.5f, 1f) : Color.white; // 고리 색상
            }

            // 배경 텍스쳐 불러오기
            var bgTexture = Resources
                .Load($"ChapterBG/Background_{m_ChapterScrollItemData.ChapterNo.ToString("D3")}")
                as Texture2D;
            if (bgTexture != null) // 배경 텍스쳐 있음
            {
                // 현재 챕터 배경 텍스쳐 설정
                CurrChapterBg.texture = bgTexture;
            }
        }
    }
}