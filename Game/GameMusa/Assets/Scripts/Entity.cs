using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region ������Ʈ
    public SpriteRenderer sr { get; private set; } // ��������Ʈ ������
    public Animator anim { get; private set; } // �ִϸ�����
    public Collider2D col { get; private set; } // �ݶ��̴�
    public Rigidbody2D rb { get; private set; } // ������ٵ�
    public EntityFX fx { get; private set; } // Ư��ȿ��
    public CharacterStats stats { get; private set; } // ĳ���� ����
    #endregion

    public int direction { get; private set; } = 1; // ����
    protected bool isRight = true; // ���� = ������

    [Header("�˹� ����")]
    [SerializeField] protected Vector2 knockbackDirection; // �˹� ����
    [SerializeField] protected float knockbackDuration; // �˹� ���ӽð�
    protected bool isKnock; // �˹� ����

    [Header("�浹 ����")]
    [SerializeField] protected LayerMask groundLayer; // �ٴ� ���̾�
    [SerializeField] protected Transform groundCheck; // �ٴ� ����
    [SerializeField] protected float groundDistance; // �ٴ� ���� �Ÿ�
    [SerializeField] protected Transform wallCheck; // �� ����
    [SerializeField] protected float wallDistance; // �� ���� �Ÿ�
    public Transform attackCheck; // ���� ����
    public float attackRadius; // ���� ���� ����

    public System.Action onFlip; // ���� ��ȯ ��������Ʈ

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        // ������Ʈ ��������
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        stats = GetComponent<CharacterStats>();
    }

    protected virtual void Update()
    {
    }

    // ������ ����Ʈ �Լ�
    public virtual void DamageEffect()
    {
        // ������ �ڷ�ƾ
        fx.StartCoroutine("Blink");

        // �˹� �ڷ�ƾ
        StartCoroutine(KnockBack());
    }

    // �˹� �ڷ�ƾ
    protected virtual IEnumerator KnockBack()
    {
        isKnock = true; // �˹� ��

        // ��ƼƼ �˹�
        rb.linearVelocity = new Vector2(-direction * knockbackDirection.x, knockbackDirection.y);

        yield return new WaitForSeconds(knockbackDuration);

        isKnock = false; // �˹� ����
    }

    #region �ӵ�
    // �ӵ� ���� �Լ�
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnock) return; // �˹� �� ����

        // ��ƼƼ �̵�
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);

        FlipControl(_xVelocity); // ���� ��ȯ ��Ʈ��
    }

    // ���� �Լ�
    public virtual void SetZeroVelocity()
    {
        if (isKnock) return; // �˹� �� ����

        // ��ƼƼ ����
        rb.linearVelocity = new Vector2(0, 0);
    }

    // ��ȭ �Լ�
    public virtual void Slow(float _slowPercentage, float _slowDuration)
    {
    }

    // ��ȭ ��� �Լ�
    protected virtual void CancelSlow()
    {
        anim.speed = 1;
    }
    #endregion

    #region ���� ��ȯ
    // ���� ��ȯ �Լ�
    public virtual void Flip()
    {
        direction = direction * -1;
        isRight = !isRight;
        transform.Rotate(0, 180, 0);

        // ���� ��ȯ ��������Ʈ �޼��� ȣ��
        onFlip?.Invoke();
    }

    // ���� ��Ʈ�� �Լ� : �̵� �� ���� ��ȯ
    public virtual void FlipControl(float _x)
    {
        if (_x > 0 && !isRight)
            Flip(); // ���� ��ȯ
        else if (_x < 0 && isRight)
            Flip(); // ���� ��ȯ
    }
    #endregion

    #region �浹
    // ����� �׸��� �Լ�
    protected virtual void OnDrawGizmos()
    {
        // �ٴ�
        Gizmos.DrawLine(groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundDistance));

        // ��
        Gizmos.DrawLine(wallCheck.position,
            new Vector3(wallCheck.position.x + direction * wallDistance, wallCheck.position.y));

        // ����
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
    }

    // �ٴ� ���� �Լ�
    public virtual bool IsGround()
        => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundLayer);

    // �� ���� �Լ�
    public virtual bool IsWall()
        => Physics2D.Raycast(wallCheck.position, Vector2.right, direction * wallDistance, groundLayer);
    #endregion

    // ����ȭ �Լ�
    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
            sr.color = Color.clear; // ����ȭ
        else
            sr.color = Color.white; // ���󺹱�
    }

    // ��� �Լ�
    public virtual void Die()
    {
    }
}