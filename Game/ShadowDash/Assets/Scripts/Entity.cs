using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb; // 리지드바디
    protected Animator ani; // 애니메이터

    [Header("이동 정보")]
    protected int dircetion = 1; // 방향
    protected bool isRight = true;

    [Header("충돌 정보")]
    [SerializeField] protected Transform groundCheck; // 바닥 충돌 체크
    [SerializeField] protected float groundDistance; // 바닥 충돌 감지 거리
    [SerializeField] protected LayerMask ground; // 바닥 레이어
    protected bool isGround; // 바닥 여부

    [Space]
    [SerializeField] protected Transform wallCheck; // 벽 충돌 체크
    [SerializeField] protected float wallDistance; // 벽 충돌 감지 거리
    protected bool isWall; // 벽 여부

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        CollisionCheck(); // 충돌 체크
    }

    protected virtual void Flip() // 방향 전환
    {
        dircetion = dircetion * -1;
        isRight = !isRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos() // 기즈모 그리기
    {
        // 바닥
        Gizmos.DrawLine(groundCheck.position,
            new Vector3(
                groundCheck.position.x,
                groundCheck.position.y - groundDistance
                )
            );  

        // 벽
        Gizmos.DrawLine(wallCheck.position,
            new Vector3(
                wallCheck.position.x + dircetion * wallDistance,
                wallCheck.position.y
                )
            );
    }

    protected virtual void CollisionCheck() // 충돌 감지
    {
        // 바닥
        isGround = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, ground);

        // 벽
        isWall = Physics2D.Raycast(wallCheck.position, Vector2.right, wallDistance, ground);
    }
}