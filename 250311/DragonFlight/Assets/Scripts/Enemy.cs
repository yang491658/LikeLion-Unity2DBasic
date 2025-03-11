using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �ӵ� ����
    public float speed = 2.0f;

    void Start()
    {

    }

    void Update()
    {
        // �����̴� �Ÿ� ���
        float distanceY = speed * Time.deltaTime;
        // ������ �ݿ�
        transform.Translate(0, -distanceY, 0);
    }

    // ȭ�� ������ ���� ��� (ī�޶� ������ ������) ȣ��
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // ��ü ����
    }
}
