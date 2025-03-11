using UnityEngine;

public class Background : MonoBehaviour
{
    // ��� ��ũ�� �ӵ�
    public float speed = 0.5f;
    // ������ ��ϵ� ��Ƽ���� �����͸� ������ ����
    private Material material;

    void Start()
    {
        // ���� ��ü�� ������Ʈ ���� -> ������ ������Ʈ�� ���͸��� ����
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Offset ����
        Vector2 offset = material.mainTextureOffset;
        // ���� ������ + ���ǵ� * �ð�(������ ����)
        offset.Set(0, offset.y + (speed * Time.deltaTime));
        // ���� ������ �� ����
        material.mainTextureOffset = offset;
    }
}
