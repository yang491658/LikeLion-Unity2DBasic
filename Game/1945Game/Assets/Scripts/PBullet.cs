using UnityEngine;

public class PBullet : MonoBehaviour
{
    // 속도
    public float speed = 5f;
    // 공격력
    public int attack = 10;
    // 이펙트
    public GameObject effect;

    void Update()
    {
        // 총알 발사 : 방향 * 속도 * 시간
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    // 화면 밖으로 나감
    private void OnBecameInvisible()
    {
        // 제거
        Destroy(gameObject);
    }

    // 충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            // 이펙트 생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            // 1초 뒤 이펙트 제거
            Destroy(go, 1);

            // 몬스터 제거
            //Destroy(collision.gameObject);
            // 충돌 객체에서 컴포넌트를 가져와서 몬스터 클래스의 함수 사용
            collision.gameObject.GetComponent<Monster>().Damage(attack);

            // 제거
            Destroy(gameObject);
        }

        if (collision.CompareTag("Boss"))
        {
            // 이펙트 생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            // 1초 뒤 이펙트 제거
            Destroy(go, 1);

            // 제거
            Destroy(gameObject);
        }
    }
}
