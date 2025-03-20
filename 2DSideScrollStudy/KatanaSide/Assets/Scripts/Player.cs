using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5; // 속려
    public float jump = 5; // 점프력
    public Vector3 direction;

    public GameObject slash; // 슬래쉬(공격)

    Animator ani; // 애니메티어
    Rigidbody2D rb; // 리지드바디
    SpriteRenderer sr; // 스프라이트 렌더러

    void Start()
    {
        direction = Vector2.zero;

        // 각각의 컴포넌트
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        InputKey();
        Move();

        if (Input.GetKeyDown(KeyCode.W)) // W키 입력
        {
            if (ani.GetBool("Jump") == false)
            {
                Jump();
                ani.SetBool("Jump", true);
            }
        }

        if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭
        {
            ani.SetTrigger("Attack");
        }
    }

    void InputKey() // 키 입력 함수
    {
        direction.x = Input.GetAxisRaw("Horizontal"); // A/S키 또는 좌우 방향키 입력

        // 플레이어 모습 변경
        if (direction.x < 0) // 왼쪽
        {
            sr.flipX = true;
            ani.SetBool("Run", true);
        }
        else if (direction.x > 0) // 오른쪽
        {
            sr.flipX = false;
            ani.SetBool("Run", true);
        }
        else if (direction.x == 0)
        {
            ani.SetBool("Run", false);
        }
    }

    public void Move() // 이동 함수
    {
        // 플레이이 이동
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Jump() // 점프 함수
    {
        // 플레이이 점프
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
    }

    public void Slash() // 슬래시 함수
    {
        // 플레이어 슬래쉬(공격 이펙트) 생성
        Instantiate(slash, transform.position, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit
            = Physics2D.Raycast(rb.position, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (rb.linearVelocityY < 0) // 하강 중
        {
            if (rayHit.collider != null) // 오브젝트의 콜라이더 접촉
            {
                if (rayHit.distance < 0.7f) // 거리 측정
                {
                    ani.SetBool("Jump", false);
                }
            }
        }
    }
}