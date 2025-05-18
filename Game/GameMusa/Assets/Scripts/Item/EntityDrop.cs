using System.Collections.Generic;
using UnityEngine;

public class EntityDrop : MonoBehaviour
{
    [Header("엔티티 드랍")]
    [SerializeField] private ItemData[] items; // 아이템 목록
    private List<ItemData> drops = new List<ItemData>(); // 드랍 목록
    [SerializeField] private int dropCount; // 드랍 수
    [SerializeField] private GameObject dropPrefab; // 드랍 프리팹

    // 드랍 생성 함수
    public virtual void GenerateDrops()
    {
        for (int i = 0; i < items.Length; i++) // 아이템 목록
        {
            if (Random.Range(0, 100) < items[i].dropChance) // 아이템 드랍 확률
            {
                drops.Add(items[i]); // 드랍 항목 추가
            }
        }

        for (int i = 0; i < dropCount; i++) // 드랍 수
        {
            ItemData randomItem = drops[Random.Range(0, drops.Count)]; // 랜덤 아이템

            DropItem(randomItem); // 아이템 드랍

            drops.Remove(randomItem); // 드랍 항목 제거
        }
    }

    // 아이템 드랍 함수
    protected void DropItem(ItemData _itemData)
    {
        // 새로운 아이템 생성
        GameObject newItem = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        // 랜덤 속도
        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));

        // 아이템 설정
        newItem.GetComponent<Item>().SetItem(_itemData, randomVelocity);
    }
}