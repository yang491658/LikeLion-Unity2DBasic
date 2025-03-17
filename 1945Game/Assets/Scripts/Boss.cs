using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    int flag = 1;
    int speed = 2;

    public GameObject mBullet;
    public GameObject bossBullet;
    public Transform pos1;
    public Transform pos2;


    void Start()
    {
        Invoke("Hide", 3); // 3초 뒤 텍스트 숨김
        StartCoroutine(Shoot());
        StartCoroutine(Fire());
    }

    // 텍스트 숨기기
    void Hide()
    {
        GameObject.Find("TextBossWarning").SetActive(false);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            // 총알 생성
            Instantiate(mBullet, pos1.position, Quaternion.identity);
            Instantiate(mBullet, pos2.position, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }
    }

    // 원방향으로 에너지 발사
    IEnumerator Fire()
    {
        // 공격 주기
        float attackRate = 3f;
        // 발사체 생성갯수
        int count = 30;
        // 발사체 사이의 각도
        float intervalAngle = 360 / count;
        // 가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)
        float weightAngle = 0;

        // 원 형태로 방사하는 발사체 생성
        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                // 발사체 생성
                GameObject clone = Instantiate(bossBullet, transform.position, Quaternion.identity);

                // 발사체 이동 방향(각도)
                float angle = weightAngle + intervalAngle * i;
                // 발사체 이동 방향(벡터)
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                // 발사체 이동 방향 설정
                clone.GetComponent<BossBullet>().Move(new Vector2(x, y));
            }

            // 발사체가 생성되는 시작 각도 설정을 위한 변수
            weightAngle += 1;

            //3초마다 미사일 발사
            yield return new WaitForSeconds(attackRate);
        }
    }

    // 좌우 이동
    private void Update()
    {
        if (transform.position.x >= 1)
            flag *= -1;
        if (transform.position.x <= -1)
            flag *= -1;

        transform.Translate(flag * speed * Time.deltaTime, 0, 0);
    }
}
