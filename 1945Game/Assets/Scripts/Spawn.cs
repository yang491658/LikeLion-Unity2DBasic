using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // ���� ���� ��ġ (�ּ�/�ִ�)
    public float minX = -2f;
    public float maxX = 2f;
    // ���� ���� �ð� (����/����)
    public float start = 1f;
    public float stop = 10f;
    // ���� ���� ����
    bool swi = true;
    bool swi2 = true;

    public GameObject monster;
    public GameObject monster2;
    public GameObject boss;

    [SerializeField]
    GameObject textBossWarning;

    private void Awake()
    {
        textBossWarning.SetActive(false);

        PoolManager.Instance.CreatePool(monster, 10); // ������Ʈ Ǯ��
    }

    void Start()
    {
        // �ڷ�ƾ ����
        StartCoroutine(RandomSpawn());
        // 10�� �� ���� ���� ����
        Invoke("Stop", stop);
    }

    // �ڷ�ƾ
    IEnumerator RandomSpawn()
    {
        while (swi)
        {
            // 1�� ����
            yield return new WaitForSeconds(start);
            // ���� x��
            float x = Random.Range(minX, maxX);

            Vector2 r = new Vector2(x, transform.position.y);

            // ���� ����
            //Instantiate(monster, r, Quaternion.identity);
            GameObject enemy = PoolManager.Instance.Get(monster); // ������Ʈ Ǯ��
            enemy.transform.position = r;
        }
    }

    IEnumerator RandomSpawn2()
    {
        while (swi2)
        {
            // 1�� ����
            yield return new WaitForSeconds(start + 2);
            // ���� x��
            float x = Random.Range(minX, maxX);

            Vector2 r = new Vector2(x, transform.position.y);

            // ���� ����
            //Instantiate(monster2, r, Quaternion.identity);
            GameObject enemy = PoolManager.Instance.Get(monster2); // ������Ʈ Ǯ��
            enemy.transform.position = r;
        }
    }

    void Stop()
    {
        swi = false;
        StopCoroutine(RandomSpawn());

        // �ι�° �ڷ�ƾ
        StartCoroutine(RandomSpawn2());

        // 30�� �� ����2 ���� ����
        Invoke("Stop2", stop + 20);
    }

    void Stop2()
    {
        swi2 = false;
        StopCoroutine(RandomSpawn2());

        // ���� ���� �ؽ�Ʈ
        textBossWarning.SetActive(true);

        // ���� ����
        Vector3 pos = new Vector3(0, 3, 0);
        GameObject go = Instantiate(boss, pos, Quaternion.identity);
    }
}
