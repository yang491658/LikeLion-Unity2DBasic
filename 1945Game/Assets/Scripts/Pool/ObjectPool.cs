using System.Collections.Generic;
using UnityEngine;

// 프리팹과 초기 크기를 받아 풀 초기화
// 생성자에서 프리팹과 초기 크기를 받아 풀을 초기화
// 초기 크기만큼 오브젝트를 생성하여 풀에 추가

// 새로운 오브젝트를 생성하여 풀에 추가
// CreateNewObject 메서드는 새로운 오브젝트를 생성하고 비활성화 상태로 풀에 추가

// 풀에서 사용 가능한 오브젝트를 가져오기
// Get 메서드는 풀에서 사용 가능한 오브젝트를 가져옴
// 풀이 비어있으면 새로운 오브젝트를 생성
// 가져온 오브젝트를 활성화 상태로 반환

// 사용이 끝난 오브젝트를 풀로 반환
// Return 메서드는 사용이 끝난 오브젝트를 비활성화 상태로 풀에 반환
// 이 클래스는 오브젝트 풀링을 통해 오브젝트 생성과 파괴에 따른 성능 저하를 줄이고
// 메모리 관리를 효율적으로 할 수 있도록 도와줌

public class ObjectPool
{

    // 풀링할 프리팹
    private GameObject prefab;

    // 비활성화된 오브젝트들을 보관하는 큐
    private Queue<GameObject> pool;

    // 풀링된 오브젝트들의 부모 트랜스폼
    private Transform parent;

    // 생성자 : 프리팹과 초기 크기를 받아 풀 초기화
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

    // 새로운 오브젝트를 생성하여 풀에 추가하는 private 메서드
    private void CreateNewObject()
    {
        GameObject obj = GameObject.Instantiate(prefab, parent);
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    // 풀에서 사용 가능한 오브젝트를 가져오는 메서드
    // 풀이 비어있으면 새로 생성
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

    // 사용이 끝난 오브젝트를 풀로 반환하는 메서드
    public void Return(GameObject OBJ)
    {
        OBJ.SetActive(false);
        pool.Enqueue(OBJ);
    }
}
