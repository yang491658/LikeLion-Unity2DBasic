using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("플레이어 속성")]
    public float speed = 5; // 속도
    public float jump = 8; // 점프력
    public float power = 5; // 파워
    public Vector3 direction; // 방향

    public GameObject slash; // 슬래쉬
    public GameObject shadow; // 그림자
    List<GameObject> shadowList = new List<GameObject>(); // 그림자 배열
    public GameObject lazer; // 레이저
    public GameObject dustRun; // 달리기 먼지
    public GameObject dustJump; // 점프 먼지
    public GameObject dustWall; // 벽점프 먼지

    // 벽잡기
    bool isWall; // 벽 유무
    public Transform wallCheck; // 벽 위치
    public float wallDistance = 0.5f; // 벽과의 거리
    public LayerMask wallLayer; // 벽 레이어

    // 벽점프
    public bool wallJumping; // 벽 점프 상태
    public float slidingSpeed = 0.8f; // 낙하 속도
    float isRight = 1; // 벽 잡기 방향

    Animator ani; // 애니메이터
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
        // 시간 조절 입력 체크 ( Shift키 입력 시 슬로우 모션 시작)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // 포스트 프로세싱 화면 효과
            TimeControler.Instance.SetSlowMotion(true);
        }

        if (!isWall && !wallJumping)
        {
            InputKey(); // 키 입력 함수 실행
            Move();// 이동 함수 실행
        }

        // 벽 유무 확인
        isWall = Physics2D.Raycast(wallCheck.position, Vector2.right * isRight, wallDistance, wallLayer);
        ani.SetBool("Grab", isWall);

        if (Input.GetKeyDown(KeyCode.W)) // W키 입력
        {
            if (ani.GetBool("Jump") == false)
            {
                Jump(); // 점프 함수 실행
                ani.SetBool("Jump", true);
            }
        }

        if (isWall)
        {
            // 플레이어 벽잡기
            wallJumping = false;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * slidingSpeed);

            // 벽점프
            if (Input.GetKeyDown(KeyCode.W)) // W키 입력
            {
                wallJumping = true;

                // 잡기 함수 실행
                Invoke("Grab", 0.3f);

                // 플레이어 벽점프
                rb.linearVelocity = new Vector2(-isRight * jump, 0.9f * jump);

                DustJump(); // 점프 먼지 함수 실행

                sr.flipX = !sr.flipX;
                isRight = -isRight;
            }
        }
    }

    void InputKey() // 방향키 입력 함수
    {
        direction.x = Input.GetAxisRaw("Horizontal"); // A/S키 또는 좌우 방향키 입력

        // 플레이어 모습 변경
        if (direction.x < 0) // 왼쪽 방향키
        {
            sr.flipX = true; // 플레이어 방향 전환
            ani.SetBool("Run", true);

            for (int i = 0; i < shadowList.Count; i++)
            {
                shadowList[i].GetComponent<SpriteRenderer>().flipX = sr.flipX; // 그림자 방향 전환
            }
        }
        else if (direction.x > 0) // 오른쪽 방향키
        {
            sr.flipX = false; // 플레이어 방향 전환
            ani.SetBool("Run", true);

            for (int i = 0; i < shadowList.Count; i++)
            {
                shadowList[i].GetComponent<SpriteRenderer>().flipX = sr.flipX; // 그림자 방향 전환
            }
        }
        else if (direction.x == 0) // 정지 (입력 없음)
        {
            ani.SetBool("Run", false);

            // 그림자 제거
            for (int i = 0; i < shadowList.Count; i++)
            {
                Destroy(shadowList[i]); // 그림자 제거 
                shadowList.RemoveAt(i); // 리스트 내에서 그림자 제거
            }
        }

        if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭
        {
            ani.SetTrigger("Attack");

            // 레이저 생성
            Instantiate(lazer, transform.position, Quaternion.identity);
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

        DustJump(); // 점프 먼지 함수 실행
    }

    void Grab() // 잡기 함수
    {
        wallJumping = false;
    }

    private const float checkDistance = 0.7f; // 점프 가능 거리

    private void FixedUpdate()
    {
        Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit
            = Physics2D.Raycast(rb.position, Vector3.down, checkDistance, LayerMask.GetMask("Ground"));

        CheckGround(rayHit);
    }

    void CheckGround(RaycastHit2D rayHit)
    {
        bool isGround = rayHit.collider != null && rayHit.distance < checkDistance;

        if (isGround) // 오브젝트의 콜라이더 접촉
        {
            ani.SetBool("Jump", false);
        }
        else
        {
            if (isWall) // 벽 잡기 상태
            {
                ani.SetBool("Grab", true);

            }
            else
            {
                ani.SetBool("Jump", true);
            }
        }
    }

    public void Slash() // 슬래쉬 함수
    {
        //// 플레이어 슬래쉬 생성
        //Instantiate(slash, transform.position, Quaternion.identity);

        // 방향에 따른 플레이어 슬래쉬(공격 이펙트) 생성
        if (sr.flipX == false) // 플레이어 방향 오른쪽
        {
            rb.AddForce(Vector2.right * power, ForceMode2D.Impulse); // 플레이어 이동
            GameObject go = Instantiate(slash, transform.position, Quaternion.identity); // 슬래쉬 생성
            //go.GetComponent<SpriteRenderer>().flipX = sr.flipX; // 슬래쉬 방향 전환

        }
        else // 플레이어 방향 왼쪽
        {
            rb.AddForce(Vector2.left * power, ForceMode2D.Impulse); // 플레이어 이동
            GameObject go = Instantiate(slash, transform.position, Quaternion.identity); // 슬래쉬 생성
            //go.GetComponent<SpriteRenderer>().flipX = sr.flipX; // 슬래쉬 방향 전환
        }
    }

    public void Shadow() // 그림자 함수
    {
        if (shadowList.Count < 6)
        {
            GameObject go = Instantiate(shadow, transform.position, Quaternion.identity); // 그림자 생성
            go.GetComponent<Shadow>().speed = 10 - shadowList.Count; // 그림자 속도 감소
            shadowList.Add(go);
        }
    }

    public void DustRun(GameObject dustRun) // 달리기 먼지 함수
    {
        // 달릭기 먼지 생성
        Instantiate(dustRun, transform.position + new Vector3(0f, -0.35f, 0f), Quaternion.identity);
    }

    public void DustJump() // 점프 먼지 함수
    {
        if (!isWall)
        {
            // 점프 먼지 생성
            Instantiate(dustJump, transform.position, Quaternion.identity);
        }
        else
        {
            // 벽점프 먼지 생성
            GameObject go = Instantiate(dustWall, transform.position + new Vector3(0.8f * isRight, 0, 0), Quaternion.identity);
            go.GetComponent<SpriteRenderer>().flipX = sr.flipX; // 벅점프 먼지 방환 전환
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 보스 포탈과 충돌
        if (collision.CompareTag("BossScene"))
        {
            // 보스 씬으로 전환
            SceneManager.LoadScene("Boss");
        }
    }
}