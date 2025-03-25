using UnityEngine;

public class Slash : MonoBehaviour
{
    float angle; // ����

    private GameObject player; // �÷��̾�

    public Vector3 direction = Vector3.right; // ����(�̵�)
    Vector3 dirRotation; // ����(ȸ��)
    Vector2 MousePos; // ���콺 ��ġ

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Transform tr = player.GetComponent<Transform>();
        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        Vector3 Pos = new Vector3(MousePos.x, MousePos.y, 0);

        dirRotation = Pos - tr.position; // �÷��̾�� ���콺�� ���ϴ� ����

        // ���� ���
        angle = Mathf.Atan2(dirRotation.y, dirRotation.x) * Mathf.Rad2Deg;
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