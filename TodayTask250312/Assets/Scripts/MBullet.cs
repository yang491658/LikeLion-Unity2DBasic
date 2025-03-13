using UnityEngine;

public class MBullet : MonoBehaviour
{
    // �ӵ�
    public float speed = 4f;

    void Start()
    {

    }

    void Update()
    {
        // �Ѿ� �߻� : ���� * �ӵ� * �ð�
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    // ȭ�� ������ ����
    private void OnBecameInvisible()
    {
        // ����
        Destroy(gameObject);
    }

    // �÷��̾�� �浹
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾� ����

            // ����
            Destroy(gameObject);
        }
    }
}
