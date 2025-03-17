using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    // ��ũ�� �ӵ�
    public float scrollSpeed = 0.4f;
    // ������ ���͸��� �����͸� �޾ƿ� ��ü ����
    private Material thisMaterial;

    void Start()
    {
        // ��ü�� ������ ��, ���� 1ȸ ȣ��Ǵ� �Լ�
        // ���� ��ü�� Component���� ������ Renderer��� ������Ʈ�� Material ������ �޾ƿ�
        thisMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // ���Ӱ� �������� Offest ��ü ����
        Vector2 newOffset = thisMaterial.mainTextureOffset;
        // y �� = ���� y ��ǥ + �ӵ� * ������ ����
        newOffset.Set(0, newOffset.y + (scrollSpeed * Time.deltaTime));
        // ���������� ������ �� ����
        thisMaterial.mainTextureOffset = newOffset;
    }
}
