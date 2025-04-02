using UnityEngine;

public class Homing : MonoBehaviour
{
    // 타겟 = 플레이어
    public GameObject target;
    // 속도
    public float speed = 3f;
    // 방향
    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {
        // 태그로 플레이어 찾기
        target = GameObject.FindGameObjectWithTag("Player");
        // A - B 벡터 : B에서 A를 바라보는 벡터
        dir = target.transform.position - transform.position;
        // 정규화 -> 방향벡터
        dirNo = dir.normalized;
        // Start() : 처음 시작 시에만 추적
        // Update() : 계속 추적
    }

    void Update()
    {
        // 이동
        transform.Translate(dirNo * speed * Time.deltaTime);

        //// 위와 같은 기능을 하는 함수
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    // 화면 밖으로 나감
    private void OnBecameInvisible()
    {
        // 제거
        Destroy(gameObject);
    }

    // 플레이어와 충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어 제거

            // 제거
            Destroy(gameObject);
        }
    }
}
