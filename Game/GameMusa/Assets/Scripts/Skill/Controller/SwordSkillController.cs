using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    // ������Ʈ
    private CircleCollider2D col;
    private Rigidbody2D rb;
    private Animator anim;

    private Player player; // �÷��̾�

    private bool isRotating = true; // ȸ�� ����

    private float returnSpeed; // ȸ�� �ӵ�
    private bool isReturning; // ȸ�� ����
    private float freezeTimeDuration; // �ð� ���� ���ӽð�

    [Header("�ٿ ����")]
    private float bounceSpeed; // �ٿ �ӵ�
    private int bounceAmount; // �ٿ Ƚ��
    private bool isBouncing; // �ٿ ����
    private List<Transform> enemyTarget; // Ÿ�� �迭
    private int targetIndex; // Ÿ�� ����

    [Header("���� ����")]
    private float pierceAmount; // ���� Ƚ��

    [Header("���� ����")]
    private float spinDistance; // ���� �Ÿ�
    private float spinDuration; // ���� ���ӽð�
    private float spinTimer; // ���� Ÿ�̸�
    private bool isSpinning; // ���� ����

    private float moveDirection; // �̵� ����
    private bool isHitting; // Ÿ�� ����
    private float hitCooldown; // Ÿ�� ��ٿ�
    private float hitTimer; // Ÿ�� Ÿ�̸�

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isRotating) // ȸ�� ��
        {
            // �ҵ� ���� ����
            transform.right = rb.linearVelocity;
        }

        if (isReturning) // ȸ�� ��
        {
            // �ҵ� �̵� = �÷��̾ ���� �̵�
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.transform.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1) // �ҵ尡 �÷��̾ ����
            {
                // �÷��̾� �ҵ� ���
                player.CatchSwrod();
            }
        }

        BounceSword(); // �ҵ� �ٿ

        SpinSword(); // �ҵ� ����
    }

    // �ҵ� �ٿ �Լ�
    private void BounceSword()
    {
        if (isBouncing && // �ٿ ��
            enemyTarget.Count > 0) // Ÿ�� ����
        {
            // �ҵ� �̵� = Ÿ���� ���� �̵�
            transform.position = Vector2.MoveTowards(
                transform.position,
                enemyTarget[targetIndex].position,
                bounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < 0.1f)
            // �ҵ尡 Ÿ�ٿ� ����
            {
                DamageBySword(enemyTarget[targetIndex].GetComponent<Enemy>()); // �ҵ� ������

                targetIndex++; // ���� Ÿ�� ����
                bounceAmount--; // �ٿ Ƚ�� ����

                if (bounceAmount <= 0) // �ٿ Ƚ�� ����
                {
                    isBouncing = false; // �ٿ ����
                    isReturning = true; // ȸ�� ����
                }

                if (targetIndex >= enemyTarget.Count) // ���� Ÿ�� ����
                {
                    targetIndex = 0; // Ÿ�� ���� �ʱ�ȭ
                }
            }
        }
    }

    // �ҵ� ���� �Լ�
    private void SpinSword()
    {
        if (isSpinning) // ���� ��
        {
            if (!isHitting && // Ÿ�� �� �ƴ�
                Vector2.Distance(player.transform.position, transform.position) > spinDistance)
            // ���� �Ÿ� ����
            {
                StartHit(); // Ÿ�� ����
            }

            HitBySpin(); // ���� Ÿ�� Ÿ��
        }
    }

    // Ÿ�� ���� �Լ�
    private void StartHit()
    {
        isHitting = true; // Ÿ�� ����

        rb.constraints = RigidbodyConstraints2D.FreezePosition; // ��ġ ����

        spinTimer = spinDuration; // ���� Ÿ�̸� �ʱ�ȭ = ���� ���ӽð�
    }

    // ���� Ÿ�� �Լ�
    private void HitBySpin()
    {
        if (isHitting) // Ÿ�� ��
        {
            // ���� õõ�� �̵�
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(transform.position.x + moveDirection, transform.position.y),
                1.5f * Time.deltaTime);

            spinTimer -= Time.deltaTime; // ���� Ÿ�̸� ����

            if (spinTimer < 0) // ���� ���ӽð� ����
            {
                isSpinning = false; // ���� ����
                isReturning = true; // ȸ�� ����
            }

            hitTimer -= Time.deltaTime; // Ÿ�� Ÿ�̸� ����

            if (hitTimer < 0) // Ÿ�� ��ٿ� ����
            {
                hitTimer = hitCooldown; // Ÿ�� Ÿ�̸� �ʱ�ȭ = Ÿ�� ��ٿ�

                // �ݶ��̴� ���� = ���� Ÿ�� ����
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null) // ���� ����
                    {
                        // �� ������ ����Ʈ
                        //hit.GetComponent<Enemy>().DamageEffect();

                        DamageBySword(hit.GetComponent<Enemy>()); // �ҵ� ������
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning) return; // ȸ�� �� ����

        // ���� �浹 �� �� ������ ����Ʈ
        //collision.GetComponent<Enemy>()?.DamageEffect();

        if (collision.GetComponent<Enemy>() != null) // ���� �浹
        {
            DamageBySword(collision.GetComponent<Enemy>()); // �ҵ� ������
        }

        SetTargetsForBounce(collision); // �ٿ Ÿ�� ����

        StuckSword(collision); // �ҵ� ����
    }

    // �ٿ Ÿ�� ���� �Լ�
    private void SetTargetsForBounce(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null && // ���� �浹
            isBouncing && // �ٿ ��
            enemyTarget.Count <= 0) // Ÿ�� ����
        {
            // �ݶ��̴� ���� = Ÿ�� ���� ����
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null) // ���� ����
                {
                    enemyTarget.Add(hit.transform); // Ÿ�� �߰�
                }
            }
        }
    }

    // �ҵ� ���� �Լ�
    private void StuckSword(Collider2D collision)
    {
        if (isSpinning) // ���� ��
        {
            StartHit(); // Ÿ�� ����
            return; // ���� ����
        }

        if (pierceAmount > 0 && // ���� Ƚ�� ����
            collision.GetComponent<Enemy>() != null) // ���� �浹
        {
            pierceAmount--; // ���� Ƚ�� ����
            return; // ���� ����
        }

        if (!isBouncing) // �ٿ �� �ƴ�
        {
            // �浹 ��Ȱ��ȭ
            col.enabled = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // ��ġ ����
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && enemyTarget.Count > 0) return; // �ٿ �� + Ÿ�� ���� �� ���� ����

        anim.SetBool("Rotate", false); // �ҵ� �ִϸ��̼� ����

        isRotating = false; // ȸ�� ����

        // �浹ü�� ����
        transform.parent = collision.transform;
    }

    // �ҵ� ���� �Լ�
    //public void SetSword(Vector2 _dir, float _gravityScale)
    //public void SetSword(Vector2 _dir, float _gravityScale, Player _player, float _returnSpeed)
    public void SetSword
        (Vector2 _dir, float _gravityScale, Player _player, float _returnSpeed, float _freezeTimeDuration)
    {
        rb.linearVelocity = _dir; // �ӵ� �� ����
        rb.gravityScale = _gravityScale; // �߷�

        if (pierceAmount <= 0) // ���� Ƚ�� ����
        {
            anim.SetBool("Rotate", true); // �ҵ� �ִϸ��̼� ����
        }

        player = _player; // �÷��̾�
        returnSpeed = _returnSpeed; // ȸ�� �ӵ�

        moveDirection = Mathf.Clamp(rb.linearVelocity.x, -1, 1); // �̵� ����

        freezeTimeDuration = _freezeTimeDuration; // �ð� ���� ���ӽð�

        Invoke("DestroySword", 7); // ���� �ð� �� �ҵ� ����
    }

    // �ٿ ���� �Լ�
    public void SetBounce(float _bounceSpeed, int _bounceAmount, bool _isBouncing)
    {
        bounceSpeed = _bounceSpeed;
        bounceAmount = _bounceAmount;
        isBouncing = _isBouncing;

        enemyTarget = new List<Transform>();
    }

    // ���� ���� �Լ�
    public void SetPierce(int _pierceAmount)
    {
        pierceAmount = _pierceAmount;
    }

    // ���� ���� �Լ�
    public void SetupSpin(float _spinDistance, float _spinDuration, bool _isSpinning, float _hitCooldown)
    {
        spinDistance = _spinDistance;
        spinDuration = _spinDuration;
        isSpinning = _isSpinning;

        hitCooldown = _hitCooldown;
    }

    // �ҵ� ȸ�� �Լ�
    public void ReturnSword()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = null;
        isReturning = true; // ȸ�� ����
    }

    // �ҵ� ������ �Լ�
    private void DamageBySword(Enemy enemy)
    {
        // �� ������ ����Ʈ
        enemy.DamageEffect();

        // �ð� ���� �ڷ�ƾ
        enemy.StartCoroutine("FreezeTimerFor", freezeTimeDuration);
    }

    // �ҵ� ���� �Լ�
    private void DestroySword()
    {
        Destroy(gameObject); // �ҵ� ����
    }
}