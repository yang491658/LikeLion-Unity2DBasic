using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // 속도
    public float speed = 3f;
    //// 화면 경계
    //private Vector2 minBounds; // 최소
    //private Vector2 maxBounds; // 최대

    // 애니메이터를 가져올 변수
    Animator ani;

    // 총알
    //public GameObject bullet;
    public GameObject[] bullet; // 배열로 수정
    public Transform pos = null;
    public int power = 0;

    [SerializeField]
    private GameObject powerUp; // private를 인스펙터에서 사용하는 방법

    // 레이저
    public GameObject lazer;
    public float gValue = 0;

    // 이미지 UI
    public Image gage; 

    void Start()
    {
        //// 화면 경계 충돌 로직
        //// 카메라 변수
        //Camera cam = Camera.main;
        //// 화면 기준 좌표를 월드 기준으로 변환
        //Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)); // 왼쪽 아래 모서리
        //Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0)); // 오른쪽 위 모서리
        //minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        //maxBounds = new Vector2(topRight.x, topRight.y);

        // 애니메이터 컴포넌트 가져오기
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        // 방향키에 따른 움직임
        float moveX = speed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = speed * Time.deltaTime * Input.GetAxis("Vertical");

        // 움직임에 따른 값 변경
        if (Input.GetAxis("Horizontal") <= -0.5f)
            ani.SetBool("Left", true);
        else
            ani.SetBool("Left", false);

        if (Input.GetAxis("Horizontal") >= 0.5f)
            ani.SetBool("Right", true);
        else
            ani.SetBool("Right", false);

        if (Input.GetAxis("Vertical") >= 0.5f)
            ani.SetBool("Up", true);
        else
            ani.SetBool("Up", false);

        // 스페이스바 -> 총알 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(bullet, pos.position, Quaternion.identity);
            Instantiate(bullet[power], pos.position, Quaternion.identity); // 배열로 수정
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            gValue += Time.deltaTime;
            gage.fillAmount = gValue;

            if (gValue >= 1)
            {
                GameObject go = Instantiate(lazer, pos.position, Quaternion.identity);
                Destroy(go, 1);
                gValue = 0;
            }
        }
        else
        {
            gValue -= Time.deltaTime;
            if (gValue <= 0)
            {
                gValue = 0;
            }

            gage.fillAmount = gValue;
        }

        transform.Translate(moveX, moveY, 0);

        //// 화면 경계 충돌 로직
        //// 새로운 위치 = 현재 위치 + 이동량
        //Vector3 newPosition = transform.position + new Vector3(moveX, moveY, 0);
        //// 경계를 벗어나지 않도록 위치 제한
        //newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        //newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
        //// 새로운 위치 업데이트
        //transform.position = newPosition;

        // 화면 경계 충돌
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); // 캐릭터 월드 좌표 -> 뷰토프 좌표계 변환
        viewPos.x = Mathf.Clamp01(viewPos.x); // x값을 0 이상, 1 이하로 제한
        viewPos.y = Mathf.Clamp01(viewPos.y); // y값을 0 이상, 1 이하로 제한
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); // 다시 월드 좌표 변환
        transform.position = worldPos; // 좌표 적용
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            power += 1;

            if (power >= 3)
            {
                power = 3;
            }
            else
            {
                // 파워업 시 UI 출력, 1초 후 제거
                //Destroy(Instantiate(powerUp, Vector3.zero, Quaternion.identity), 1); // 화면 중앙
                Destroy(Instantiate(powerUp, transform.position, Quaternion.identity), 1); // 화면 중앙
            }

            // 아이템 획득 처리
            Destroy(collision.gameObject);
        }
    }

}
