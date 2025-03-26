using UnityEngine;

public class Slash : MonoBehaviour
{
    float angle; // ����
    //public Vector3 direction = Vector3.right; // ����
    Vector3 direction; // ����
    Vector2 MousePos; // ���콺 ��ġ

    private GameObject player; // �÷��̾�

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Transform tr = player.GetComponent<Transform>();
        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        Vector3 Pos = new Vector3(MousePos.x, MousePos.y, 0);

        direction = Pos - tr.position; // �÷��̾�� ���콺�� ���ϴ� ����

        // ���� ���
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        transform.position = player.transform.position; // ������ ��ġ
        transform.rotation = Quaternion.Euler(0f, 0f, angle); // ������ ȸ��
    }

    public void Destr() // ����Ʈ ���� �Լ�
    {
        Destroy(gameObject);
    }
}