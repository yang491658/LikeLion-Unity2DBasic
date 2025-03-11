using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // 적 객체를 받아올 변수
    public GameObject enemy;

    // 적 생성 함수
    void SpawnEnemy()
    {
        float randomX = Random.Range(-2f, 2f); // 적 생성될 랜덤 좌표

        // 적 생성
        Instantiate(enemy, new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
    }
    void Start()
    {
        // 적 생성 함수 반복 사용
        InvokeRepeating("SpawnEnemy", 1, 0.5f);
    }

    void Update()
    {

    }
}
