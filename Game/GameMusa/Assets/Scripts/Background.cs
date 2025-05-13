using UnityEngine;

public class Background : MonoBehaviour
{
    private GameObject cam; // ī�޶�

    // �з����� ��� (0�� �������� ������, 1�� �������� ī�޶�� �Բ� �̵�)
    [SerializeField] private float parallaxEffect;

    private float xPosition; // ��ġ
    private float length; // ����

    void Start()
    {
        cam = Camera.main.gameObject; // ���� ī�޶�

        xPosition = transform.position.x; // ����̹����� x ��ġ
        length = GetComponent<SpriteRenderer>().bounds.size.x; // ����̹����� ���� ����
    }

    void Update()
    {
        float distanceToMove = cam.transform.position.x * parallaxEffect; // �̵� �Ÿ�

        // ��� �̵�
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect); // �̵� �Ÿ� (ī�޶� ����)

        // ���� ��� : ����� ���������� ���� �Ÿ� ����� ��� �̵�
        if (distanceMoved > xPosition + length)
            xPosition = xPosition + length;
        else if (distanceMoved < xPosition - length)
            xPosition = xPosition - length;
    }
}