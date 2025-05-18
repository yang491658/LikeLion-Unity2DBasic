using UnityEngine;

public class Item : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField] private ItemData item;

    // ������ ���� �Լ�
    public void SetItem(ItemData _itemData, Vector2 _velocity)
    {
        rb.linearVelocity = _velocity;
        item = _itemData;

        if (item != null) // ������ ������ ����
        {
            // ������ �̸� �� ������ ����
            gameObject.name = item.name;
            GetComponent<SpriteRenderer>().sprite = item.itemIcon;
        }
    }

    // ������ �浹 �Լ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null && // �÷��̾�� ����
            !collision.gameObject.GetComponent<CharacterStats>().isDead) // �÷��̾� ��� �ƴ�
        {
            PickupItem(); // ������ ȹ��
        }
    }

    // ������ ȹ�� �Լ�
    public void PickupItem()
    {
        Inventory.instance.AddItem(item); // �κ��丮 ������ �߰�

        Destroy(gameObject); // ������ ����
    }
}