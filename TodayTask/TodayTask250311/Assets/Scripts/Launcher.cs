using UnityEngine;

public class Launcher : MonoBehaviour
{
    // �������� ������ ���� : �Ѿ�
    public GameObject bullet;

    void Start()
    {
        // �ݺ� ���� �Լ� : InvokeRepeating(�Լ�, �ʱ� ����, �ݺ� ����);
        InvokeRepeating("Shoot", 0.5f, 0.5f);
    }

    void Shoot()
    {
        // ���� �Լ� : Instantiate(������ ������Ʈ, ���� ��ġ ����, ���Ⱚ) -> �Ѿ�, ���Ŀ� ����, ����
        Instantiate(bullet, transform.position, Quaternion.identity);

        // �߻� ���� �߰� (�̱���)
        SoundManager.instance.PlayShootSound();
    }

    void Update()
    {
        
    }
}
