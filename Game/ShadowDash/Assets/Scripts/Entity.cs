using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb; // ������ٵ�
    protected Animator ani; // �ִϸ�����

    [Header("�̵� ����")]
    protected int dircetion = 1; // ����
    protected bool isRight = true;

    [Header("�浹 ����")]
    [SerializeField] protected Transform groundCheck; // �ٴ� �浹 üũ
    [SerializeField] protected float groundDistance; // �ٴ� �浹 ���� �Ÿ�
    [SerializeField] protected LayerMask ground; // �ٴ� ���̾�
    protected bool isGround; // �ٴ� ����

    [Space]
    [SerializeField] protected Transform wallCheck; // �� �浹 üũ
    [SerializeField] protected float wallDistance; // �� �浹 ���� �Ÿ�
    protected bool isWall; // �� ����

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        CollisionCheck(); // �浹 üũ
    }

    protected virtual void Flip() // ���� ��ȯ
    {
        dircetion = dircetion * -1;
        isRight = !isRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos() // ����� �׸���
    {
        // �ٴ�
        Gizmos.DrawLine(groundCheck.position,
            new Vector3(
                groundCheck.position.x,
                groundCheck.position.y - groundDistance
                )
            );  

        // ��
        Gizmos.DrawLine(wallCheck.position,
            new Vector3(
                wallCheck.position.x + dircetion * wallDistance,
                wallCheck.position.y
                )
            );
    }

    protected virtual void CollisionCheck() // �浹 ����
    {
        // �ٴ�
        isGround = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, ground);

        // ��
        isWall = Physics2D.Raycast(wallCheck.position, Vector2.right, wallDistance, ground);
    }
}