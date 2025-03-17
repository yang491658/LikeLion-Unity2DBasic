using System.Collections.Generic;
using UnityEngine;

// ������Ʈ Ǯ�� �������� ������ ����ϴ� �Ŵ��� Ŭ����

public class PoolManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    private static PoolManager instance;

    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("PoolManager");
                instance = go.AddComponent<PoolManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    // ������ �̸��� Ű�� ����ϴ� Ǯ ��ųʸ�
    private Dictionary<string, ObjectPool> pools = new Dictionary<string, ObjectPool>();

    // ���ο� ������Ʈ Ǯ�� �����ϴ� �޼���
    // prefab : Ǯ���� ������
    // initialSize: �ʱ� Ǯ ũ��
    public void CreatePool(GameObject PREFAB, int INITIALSIZE)
    {
        string key = PREFAB.name;
        if (!pools.ContainsKey(key))
        {
            pools.Add(key, new ObjectPool(PREFAB, INITIALSIZE, transform));
        }
    }

    // Ǯ���� ������Ʈ�� �������� �޼���
    // ��û�� �������� Ǯ�� ���ٸ� ���� ����
    public GameObject Get(GameObject PREFAB)
    {
        string key = PREFAB.name;
        if (!pools.ContainsKey(key))
        {
            CreatePool(PREFAB, 10);
        }
        return pools[key].Get();
    }
    
    // ����� ���� ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
    public void Return(GameObject OBJ)
    {
        string key = OBJ.name.Replace("(Clone)", "");
        if (pools.ContainsKey(key))
        {
            pools[key].Return(OBJ);
        }
    }
}
