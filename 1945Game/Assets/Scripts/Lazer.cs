using UnityEngine;

public class Lazer : MonoBehaviour
{
    public GameObject effect;
    Transform pos;
    int attack = 10;

    void Start()
    {
        // �÷��̾� ��ġ ��������
        pos = GameObject.Find("Player").GetComponent<Player>().pos;
    }

    void Update()
    {
        transform.position = pos.position;
    }

    // �浹 ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            // ���� ���� : �浹 ��ü���� ������Ʈ�� �����ͼ� ���� Ŭ������ �Լ� ���
            collision.gameObject.GetComponent<Monster>().Damage(attack++);
            // ����Ʈ ���� �� ����
            CreateEffect(collision.transform.position);
        }

        if (collision.CompareTag("Boss"))
        {
            // ����Ʈ ���� �� ����
            CreateEffect(collision.transform.position);
        }
    }

    // �浹 ����
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            // ���� ���� : �浹 ��ü���� ������Ʈ�� �����ͼ� ���� Ŭ������ �Լ� ���
            collision.gameObject.GetComponent<Monster>().Damage(attack++);
            // ����Ʈ ���� �� ����
            CreateEffect(collision.transform.position);
        }

        if (collision.CompareTag("Boss"))
        {
            // ����Ʈ ���� �� ����
            CreateEffect(collision.transform.position);
        }
    }

    // ����Ʈ ���� �Լ�
    void CreateEffect(Vector3 position)
    {
        GameObject go = Instantiate(effect, position, Quaternion.identity);
        //GameObject go = Instantiate(effect, new Vector2(transform.position.x, position.y), Quaternion.identity);
        Destroy(go, 1);
    }
}
