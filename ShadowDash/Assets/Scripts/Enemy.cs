using UnityEngine;

public class Enemy : Entity
{
    bool attack;// 공격 여부

    [Header("이동 정보")]
    [SerializeField] private float speed; // 속도

    [Header("플레이어 정보")]
    [SerializeField] private float playerDistance; // 플레이어 충돌 감지 거리
    [SerializeField] private LayerMask player; // 플레이어 레이어
    private RaycastHit2D isPlayer; // 플레이어 여부


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        // 플레이어 감지 시 추적 및 공격
        if (isPlayer)
        {
            if (isPlayer.distance > 1)
            {
                rb.linearVelocity = new Vector2(dircetion * speed * 2f, rb.linearVelocity.y);

                Debug.Log("플레이어 감지");
                attack = false;
            }
            else
            {
                Debug.Log("공격 : " + isPlayer.collider.gameObject.name);

                attack = true;
            }
        }

        // 바닥이 없으면 방향 회전  + 벽이 있으면 방향 회전
        if (!isGround || isWall) Flip();

        Move(); // 적 이동
    }

    private void Move() // 이동
    {
        if (!attack)
            rb.linearVelocity = new Vector2(dircetion * speed, rb.linearVelocity.y);
    }

    protected override void OnDrawGizmos() // 기즈모 그리기
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position,
            new Vector3(
                transform.position.x + dircetion * playerDistance,
                transform.position.y
                )
            );
    }

    protected override void CollisionCheck() // 충돌 감지
    {
        base.CollisionCheck();

        isPlayer = Physics2D.Raycast(transform.position, Vector2.right, dircetion * playerDistance, player);
    }
}
