using UnityEngine;

public class UserPlayData : UserData
{
    public int MaxClearedChapter { get; set; } // 최대 클리어 챕터
    public int SelectedChapter { get; set; } = 1; // 선택된 챕터

    // 기본 설정 함수
    public void SetDefault()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::SetDefaultData");

        // 챕터 초기화
        MaxClearedChapter = 0; // 최대 클리어
        SelectedChapter = 1; // 선택
    }

    // 데이터 불러오기 함수
    public bool LoadData()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;

        try
        {
            // 로컬 저장소에서 최대 클리어 챕터 가져오기
            MaxClearedChapter = PlayerPrefs.GetInt("MaxClearedChapter");

            // 선택된 챕터 = 최대 클리어 챕터 + 1
            SelectedChapter = MaxClearedChapter + 1;

            result = true; // 결과 저장

            // 일반 로그 출력
            Logger.Log($"MaxClearedChpater : {MaxClearedChapter}");
        }
        catch (System.Exception e) // 오류 발생
        {
            // 오류 로그 출력 : 불러오기 실패
            Logger.Log($"Load Failed. (" + e.Message + ")");
        }

        return result; // 결과 반환
    }

    // 데이터 저장 함수
    public bool SaveData()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::SaveData");

        bool result = false;

        try
        {
            // 로컬 저장소에서 최대 클리어 챕터 설정 및 저장
            PlayerPrefs.SetInt("MaxClearedChapter", MaxClearedChapter); // 설정
            PlayerPrefs.Save(); // 저장

            result = true; // 결과 저장

            // 일반 로그 출력
            Logger.Log($"MaxClearedChpater : {MaxClearedChapter}");
        }
        catch (System.Exception e) // 오류 발생
        {
            // 오류 로그 출력 : 불러오기 실패
            Logger.Log($"Save Failed. (" + e.Message + ")");
        }

        return result; // 결과 반환
    }
}