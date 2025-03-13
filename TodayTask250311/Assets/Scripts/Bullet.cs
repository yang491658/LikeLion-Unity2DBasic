using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �ӵ�
    public float speed = 2.5f;
    // �������� ������ ���� : ����
    public GameObject explosion;

    void Start()
    {
        //Singleton.instance.PrintMessage();
    }

    void Update()
    {
        // �̵� �Ÿ� = �ӵ� * �ð�(������ ����)
        float distanceY = speed * Time.deltaTime;
        // �Ʒ� �̵�
        transform.Translate(0, distanceY, 0);
    }

    // ȭ���� ����� ���� ~ ȭ�� : Game + Scene
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // ��ü ����
    }

    // 2D �浹 Ʈ���� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �浹
        //if (collision.gameObject.tag == "Enemy")
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // ���� ����
            Instantiate(explosion, transform.position, Quaternion.identity);

            // óġ ���� �߰� (�̱���)
            SoundManager.instance.PlayKillSound();

            // ���� ����
            GameManager.instance.AddScore(100);

            // �� ����
            Destroy(collision.gameObject);

            // �Ѿ�(�ڱ��ڽ�) ����
            Destroy(gameObject);
        }
    }
}
