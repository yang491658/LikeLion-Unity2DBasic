using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // �̱���
    public static SoundManager instance; // �ڱ� �ڽ��� ������ ���

    AudioSource myAudio; // AudioSource ������Ʈ�� ������ ����

    // ����� �Ҹ�
    public AudioClip soundBullet; // �̻��� �߻�
    public AudioClip soundEnemy; // �� óġ

    private void Awake()
    {
        if (SoundManager.instance == null) // �ν��ͽ� ���� �˻�
        {
            SoundManager.instance = this; // �ڱ� �ڽ��� ����
        }
    }

    void Start()
    {
        myAudio = GetComponent<AudioSource>(); // ������ҽ� ������Ʈ ��������
    }

    public void PlayBulletSound() // �̻��� �߻�
    {
        myAudio.PlayOneShot(soundBullet);
    }

    public void PlayEnemySound() // �� óġ
    {
        myAudio.PlayOneShot(soundEnemy);
    }

    void Update()
    {
    }
}
