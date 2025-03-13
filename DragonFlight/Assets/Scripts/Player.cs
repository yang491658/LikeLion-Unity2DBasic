using UnityEngine;

public class Player : MonoBehaviour
{
    // �ӵ� ����
    public float speed = 5.0f;

    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // ������ Axis�� ���� Ű�� ������ �Ǵ�
        // �ӵ��� �������� �����Ͽ� �̵��� ����
        // �����̴� �Ÿ� = ���� * �ӵ� * �ð�(������ ����)
        float distanceX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        // �̵�����ŭ ���� �̵� �Լ�
        transform.Translate(distanceX, 0, 0);
    }
}
