using UnityEngine;

public class UserGoodsData : UserData
{
    public long Gold { get; set; } // 골드 수량
    public long Gem { get; set; } // 보석 수량

    // 기본값 설정 함수
    public void SetDefault()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::SetDefault");

        // 골드 및 보석 초기화
        Gold = 0;
        Gem = 0;
    }

    // 데이터 불러오기 함수
    public bool LoadData()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::LoadData");

        bool result = false; 

        try
        {
            // 로컬 저장소에서 골드 및 보석 가져오기
            Gold = long.Parse(PlayerPrefs.GetString("Gold"));
            Gem = long.Parse(PlayerPrefs.GetString("Gem"));

            result = true; // 결과 저장

            // 일반 로그 출력 : 골드 및 보석
            Logger.Log($"Gold : {Gold} / Gem : {Gem}");
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
            // 로컬 저장소에서 골드 및 보석 설정 및 저장
            PlayerPrefs.SetString("Gold", Gold.ToString());
            PlayerPrefs.SetString("Gem", Gem.ToString());
            PlayerPrefs.Save();

            result = true; // 결과 저장

            // 일반 로그 출력 : 골드 및 보석 
            Logger.Log($"Gold : {Gold} / Gem : {Gem}");
        }
        catch (System.Exception e) // 오류 발생
        {
            // 오류 로그 출력 : 저장 실패
            Logger.LogError("Save Failed (" + e.Message + ")");
        }

        return result; // 결과 반환
    }
}