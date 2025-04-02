using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 속도
    public float speed = 2.5f;
    // 프리팹을 저장할 변수 : 폭발
    public GameObject explosion;

    void Start()
    {
        //Singleton.instance.PrintMessage();
    }

    void Update()
    {
        // 이동 거리 = 속도 * 시간(프레임 판정)
        float distanceY = speed * Time.deltaTime;
        // 아래 이동
        transform.Translate(0, distanceY, 0);
    }

    // 화면을 벗어나면 제거 ~ 화면 : Game + Scene
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // 객체 삭제
    }

    // 2D 충돌 트리거 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌
        //if (collision.gameObject.tag == "Enemy")
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 폭발 생성
            Instantiate(explosion, transform.position, Quaternion.identity);

            // 처치 사운드 추가 (싱글톤)
            SoundManager.instance.PlayKillSound();

            // 점수 증가
            GameManager.instance.AddScore(100);

            // 적 제거
            Destroy(collision.gameObject);

            // 총알(자기자신) 제거
            Destroy(gameObject);
        }
    }
}
