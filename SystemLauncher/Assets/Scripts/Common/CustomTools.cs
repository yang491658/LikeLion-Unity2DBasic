using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
public class CustomTools : Editor
{
    // 유저 골드 추가 함수
    [MenuItem("Inflearn / Add User Gold (+100)")]
    public static void AddUserGold()
    {
        // 로컬 저장소에서 골드 가져오기
        var Gold = long.Parse(PlayerPrefs.GetString("Gold"));
        Gold += 100;

        // 로컬 저장소에서 골드 설정 및 저장
        PlayerPrefs.SetString("Gold", Gold.ToString());
        PlayerPrefs.Save();
    }

    // 유저 보석 추가 함수
    [MenuItem("Inflearn / Add User Gem (+10)")]
    public static void AddUserGem()
    {
        // 로컬 저장소에서 보석 가져오기
        var Gem = long.Parse(PlayerPrefs.GetString("Gem"));
        Gem += 10;

        // 로컬 저장소에서 보석 설정 및 저장
        PlayerPrefs.SetString("Gem", Gem.ToString());
        PlayerPrefs.Save();
    }
}
#endif