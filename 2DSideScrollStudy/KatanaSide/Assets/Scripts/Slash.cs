using UnityEngine;

public class Slash : MonoBehaviour
{
    float angle; // 각도
    //public Vector3 direction = Vector3.right; // 방향
    Vector3 direction; // 방향
    Vector2 MousePos; // 마우스 위치

    private GameObject player; // 플레이어

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Transform tr = player.GetComponent<Transform>();
        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        Vector3 Pos = new Vector3(MousePos.x, MousePos.y, 0);

        direction = Pos - tr.position; // 플레이어에서 마우스를 향하는 벡터

        // 각도 계산
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        transform.position = player.transform.position; // 슬래쉬 위치
        transform.rotation = Quaternion.Euler(0f, 0f, angle); // 슬래쉬 회전
    }

    public void Destr() // 이펙트 제거 함수
    {
        Destroy(gameObject);
    }

    // 적의 미사일과 충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyMissile>() != null)
        {
            // 미사일 정보
            EnemyMissile missile = collision.gameObject.GetComponent<EnemyMissile>();
            SpriteRenderer missileSr = collision.gameObject.GetComponent<SpriteRenderer>();

            // 미사일 방향 전환
            Vector2 reverseDir = -missile.GetDirection();
            missile.SetDirection(reverseDir);

            // 미사일 모습 변경
            if (missileSr != null)
            {
                missileSr.flipX = !missileSr.flipX;
            }
        }
    }
}