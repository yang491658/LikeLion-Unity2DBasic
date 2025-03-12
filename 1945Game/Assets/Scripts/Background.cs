using UnityEngine;

public class Background : MonoBehaviour
{
    // 속도
    public float spped = 0.01f;
    Material material;

    void Start()
    {
        // 머터리얼 가져옴
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // 오프셋 적용
        float offsetY = material.mainTextureOffset.y + spped * Time.deltaTime;
        Vector2 offset = new Vector2(0, offsetY);
        material.mainTextureOffset = offset;
    }
}
