using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb; // ������ٵ�
    private Animator ani; // �ִϸ�����

    private float xInput;

    [SerializeField] private float speed; // �ӵ�
    [SerializeField] private float jump; // ������

    [SerializeField] private bool move; // �̵� ����

    private int dircetion = 1; // ����
    private bool isRight = true;

    [Header("Collision Info")]
    [SerializeField] private float distance;
    [SerializeField] private LayerMask ground;
    private bool isGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        InputKey(); // Ű �Է�
        Move(); // �÷��̾� �̵�
        FlipControl(); // �÷��̾� �̵� �� ���� ��ȯ
        CollisionCheck(); // �浹 üũ
        AnimationControl(); // �÷��̾� �̵� ���
    }

    private void InputKey()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        // �÷��̾� ���� ��ȯ
        if (Input.GetKeyDown(KeyCode.R)) Flip();

        // �÷��̾� ����
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocityY);
    }

    private void Flip()
    {
        dircetion = dircetion * -1;
        isRight = !isRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipControl()
    {
        if (rb.linearVelocityX > 0 && !isRight)
        {
            Flip();
        }
        else if (rb.linearVelocityX < 0 && isRight)
        {
            Flip();
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - distance));
    }

    private void CollisionCheck()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, distance, ground);
    }

    private void AnimationControl()
    {
        move = rb.linearVelocity.x != 0;
        ani.SetBool("Move", move);
    }
}