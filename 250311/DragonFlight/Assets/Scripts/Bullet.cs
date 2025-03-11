using UnityEngine;

public class Bullet : MonoBehaviour
{
    // �ӵ� ����
    public float speed = 2.0f;
    // ������ ������ ����
    public GameObject explosion;

    void Start()
    {
        //Singleton.instance.PrintMessage();
    }

    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    // ȭ�� ������ ���� ��� (ī�޶� ������ ������) ȣ��
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // ��ü ����
    }

    // 2D �浹 Ʈ���� �̺�Ʈ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �̻��ϰ� �� �浹
        //if (collision.gameObject.tag == "Enemy")
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // ���� ����Ʈ ����
            Instantiate(explosion, transform.position, Quaternion.identity);

            // �� óġ ����
            SoundManager.instance.PlayEnemySound();

            // ���� ���
            GameManager.instance.AddScore(10);

            // �� �����
            Destroy(collision.gameObject);
            // �Ѿ� ����� (�ڱ��ڽ�)
            Destroy(gameObject);
        }
    }
}
