using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �ӵ�
    public float speed = 2.0f;

    void Start()
    {

    }

    void Update()
    {
        // �̵� �Ÿ� = �ӵ� * �ð�(������ ����)
        float distanceY = speed * Time.deltaTime;
        // �Ʒ� �̵�
        transform.Translate(0, -distanceY, 0);
    }

    // ȭ���� ����� ���� ~ ȭ�� : Game + Scene
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // ��ü ����
    }
}
