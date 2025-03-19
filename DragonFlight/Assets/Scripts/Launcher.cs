using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject bullet; // �̻��� ������ ������ ����

    void Start()
    {
        // InvokeRepeating (�Լ��̸�, �ʱ������ð�, ������ �ð�)
        InvokeRepeating("Shoot", 0.3f, 0.3f);
    }

    void Shoot()
    {
        // Instantiate(�̻��� ������, ����������, �������)
        Instantiate(bullet, transform.position, Quaternion.identity);

        // ���� ��� : ���� �Ŵ������� �Ѿ� ����
        SoundManager.instance.PlayBulletSound();
    }

    void Update()
    {

    }
}
