using System.Collections.Generic;
using UnityEngine;

// 배경음
public enum BGM
{
    lobby, // 로비
    COUNT // 배경음 수
}

// 효과음
public enum SFX
{
    chapter_clear, // 챕터 클리어
    stage_clear, // 스테이지 클리어
    ui_button_click, // UI 버튼 클릭
    COUNT // 효과음 수
}

public class AudioManager : Singleton<AudioManager>
{
    public Transform BGM; // 배경음
    public Transform SFX; // 효과음

    private const string AUDIO_PATH = "Audio"; // 오디오 경로

    private AudioSource currentBGM; // 현재 배경음
    private Dictionary<BGM, AudioSource> BGMDic = new Dictionary<BGM, AudioSource>(); // 배경음 배열
    private Dictionary<SFX, AudioSource> SFXDic = new Dictionary<SFX, AudioSource>(); // 효과음 배열

    // 초기화 함수 (상속)
    protected override void Init()
    {
        base.Init(); // 초기화 (상속)

        LoadBGM(); // 배경음 불러오기
        LoadSFX(); // 효과음 불러오기
    }

    // 배경음 불러오기 함수
    private void LoadBGM()
    {
        for (int i = 0; i < (int)global::BGM.COUNT; i++) // 배경음 수
        {
            // 오디오 설정
            var audioName = ((BGM)i).ToString(); // 이름
            var pathStr = $"{AUDIO_PATH}/{audioName}"; // 파일 경로
            var audioClip = Resources.Load(pathStr, typeof(AudioClip)) as AudioClip; // 클립 가져오기

            if (!audioClip) // 오디오 클립 없음
            {
                // 오류 로그 출력 : 존재하지 않는 오디오
                Logger.LogError($"{audioName} Clip Does Not Exist.");
                continue; // 건너뛰기
            }

            // 새 오브젝트 생성 및 부모 설정
            var newGO = new GameObject(audioName);  // 생성
            newGO.transform.parent = BGM; // 부모

            // 오디오소스 컴포넌트 추가 및 설정
            var newAudioSource = newGO.AddComponent<AudioSource>(); // 컴포넌트 추가
            newAudioSource.clip = audioClip; // 오디오 클립 설정
            newAudioSource.loop = true;  // 반복 재생 활성화
            newAudioSource.playOnAwake = false; // 자동 재생 비활성화

            BGMDic[(BGM)i] = newAudioSource; // 배경음 배열 항목 추가
        }
    }

    // 효과음 불러오기 함수
    private void LoadSFX()
    {
        for (int i = 0; i < (int)global::SFX.COUNT; i++) // 효과음 수
        {
            var audioName = ((SFX)i).ToString(); // 이름
            var pathStr = $"{AUDIO_PATH}/{audioName}"; // 파일 경로
            var audioClip = Resources.Load(pathStr, typeof(AudioClip)) as AudioClip; // 클립 가져오기

            if (!audioClip) // 오디오 클립 없음
            {
                // 오류 로그 출력 : 존재하지 않는 오디오
                Logger.LogError($"{audioName} Clip Does Not Exist.");
                continue; // 건너뛰기
            }

            // 새 오브젝트 생성 및 부모 설정
            var newGO = new GameObject(audioName); // 생성
            newGO.transform.parent = SFX; // 부모

            // 오디오소스 컴포넌트 추가 및 설정
            var newAudioSource = newGO.AddComponent<AudioSource>(); // 컴포넌트 추가
            newAudioSource.clip = audioClip; // 오디오 클립 설정
            newAudioSource.loop = false; // 반복 재생 비활성화
            newAudioSource.playOnAwake = false; // 자동 재생 비활성화

            SFXDic[(SFX)i] = newAudioSource; // 효과음 배열 항목 추가
        }
    }

    // 배경음 재생 함수
    public void PlayBGM(BGM _bgm)
    {
        if (currentBGM) // 현재 배경음 있음
        {
            // 현재 배경음 정지 및 초기화
            currentBGM.Stop(); // 정지
            currentBGM = null; // 초기화
        }

        if (!BGMDic.ContainsKey(_bgm)) // 해당 배경음 없음
        {
            // 오류 로그 출력 : 잘못된 배경음 이름
            Logger.LogError($"Invalid Clip Name : {_bgm}");
            return; // 종료
        }

        // 현재 배경음 설정 및 재생
        currentBGM = BGMDic[_bgm]; // 설정
        currentBGM.Play(); // 재생
    }

    // 배경음 일시중지 함수
    public void PauseBGM() => currentBGM?.Pause(); // 현재 배경음 일시정지

    // 배경음 재개 함수
    public void ResumeBGM() => currentBGM?.UnPause(); // 현재 배경음 일시정지 해제

    // 배경음 정지 함수
    public void StopBGM() => currentBGM?.Stop(); // 현재 배경음 정지

    // 효과음 재생 함수
    public void PlaySFX(SFX _sfx)
    {
        if (!SFXDic.ContainsKey(_sfx)) // 해당 효과음 없음
        {
            // 오류 로그 출력 : 잘못된 효과음 이름
            Logger.LogError($"Invalid Clip Name : {_sfx}");
            return; // 종료
        }

        SFXDic[_sfx].Play(); // 효과음 재생
    }

    // 음소거 함수
    public void Mute()
    {
        // 배경음 및 효과음 음소거
        foreach (var _bgm in BGMDic) _bgm.Value.volume = 0;
        foreach (var _sfx in SFXDic) _sfx.Value.volume = 0;
    }

    // 음소거 해제 함수
    public void UnMute()
    {
        // 배경음 및 효과음 음소거 해제
        foreach (var _bgm in BGMDic) _bgm.Value.volume = 1;
        foreach (var _sfx in SFXDic) _sfx.Value.volume = 1;
    }

    // 유저 데이터 불러오기 함수
    public void OnLoadUserData()
    {
        // 유저 설정 데이터 가져오기
        var userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        if (userSettingsData != null) // 유저 설정 데이터 있음
        {
            if (!userSettingsData.Sound) // 유저 설정 사운드 ON
            {
                Mute(); // 음소거
            }
        }
    }
}