using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 5f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= spawnInterval)
        {
            SpawnRandomEnemy();
            _timer = 0;
        }
    }

    private void SpawnRandomEnemy()
    {
        // 랜덤 위치 계산
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));

        // 랜덤 적 타입 선택
        EnemyType randomType = (EnemyType)Random.Range(0, 4);

        // 팩토리를 사용하여 적 생성
        IEnemy enemy = EnemyFactory.Instance.CreateEnemy(randomType, spawnPosition);

        Debug.Log($"{randomType} 적이 {spawnPosition}에 생성되었습니다.");
    }
}