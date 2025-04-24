using UnityEngine;

public class Enemy : Entity
{
    bool attack;// ���� ����

    [Header("�̵� ����")]
    [SerializeField] private float speed; // �ӵ�

    [Header("�÷��̾� ����")]
    [SerializeField] private float playerDistance; // �÷��̾� �浹 ���� �Ÿ�
    [SerializeField] private LayerMask player; // �÷��̾� ���̾�
    private RaycastHit2D isPlayer; // �÷��̾� ����


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        // �÷��̾� ���� �� ���� �� ����
        if (isPlayer)
        {
            if (isPlayer.distance > 1)
            {
                rb.linearVelocity = new Vector2(dircetion * speed * 2f, rb.linearVelocity.y);

                Debug.Log("�÷��̾� ����");
                attack = false;
            }
            else
            {
                Debug.Log("���� : " + isPlayer.collider.gameObject.name);

                attack = true;
            }
        }

        // �ٴ��� ������ ���� ȸ��  + ���� ������ ���� ȸ��
        if (!isGround || isWall) Flip();

        Move(); // �� �̵�
    }

    private void Move() // �̵�
    {
        if (!attack)
            rb.linearVelocity = new Vector2(dircetion * speed, rb.linearVelocity.y);
    }

    protected override void OnDrawGizmos() // ����� �׸���
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position,
            new Vector3(
                transform.position.x + dircetion * playerDistance,
                transform.position.y
                )
            );
    }

    protected override void CollisionCheck() // �浹 ����
    {
        base.CollisionCheck();

        isPlayer = Physics2D.Raycast(transform.position, Vector2.right, dircetion * playerDistance, player);
    }
}
