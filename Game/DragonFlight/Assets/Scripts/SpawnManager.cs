using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // �� ��ü�� �޾ƿ� ����
    public GameObject enemy;

    // �� ���� �Լ�
    void SpawnEnemy()
    {
        float randomX = Random.Range(-2f, 2f); // �� ������ ���� ��ǥ

        // �� ����
        Instantiate(enemy, new Vector3(randomX, transform.position.y, 0f), Quaternion.identity);
    }
    void Start()
    {
        // �� ���� �Լ� �ݺ� ���
        InvokeRepeating("SpawnEnemy", 1, 0.5f);
    }

    void Update()
    {

    }
}
