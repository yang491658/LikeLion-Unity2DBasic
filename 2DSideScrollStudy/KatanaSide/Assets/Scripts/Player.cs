using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("플레이어 속성")]
    public float speed = 5; // 속도
    public float jump = 5; // 점프력
    public float power = 5; // 파워
    public Vector3 direction; // 방향

    // 슬래쉬
    public GameObject slash;

    // 그림자
    public GameObject shadow;
    List<GameObject> shadowList = new List<GameObject>();

    // 레이저
    public GameObject lazer;

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
        InputKey();
        Move();
    }

    void InputKey() // 키 입력 함수
    {
        direction.x = Input.GetAxisRaw("Horizontal"); // A/S키 또는 좌우 방향키 입력

        // 플레이어 모습 변경
        if (direction.x < 0) // 왼쪽 방향키
        {
            sr.flipX = true;
            ani.SetBool("Run", true);

            // 그림자 방향 변경
            for (int i = 0; i < shadowList.Count; i++)
            {
                shadowList[i].GetComponent<SpriteRenderer>().flipX = sr.flipX;
            }
        }
        else if (direction.x > 0) // 오른쪽 방향키
        {
            sr.flipX = false;
            ani.SetBool("Run", true);

            // 그림자 방향 변경
            for (int i = 0; i < shadowList.Count; i++)
            {
                shadowList[i].GetComponent<SpriteRenderer>().flipX = sr.flipX;
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

    public void Slash() // 슬래쉬 함수
    {
        // 방향에 따른 플레이어 슬래쉬(공격 이펙트) 생성
        if (sr.flipX == false) // 플레이어 방향 오른쪽
        {
            rb.AddForce(Vector2.right * power, ForceMode2D.Impulse); // 플레이어 이동
            GameObject go = Instantiate(slash, transform.position, Quaternion.identity); // 슬래쉬 생성
            //go.GetComponent<SpriteRenderer>().flipX = sr.flipX; // 슬래쉬 방향 오른쪽

        }
        else // 플레이어 방향 왼쪽
        {
            rb.AddForce(Vector2.left * power, ForceMode2D.Impulse); // 플레이어 이동
            GameObject go = Instantiate(slash, transform.position, Quaternion.identity); // 슬래쉬 생성
            //go.GetComponent<SpriteRenderer>().flipX = sr.flipX; // 슬래쉬 방향 왼쪽
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
}