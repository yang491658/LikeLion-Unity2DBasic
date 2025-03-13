using System.Collections;
using UnityEngine;

public class CoroutineStudy : MonoBehaviour
{
    // ����Ƽ �ڷ�ƾ(Coroutine)
    // �ڷ�ƾ�� �Ϲ����� �Լ��� �޸� ������ ����ٰ� �ٽ� �̾ ������ �� �ִ� ���
    // �̸� �̿��ϸ� �����ð� �� ����ǰų�, Ư�� ������ ��ٸ��� ���� ����� ���� ����

    void Start()
    {
        //StartCoroutine("ExampleCoroutine");
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        //Debug.Log("�ڷ�ƾ ����");
        //yield return new WaitForSeconds(2f); // 2�� ���
        //Debug.Log("2�� �� ����");

        while (true)
        {
            Debug.Log("�� 1�ʸ��� ����");
            yield return new WaitForSeconds(1f);
        }
    }

    void Update()
    {
        
    }
}
