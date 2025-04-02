using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb; // 리지드바디
    private Animator ani; // 애니메이터

    private float xInput;

    [SerializeField] private float speed; // 속도
    [SerializeField] private float jump; // 점프력

    [SerializeField] private bool move; // 이동 여부

    private int dircetion = 1; // 방향
    private bool isRight = true;

    [Header("Collision Info")]
    [SerializeField] private float distance;
    [SerializeField] private LayerMask ground;
    private bool isGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        InputKey(); // 키 입력
        Move(); // 플레이어 이동
        FlipControl(); // 플레이어 이동 중 방향 전환
        CollisionCheck(); // 충돌 체크
        AnimationControl(); // 플레이어 이동 모션
    }

    private void InputKey()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        // 플레이어 방향 전환
        if (Input.GetKeyDown(KeyCode.R)) Flip();

        // 플레이어 점프
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocityY);
    }

    private void Flip()
    {
        dircetion = dircetion * -1;
        isRight = !isRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipControl()
    {
        if (rb.linearVelocityX > 0 && !isRight)
        {
            Flip();
        }
        else if (rb.linearVelocityX < 0 && isRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - distance));
    }

    private void CollisionCheck()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, distance, ground);
    }

    private void AnimationControl()
    {
        move = rb.linearVelocity.x != 0;
        ani.SetBool("Move", move);
    }
}