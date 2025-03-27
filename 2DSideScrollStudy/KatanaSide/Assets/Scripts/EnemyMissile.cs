using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float speed = 5f; // �ӵ�
    public float lifeTime = 3f; // ���� �ð�
    public int damage = 10; // ������
    public Vector2 direction; // ����
    
    void Start()
    {
        // ���� �ð� �� �̻��� ����
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        // �̻��� �̵�
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // �÷��̾� �浹
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �̻��� ����
            Destroy(gameObject);
        }
    }
}