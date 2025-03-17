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
        Invoke("Hide", 3); // 3�� �� �ؽ�Ʈ ����
        StartCoroutine(Shoot());
        StartCoroutine(Fire());
    }

    // �ؽ�Ʈ �����
    void Hide()
    {
        GameObject.Find("TextBossWarning").SetActive(false);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            // �Ѿ� ����
            Instantiate(mBullet, pos1.position, Quaternion.identity);
            Instantiate(mBullet, pos2.position, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }
    }

    // ���������� ������ �߻�
    IEnumerator Fire()
    {
        // ���� �ֱ�
        float attackRate = 3f;
        // �߻�ü ��������
        int count = 30;
        // �߻�ü ������ ����
        float intervalAngle = 360 / count;
        // ���ߵǴ� ���� (�׻� ���� ��ġ�� �߻����� �ʵ��� ����)
        float weightAngle = 0;

        // �� ���·� ����ϴ� �߻�ü ����
        while (true)
        {
            for (int i = 0; i < count; ++i)
            {
                // �߻�ü ����
                GameObject clone = Instantiate(bossBullet, transform.position, Quaternion.identity);

                // �߻�ü �̵� ����(����)
                float angle = weightAngle + intervalAngle * i;
                // �߻�ü �̵� ����(����)
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);

                // �߻�ü �̵� ���� ����
                clone.GetComponent<BossBullet>().Move(new Vector2(x, y));
            }

            // �߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
            weightAngle += 1;

            //3�ʸ��� �̻��� �߻�
            yield return new WaitForSeconds(attackRate);
        }
    }

    // �¿� �̵�
    private void Update()
    {
        if (transform.position.x >= 1)
            flag *= -1;
        if (transform.position.x <= -1)
            flag *= -1;

        transform.Translate(flag * speed * Time.deltaTime, 0, 0);
    }
}
