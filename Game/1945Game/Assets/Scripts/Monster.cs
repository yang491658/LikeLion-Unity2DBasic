using UnityEngine;

public class Monster : MonoBehaviour
{
    // ü��
    public int hp = 100;
    // �ӵ�
    public float speed = 1f;
    // ������
    public float delay = 1f;

    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;
    public GameObject item;

    void Start()
    {
        // �Լ� 1ȸ ȣ��
        Invoke("Shoot", delay);
    }

    void Shoot()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);

        // �ݺ� : ��� ȣ��
        Invoke("Shoot", delay);
    }

    void Update()
    {
        // �̵�
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    // ȭ�� ������ ����
    private void OnBecameInvisible()
    {
        // ����
        Destroy(gameObject);
        //PoolManager.Instance.Return(gameObject); // ������Ʈ Ǯ��
    }

    // �Ѿ˿� ���� ������ �Լ�
    public void Damage(int ATTACK)
    {
        hp -= ATTACK;

        if (hp <= 0)
        {
            ItemDrop();
            Destroy(gameObject);
            //PoolManager.Instance.Return(gameObject); // ������Ʈ Ǯ��
        }
    }

    // óġ �� ������ ����
    public void ItemDrop()
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }
}
