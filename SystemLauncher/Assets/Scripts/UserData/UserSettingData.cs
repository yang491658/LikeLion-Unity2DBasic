using UnityEngine;

public class UserSettingsData : UserData
{
    public bool Sound { get; set; } // 사운드 설정

    // 기본값 설정 함수
    public void SetDefault()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::SetDefault");

        Sound = true; // 사운드 ON
    }

    // 데이터 불러오기 함수
    public bool LoadData()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::LoadData");

        bool result = false;

        try
        {
            // 로컬 저장소에서 사운드 가져오기
            Sound = PlayerPrefs.GetInt("Sound") == 1 ? true : false;
        
            result = true; // 결과 저장

            // 일반 로그 출력 : 사운드
            Logger.Log($"Sound : {Sound}");
        }
        catch (System.Exception e) // 오류 발생
        {
            // 오류 로그 출력 : 불러오기 실패
            Logger.LogError("Load Failed (" + e.Message + ")");
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
            // 로컬 저장소에서 사운드 설정 및 저장
            PlayerPrefs.SetInt("Sound", Sound ? 1 : 0); // 설정
            PlayerPrefs.Save();  // 저장
            
            result = true; // 결과 저장

            // 일반 로그 출력 : 사운드
            Logger.Log($"Sound : {Sound}");
        }
        catch (System.Exception e) // 오류 발생
        {
            // 오류 로그 출력 : 저장 실패
            Logger.LogError("Save Failed (" + e.Message + ")");
        }

        return result; // 결과 반환
    }
}