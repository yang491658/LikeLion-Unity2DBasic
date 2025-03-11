using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 싱글톤
    public static SoundManager instance; // 자기 자신을 변수로 담기

    AudioSource myAudio; // AudioSource 컴포넌트를 변수로 담음

    // 재생할 소리
    public AudioClip soundBullet; // 미사일 발사
    public AudioClip soundEnemy; // 적 처치

    private void Awake()
    {
        if (SoundManager.instance == null) // 인스터스 유무 검사
        {
            SoundManager.instance = this; // 자기 자신을 저장
        }
    }

    void Start()
    {
        myAudio = GetComponent<AudioSource>(); // 오디오소스 컴포넌트 가져오기
    }

    public void PlayBulletSound() // 미사일 발사
    {
        myAudio.PlayOneShot(soundBullet);
    }

    public void PlayEnemySound() // 적 처치
    {
        myAudio.PlayOneShot(soundEnemy);
    }

    void Update()
    {
    }
}
