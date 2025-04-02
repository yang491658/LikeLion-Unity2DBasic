using UnityEngine;

public class Lazer : MonoBehaviour
{
    public GameObject effect;
    Transform pos;
    int attack = 10;

    void Start()
    {
        // 플레이어 위치 가져오기
        pos = GameObject.Find("Player").GetComponent<Player>().pos;
    }

    void Update()
    {
        transform.position = pos.position;
    }

    // 충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            // 몬스터 제거 : 충돌 객체에서 컴포넌트를 가져와서 몬스터 클래스의 함수 사용
            collision.gameObject.GetComponent<Monster>().Damage(attack++);
            // 이펙트 생성 후 제거
            CreateEffect(collision.transform.position);
        }

        if (collision.CompareTag("Boss"))
        {
            // 이펙트 생성 후 제거
            CreateEffect(collision.transform.position);
        }
    }

    // 충돌 지속
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            // 몬스터 제거 : 충돌 객체에서 컴포넌트를 가져와서 몬스터 클래스의 함수 사용
            collision.gameObject.GetComponent<Monster>().Damage(attack++);
            // 이펙트 생성 후 제거
            CreateEffect(collision.transform.position);
        }

        if (collision.CompareTag("Boss"))
        {
            // 이펙트 생성 후 제거
            CreateEffect(collision.transform.position);
        }
    }

    // 이펙트 생성 함수
    void CreateEffect(Vector3 position)
    {
        GameObject go = Instantiate(effect, position, Quaternion.identity);
        //GameObject go = Instantiate(effect, new Vector2(transform.position.x, position.y), Quaternion.identity);
        Destroy(go, 1);
    }
}
