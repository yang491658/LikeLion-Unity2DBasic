using System.Collections.Generic;
using UnityEngine;

public class EntityDrop : MonoBehaviour
{
    [Header("��ƼƼ ���")]
    [SerializeField] private ItemData[] items; // ������ ���
    private List<ItemData> drops = new List<ItemData>(); // ��� ���
    [SerializeField] private int dropCount; // ��� ��
    [SerializeField] private GameObject dropPrefab; // ��� ������

    // ��� ���� �Լ�
    public virtual void GenerateDrops()
    {
        for (int i = 0; i < items.Length; i++) // ������ ���
        {
            if (Random.Range(0, 100) < items[i].dropChance) // ������ ��� Ȯ��
            {
                drops.Add(items[i]); // ��� �׸� �߰�
            }
        }

        for (int i = 0; i < dropCount; i++) // ��� ��
        {
            ItemData randomItem = drops[Random.Range(0, drops.Count)]; // ���� ������

            DropItem(randomItem); // ������ ���

            drops.Remove(randomItem); // ��� �׸� ����
        }
    }

    // ������ ��� �Լ�
    protected void DropItem(ItemData _itemData)
    {
        // ���ο� ������ ����
        GameObject newItem = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        // ���� �ӵ�
        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));

        // ������ ����
        newItem.GetComponent<Item>().SetItem(_itemData, randomVelocity);
    }
}