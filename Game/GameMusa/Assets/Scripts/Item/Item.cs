using UnityEngine;

public class Item : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField] private ItemData item;

    // 아이템 설정 함수
    public void SetItem(ItemData _itemData, Vector2 _velocity)
    {
        rb.linearVelocity = _velocity;
        item = _itemData;

        if (item != null) // 아이템 데이터 있음
        {
            // 아이템 이름 및 아이콘 설정
            gameObject.name = item.name;
            GetComponent<SpriteRenderer>().sprite = item.itemIcon;
        }
    }

    // 물리적 충돌 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null && // 플레이어와 접촉
            !collision.gameObject.GetComponent<CharacterStats>().isDead) // 플레이어 사망 아님
        {
            PickupItem(); // 아이템 획득
        }
    }

    // 아이템 획득 함수
    public void PickupItem()
    {
        Inventory.instance.AddItem(item); // 인벤토리 아이템 추가

        Destroy(gameObject); // 아이템 제거
    }
}