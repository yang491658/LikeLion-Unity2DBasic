using TMPro;
using UnityEngine;

public class SettingsUI : BaseUI
{
    public TextMeshProUGUI GameVersionTxt; // 게임 버전 텍스트
    public GameObject SoundOnToggle; // 사운드 ON 토글
    public GameObject SoundOffToggle; // 사운드 OFF 토글

    private const string PRIVACY_POLICY_URL = "https://likelion.net/";

    // 정보 설정 함수 (상속)
    public override void SetInfo(BaseUIData _uiData)
    {
        base.SetInfo(_uiData); // 정보 설정 (상속)

        SetGameVersion(); // 게임버전 설정

        // 유저 설정 데이터 가져오기
        var userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        if (userSettingsData != null) // 유저 설정 데이터 있음
        {
            SetSoundSetting(userSettingsData.Sound); // 사운드 설정
        }
    }

    // 게임버전 설정 함수
    private void SetGameVersion()
    {
        // 게임버전 텍스트 설정
        GameVersionTxt.text = $"Version : {Application.version}";
    }

    // 사운드 세팅 설정 함수
    private void SetSoundSetting(bool _sound)
    {
        // 사운드 ON/OFF 토글 활성화 또는 비활성화
        SoundOnToggle.SetActive(_sound);
        SoundOffToggle.SetActive(!_sound);
    }

    // 사운드 ON 토글 클릭 함수
    public void OnClickSoundOnToggle()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::OnClickSoundOnToggle");

        // UI 버튼 클릭 효과음 재생
        AudioManager.Instance.PlaySFX(SFX.ui_button_click);

        // 유저 설정 데이터 가져오기
        var userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        if (userSettingsData != null) // 유저 설정 데이터 있음
        {
            userSettingsData.Sound = false; // 사운드 OFF
            UserDataManager.Instance.SaveUserData(); // 유저 데이터 저장
            AudioManager.Instance.Mute(); // 음소거
            SetSoundSetting(userSettingsData.Sound); // 사운드 세팅 설정
        }
    }

    // 사운드 OFF 토글 클릭 함수
    public void OnClickSoundOffToggle()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::OnClickSoundOffToggle");

        // UI 버튼 클릭 효과음 재생
        AudioManager.Instance.PlaySFX(SFX.ui_button_click);

        // 유저 설정 데이터 가져오기
        var userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        if (userSettingsData != null) // 유저 설정 데이터 있음
        {
            userSettingsData.Sound = true; // 유저 ON
            UserDataManager.Instance.SaveUserData(); // 유저 데이터 저장
            AudioManager.Instance.UnMute(); // 음소거 해제
            SetSoundSetting(userSettingsData.Sound); // 사운도 세팅 설정
        }
    }

    // 개인정보 보호정책 버튼 클릭 함수
    public void OnClickPrivacyPolicyBtn()
    {
        // 일반 로그 출력 : 함수명
        Logger.Log($"{GetType()}::OnClickPrivacyPolicyBtn");

        // UI 버튼 클릭 효과음 재생
        AudioManager.Instance.PlaySFX(SFX.ui_button_click);

        // URL 열기
        Application.OpenURL(PRIVACY_POLICY_URL);
    }
}