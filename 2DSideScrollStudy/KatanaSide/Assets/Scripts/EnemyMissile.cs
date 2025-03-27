using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float speed = 5f; // 속도
    public float lifeTime = 3f; // 생존 시간
    public int damage = 10; // 데미지
    public Vector2 direction; // 방향
    
    void Start()
    {
        // 일정 시간 후 미사일 제거
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        // 미사일 이동
        transform.Translate(direction * speed * Time.deltaTime);
    }

    // 플레이어 충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 미사일 제거
            Destroy(gameObject);
        }
    }
}