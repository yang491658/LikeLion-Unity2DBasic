using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // �̱���
    public static SoundManager instance; // �ڱ��ڽ��� ������ ���

    AudioSource myAudio; // ������ҽ� ������Ʈ�� ���� ����

    public AudioClip soundShoot; // �߻� ����
    public AudioClip soundKill; // óġ ����

    private void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }

    void Start()
    {
        myAudio = GetComponent<AudioSource>(); // ������ҽ� ������Ʈ ��������
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
