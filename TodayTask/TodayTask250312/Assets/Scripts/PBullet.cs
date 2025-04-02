using UnityEngine;

public class PBullet : MonoBehaviour
{
    // 속도
    public float speed = 5f;
    // 공격력
    public static int power = 1;
    // 이펙트
    public GameObject effect;
    // 아이템
    public GameObject item;

    void Start()
    {

    }

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

    // 적과 충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            // 이펙트 생성
            GameObject go = Instantiate(effect, transform.position, Quaternion.identity);
            // 1초 뒤 이펙트 제거
            Destroy(go, 1);

            // 몬스터 제거
            Destroy(collision.gameObject);

            if (!Item.exist)
            {
                Instantiate(item, transform.position, Quaternion.identity);
                Item.exist = true;
            }

            // 제거
            Destroy(gameObject);
        }
    }
}
