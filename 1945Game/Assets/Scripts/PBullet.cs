using UnityEngine;

public class PBullet : MonoBehaviour
{
    // �ӵ�
    public float speed = 5f;
    // ���ݷ�
    public int attack = 10;
    // ����Ʈ
    public GameObject effect;

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
            //Destroy(collision.gameObject);
            // �浹 ��ü���� ������Ʈ�� �����ͼ� ���� Ŭ������ �Լ� ���
            collision.gameObject.GetComponent<Monster>().Damage(attack);

            // ����
            Destroy(gameObject);
        }

        if (collision.CompareTag("Boss"))
        {
            // ����Ʈ ����
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            // 1�� �� ����Ʈ ����
            Destroy(go, 1);

            // ����
            Destroy(gameObject);
        }
    }
}
