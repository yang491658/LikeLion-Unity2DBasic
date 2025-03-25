using UnityEngine;

public class Lazer : MonoBehaviour
{
    float speed = 50f; // 속도
    float angle; // 각도

    Transform pTr; // 플레이어의 트랜스폼

    Vector2 MousePos; // 마우스 위치

    // 방향
    Vector3 dir;
    Vector3 dirNo;

    void Start()
    {
        pTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        Vector3 Pos = new Vector3(MousePos.x, MousePos.y, 0);

        dir = Pos - pTr.position; // 플레이어에서 마우스를 향하는 벡터
        dirNo = new Vector3(dir.x, dir.y, 0).normalized; // 단위 벡터

        // 각도 계산
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 회전 적용
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // 레이저 제거
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        // 레이저 이동
        //transform.position += Vector3.right * speed * Time.deltaTime;
        transform.position += dirNo * speed * Time.deltaTime;
    }
}