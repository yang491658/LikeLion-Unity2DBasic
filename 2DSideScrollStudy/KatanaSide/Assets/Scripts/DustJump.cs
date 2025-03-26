using UnityEngine;

public class DustJump : MonoBehaviour
{
    public float lifeTime = 0.5f;

    private void Awake()
    {
        // 먼지 제거
        Destroy(gameObject, lifeTime);
    }
}