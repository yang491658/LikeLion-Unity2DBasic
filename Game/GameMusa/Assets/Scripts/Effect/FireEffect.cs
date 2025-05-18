using UnityEngine;

[CreateAssetMenu(fileName = "FireEffect", menuName = "Data/Effect/Fire")]
public class FireEffect : ItemEffect
{
    [SerializeField] private GameObject firePrefab; // �Ҳ� ������
    [SerializeField] private float fireSpeed; // �Ҳ� �ӵ�

    // ȿ�� ���� �Լ� (���)
    public override void DoEffect(Transform _respawn)
    {
        // �÷��̾�
        Player player = PlayerManager.instance.player;

        if (player.attackState.combo == 2) // �÷��̾� �ִ� �޺�
        {
            // ���ο� �Ҳ� ����
            GameObject newFire = Instantiate(firePrefab, _respawn.position, player.transform.rotation);

            // �Ҳ� �̵�
            newFire.GetComponent<Rigidbody2D>().linearVelocity 
                = new Vector2( player.direction * fireSpeed, 0);

            // �����ð� �� �Ҳ� ����
            Destroy(newFire, 10);
        }
    }
}
