using UnityEngine;

public class Homing : MonoBehaviour
{
    // Ÿ�� = �÷��̾�
    public GameObject target;
    // �ӵ�
    public float speed = 3f;
    // ����
    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {
        // �±׷� �÷��̾� ã��
        target = GameObject.FindGameObjectWithTag("Player");
        // A - B ���� : B���� A�� �ٶ󺸴� ����
        dir = target.transform.position - transform.position;
        // ����ȭ -> ���⺤��
        dirNo = dir.normalized;
        // Start() : ó�� ���� �ÿ��� ����
        // Update() : ��� ����
    }

    void Update()
    {
        // �̵�
        transform.Translate(dirNo * speed * Time.deltaTime);

        //// ���� ���� ����� �ϴ� �Լ�
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
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
