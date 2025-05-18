using UnityEngine;

public class MineralLight : MonoBehaviour
{
    public float speed = 5;  // �ӵ�

    private Vector3 initialScale; // �ʱ� ũ��
    public float scaleFactor = 0.2f;  // ũ�� ��ȭ��

    void Start()
    {
        initialScale = transform.localScale; // ũ�� �ʱ�ȭ
    }

    void Update()
    {
        float time = Time.time * speed; // �ð�
        float sinValue = Mathf.Sin(time); // ���� �Լ� ����
        float scaleChange = sinValue * scaleFactor; // ������ ��ȭ�� ���

        // ��� �࿡ ��ȭ ����
        transform.localScale = initialScale + new Vector3(scaleChange, scaleChange, scaleChange);
    }
}