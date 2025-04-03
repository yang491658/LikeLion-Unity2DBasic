using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb; // ������ٵ�
    private Animator ani; // �ִϸ�����

    private float xInput;

    [SerializeField] private float speed; // �ӵ�
    [SerializeField] private float jump; // ������

    private int dircetion = 1; // ����
    private bool isRight = true;

    [Header("�浹 ����")]
    [SerializeField] private float distance; // �浹 ���� �Ÿ�
    [SerializeField] private LayerMask ground;
    private bool isGround; // �浹 ����

    [Header("�뽬 ����")]
    [SerializeField] private float dashSpeed; // �뽬 �ӵ�
    [SerializeField] private float dashDuration; // �뽬 ���� �ð�
    private float dashTimer; // �뽬 �ߵ� Ÿ�̸�
    [SerializeField] private float dashCoolTime; // �뽬 ��Ÿ��
    private float dashCooldownTimer; // �뽬 ��Ÿ�� Ÿ�̸�

    [Header("���� ����")]
    [SerializeField] private float comboTime = 0.3f; // �޺� ���� �ð�
    private bool attack; // ���� ����
    private int combo; // �޺�
    private float comboTimer; // �޺� Ÿ�̸�

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

        // �뽬 Ÿ�̸�
        dashTimer -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;

        // �޺� Ÿ�̸�
        comboTimer -= Time.deltaTime;

        AnimationControl(); // �÷��̾� �̵� ���
    }

    private void InputKey() // Ű �Է�
    {
        xInput = Input.GetAxisRaw("Horizontal");

        // �÷��̾� ���� ��ȯ
        if (Input.GetKeyDown(KeyCode.R)) Flip();

        // �÷��̾� ����
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        // �÷��̾� �뽬
        if (Input.GetKeyDown(KeyCode.LeftShift)) CanDash();

        // �÷��̾� ����
        if (Input.GetKeyDown(KeyCode.Mouse0)) Attack();
    }

    private void Move() // �̵� �� �뽬
    {
        if (attack)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
        else if (dashTimer > 0)
        {
            rb.linearVelocity = new Vector2(xInput * dashSpeed, rb.linearVelocityY);
        }
        else
        {
            rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocityY);
        }
    }

    private void Flip() // ���� ��ȯ
    {
        dircetion = dircetion * -1;
        isRight = !isRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipControl() // ���� ��ȯ ��Ʈ��
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

    private void Jump() // ����
    {
        if (isGround) rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump);
    }

    private void OnDrawGizmos() // ����� �׸���
    {
        Gizmos.DrawLine(transform.position,
            new Vector3(transform.position.x,
            transform.position.y - distance));
    }

    private void CollisionCheck() // �浹 ����
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, distance, ground);
    }


    private void CanDash() // �뽬 ����
    {
        if (dashCooldownTimer < 0 && !attack)
        {
            dashTimer = dashDuration;
            dashCooldownTimer = dashCoolTime;
        }
    }

    private void Attack() // ����
    {
        if (!isGround) return; // ���� �Ұ���

        if (comboTimer < 0) combo = 0; // �޺� �ʱ�ȭ

        attack = true;

        comboTimer = comboTime;
    }

    public void AttackOver() // ���� ����
    {
        attack = false;

        combo++; // �޺� ���

        if (combo > 2) combo = 0; // �޺� �ʱ�ȭ
    }

    private void AnimationControl() // �ִϸ��̼� ��Ʈ��
    {
        // �̵� ���
        bool move = rb.linearVelocity.x != 0;
        ani.SetBool("Move", move);

        // ���� �� ���� ���
        ani.SetBool("IsGround", isGround);
        ani.SetFloat("ySpeed", rb.linearVelocityY);

        // �뽬 ���
        ani.SetBool("Dash", dashTimer > 0);

        // ���� ���
        ani.SetBool("Attack", attack);
        ani.SetInteger("Combo", combo);
    }
}