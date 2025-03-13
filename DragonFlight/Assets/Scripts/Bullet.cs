using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 속도 정의
    public float speed = 2.0f;
    // 프리팹 가져올 변수
    public GameObject explosion;

    void Start()
    {
        //Singleton.instance.PrintMessage();
    }

    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    // 화면 밖으로 나갈 경우 (카메라에 보이지 않으면) 호출
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // 객체 삭제
    }

    // 2D 충돌 트리거 이벤트
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 미사일과 적 충돌
        //if (collision.gameObject.tag == "Enemy")
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 폭발 이펙트 생성
            Instantiate(explosion, transform.position, Quaternion.identity);

            // 적 처치 사운드
            SoundManager.instance.PlayEnemySound();

            // 점수 상승
            GameManager.instance.AddScore(10);

            // 적 지우기
            Destroy(collision.gameObject);
            // 총알 지우기 (자기자신)
            Destroy(gameObject);
        }
    }
}
