using UnityEngine;

public class Lazer : MonoBehaviour
{
    float speed = 50f; // �ӵ�
    float angle; // ����

    Transform pTr; // �÷��̾��� Ʈ������

    Vector2 MousePos; // ���콺 ��ġ

    // ����
    Vector3 dir;
    Vector3 dirNo;

    void Start()
    {
        pTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        Vector3 Pos = new Vector3(MousePos.x, MousePos.y, 0);

        dir = Pos - pTr.position; // �÷��̾�� ���콺�� ���ϴ� ����
        dirNo = new Vector3(dir.x, dir.y, 0).normalized; // ���� ����

        // ���� ���
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // ȸ�� ����
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // ������ ����
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        // ������ �̵�
        //transform.position += Vector3.right * speed * Time.deltaTime;
        transform.position += dirNo * speed * Time.deltaTime;
    }
}