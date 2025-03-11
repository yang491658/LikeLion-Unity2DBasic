using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 싱글톤
    public static SoundManager instance; // 자기자신을 변수로 담기

    AudioSource myAudio; // 오디오소스 컴포넌트를 변수 저장

    public AudioClip soundShoot; // 발사 사운드
    public AudioClip soundKill; // 처치 사운드

    private void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }

    void Start()
    {
        myAudio = GetComponent<AudioSource>(); // 오디오소스 컴포넌트 가져오기
    }

    public void PlayShootSound()
    {
        myAudio.PlayOneShot(soundShoot);
    }

    public void PlayKillSound()

    {
        myAudio.PlayOneShot(soundKill);
    }

    void Update()
    {

    }
}
