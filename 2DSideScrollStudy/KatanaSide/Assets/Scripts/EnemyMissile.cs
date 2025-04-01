using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float speed = 5f; // 속도
    public float lifeTime = 3f; // 생존 시간
    public int damage = 10; // 데미지
    //public Vector2 direction; // 방향
    private Vector2 direction; // 방향

    void Start()
    {
        // 일정 시간 후 미사일 제거
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    void Update()
    {
        // 슬로우 모션 적용
        float timeScale = TimeControler.Instance.GetTimeScale();

        // 미사일 이동
        transform.Translate(direction * speed * Time.deltaTime * timeScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 충돌
        if (collision.CompareTag("Player"))
        {
            // 미사일 제거
            Destroy(gameObject);
        }
        // 적 충돌 (적 처치)
        else if (collision.CompareTag("Enemy"))
        {
            // 적 처치 시 사망 애니메이션 재생
            ShootingEnemy enemy = collision.GetComponent<ShootingEnemy>();
            if (enemy != null)
            {
                enemy.Death();
            }

            // 미사일 제거
            Destroy(gameObject);
        }
    }
}