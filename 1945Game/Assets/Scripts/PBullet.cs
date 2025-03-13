using UnityEngine;

public class PBullet : MonoBehaviour
{
    // �ӵ�
    public float speed = 5f;
    // ���ݷ�
    // ����Ʈ
    public GameObject effect;

    void Start()
    {

    }

    void Update()
    {
        // �Ѿ� �߻� : ���� * �ӵ� * �ð�
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    // ȭ�� ������ ����
    private void OnBecameInvisible()
    {
        // ����
        Destroy(gameObject);
    }

    // �浹 ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            // ����Ʈ ����
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            // 1�� �� ����Ʈ ����
            Destroy(go, 1);

            // ���� ����
            Destroy(collision.gameObject);

            // ����
            Destroy(gameObject);
        }
    }
}
