using UnityEngine;

public class DustJump : MonoBehaviour
{
    public float lifeTime = 0.5f;

    private void Awake()
    {
        // ���� ����
        Destroy(gameObject, lifeTime);
    }
}