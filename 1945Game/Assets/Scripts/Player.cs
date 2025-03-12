using UnityEngine;

public class Player : MonoBehaviour
{
    // �ӵ�
    public float speed = 5f;
    //// ȭ�� ���
    //private Vector2 minBounds; // �ּ�
    //private Vector2 maxBounds; // �ִ�

    // �ִϸ����͸� ������ ����
    Animator ani;

    void Start()
    {
        //// ī�޶�
        //Camera cam = Camera.main;

        //// ȭ�� ���� ��ǥ�� ���� �������� ��ȯ
        //Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)); // ���� �Ʒ� �𼭸�
        //Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0)); // ������ �� �𼭸�

        //minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        //maxBounds = new Vector2(topRight.x, topRight.y);

        // �ִϸ����� ������Ʈ ��������
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        // ����Ű�� ���� ������
        float moveX = speed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = speed * Time.deltaTime * Input.GetAxis("Vertical");

        // �����ӿ� ���� �� ����
        if (Input.GetAxis("Horizontal") <= -0.5f)
            ani.SetBool("Left", true);
        else
            ani.SetBool("Left", false);

        if (Input.GetAxis("Horizontal") >= 0.5f)
            ani.SetBool("Right", true);
        else
            ani.SetBool("Right", false);

        if (Input.GetAxis("Vertical") >= 0.5f)
            ani.SetBool("Up", true);
        else
            ani.SetBool("Up", false);

        transform.Translate(moveX, moveY, 0);

        //// ���ο� ��ġ = ���� ��ġ + �̵���
        //Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);

        //// ��踦 ����� �ʵ��� ��ġ ����
        //newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        //newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        //// ���ο� ��ġ ������Ʈ
        //transform.position = newPosition;
    }
}
