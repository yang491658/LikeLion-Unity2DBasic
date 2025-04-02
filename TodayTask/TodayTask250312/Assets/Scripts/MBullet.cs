using UnityEngine;

public class MBullet : MonoBehaviour
{
    // 속도
    public float speed = 4f;

    void Start()
    {

    }

    void Update()
    {
        // 총알 발사 : 방향 * 속도 * 시간
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    // 화면 밖으로 나감
    private void OnBecameInvisible()
    {
        // 제거
        Destroy(gameObject);
    }

    // 플레이어와 충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어 제거

            // 제거
            Destroy(gameObject);
        }
    }
}
