using UnityEngine;

public class Monster : MonoBehaviour
{
    // 속도
    public float speed = 1f;
    // 딜레이
    public float delay = 1f;

    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;

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
    }
}
