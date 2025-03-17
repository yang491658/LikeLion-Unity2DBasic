using System.Collections.Generic;
using UnityEngine;

// �����հ� �ʱ� ũ�⸦ �޾� Ǯ �ʱ�ȭ
// �����ڿ��� �����հ� �ʱ� ũ�⸦ �޾� Ǯ�� �ʱ�ȭ
// �ʱ� ũ�⸸ŭ ������Ʈ�� �����Ͽ� Ǯ�� �߰�

// ���ο� ������Ʈ�� �����Ͽ� Ǯ�� �߰�
// CreateNewObject �޼���� ���ο� ������Ʈ�� �����ϰ� ��Ȱ��ȭ ���·� Ǯ�� �߰�

// Ǯ���� ��� ������ ������Ʈ�� ��������
// Get �޼���� Ǯ���� ��� ������ ������Ʈ�� ������
// Ǯ�� ��������� ���ο� ������Ʈ�� ����
// ������ ������Ʈ�� Ȱ��ȭ ���·� ��ȯ

// ����� ���� ������Ʈ�� Ǯ�� ��ȯ
// Return �޼���� ����� ���� ������Ʈ�� ��Ȱ��ȭ ���·� Ǯ�� ��ȯ
// �� Ŭ������ ������Ʈ Ǯ���� ���� ������Ʈ ������ �ı��� ���� ���� ���ϸ� ���̰�
// �޸� ������ ȿ�������� �� �� �ֵ��� ������

public class ObjectPool
{

    // Ǯ���� ������
    private GameObject prefab;

    // ��Ȱ��ȭ�� ������Ʈ���� �����ϴ� ť
    private Queue<GameObject> pool;

    // Ǯ���� ������Ʈ���� �θ� Ʈ������
    private Transform parent;

    // ������ : �����հ� �ʱ� ũ�⸦ �޾� Ǯ �ʱ�ȭ
    public ObjectPool(GameObject PREFAB, int INITIALSIZE, Transform PARENT = null)
    {
        this.prefab = PREFAB;
        this.parent = PARENT;
        pool = new Queue<GameObject>();

        for (int i = 0; i < INITIALSIZE; i++)
        {
            CreateNewObject();
        }
    }

    // ���ο� ������Ʈ�� �����Ͽ� Ǯ�� �߰��ϴ� private �޼���
    private void CreateNewObject()
    {
        GameObject obj = GameObject.Instantiate(prefab, parent);
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    // Ǯ���� ��� ������ ������Ʈ�� �������� �޼���
    // Ǯ�� ��������� ���� ����
    public GameObject Get()
    {
        if (pool.Count == 0)
        {
            CreateNewObject();
        }

        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    // ����� ���� ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
    public void Return(GameObject OBJ)
    {
        OBJ.SetActive(false);
        pool.Enqueue(OBJ);
    }
}
