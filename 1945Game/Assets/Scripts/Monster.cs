using UnityEngine;

public class Monster : MonoBehaviour
{
    // 체력
    public int hp = 100;
    // 속도
    public float speed = 1f;
    // 딜레이
    public float delay = 1f;

    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;
    public GameObject item;

    void Start()
    {
        // 함수 1회 호출
        Invoke("Shoot", delay);
    }

    void Shoot()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);

        // 반복 : 재귀 호출
        Invoke("Shoot", delay);
    }

    void Update()
    {
        // 이동
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    // 화면 밖으로 나감
    private void OnBecameInvisible()
    {
        // 제거
        Destroy(gameObject);
        //PoolManager.Instance.Return(gameObject); // 오브젝트 풀링
    }

    // 총알에 따른 데미지 함수
    public void Damage(int ATTACK)
    {
        hp -= ATTACK;

        if (hp <= 0)
        {
            ItemDrop();
            Destroy(gameObject);
            //PoolManager.Instance.Return(gameObject); // 오브젝트 풀링
        }
    }

    // 처치 시 아이템 생성
    public void ItemDrop()
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }
}
