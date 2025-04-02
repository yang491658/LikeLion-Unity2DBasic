using UnityEngine;

public class Background : MonoBehaviour
{
    // �ӵ�
    public float spped = 0.01f;
    Material material;

    void Start()
    {
        // ���͸��� ������
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // ������ ����
        float offsetY = material.mainTextureOffset.y + spped * Time.deltaTime;
        Vector2 offset = new Vector2(0, offsetY);
        material.mainTextureOffset = offset;
    }
}
