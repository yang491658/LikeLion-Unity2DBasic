using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // 프리팹을 저장할 변수 : 적
    public GameObject enemy;
    public GameObject enemyEasy;
    public GameObject enemyNormal;
    public GameObject enemyHard;
    public GameObject enemyBoss;

    void Start()
    {
        InvokeRepeating("Spawn", 1, 0.5f);
    }

    void Spawn()
    {
        // 생성할 랜덤 좌표
        float randomX = Random.Range(-2f, 2f);

        if (GameManager.score < 1000)
        {
            enemy = enemyEasy;
        }
        else if (GameManager.score < 2000)
        {
            enemy = enemyNormal;
        }
        else if (GameManager.score < 5000)
        {
            enemy = enemyHard;
        }
        else 
        {
            enemy = enemyBoss;
        }

        Instantiate(enemy, new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
    }

    void Update()
    {

    }
}
