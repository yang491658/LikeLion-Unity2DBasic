using UnityEngine;

public class CrystalSkillController : MonoBehaviour
{
    // ������Ʈ
    private CircleCollider2D col => GetComponent<CircleCollider2D>();
    private Animator anim => GetComponent<Animator>();

    private float crystalTimer; // ũ����Ż Ÿ�̸�

    [Header("���� ����")]
    [SerializeField] private float growSpeed; // ��â �ӵ�
    private bool canGrow; // ��â ���� ����
    private bool isExploding; // ���� ����

    [Header("�̵� ����")]
    private float moveSpeed; // �̵� �ӵ�
    private bool isMoving; // �̵� ����
    private Transform target; // Ÿ��
    [SerializeField] private LayerMask enemy; // �� ���̾�

    private void Update()
    {
        crystalTimer -= Time.deltaTime; // ũ����Ż Ÿ�̸� ����

        if (crystalTimer < 0) // ũ����Ż ���ӽð� ����
        {
            //DestroyCrystal(); // ũ����Ż ����
            FinishSkill(); // ��ų ����
        }

        if (isMoving) // �̵� ��
        {
            // ũ����Ż �̵�
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.transform.position,
                moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target.transform.position) < 1) // ũ����Ż�� Ÿ�ٿ� ����
            {
                isMoving = false; // �̵� ����
                FinishSkill(); // ��ų ����
            }
        }

        if (canGrow) // ��â ����
        {
            // ũ����Ż ��â
            transform.localScale = Vector2.Lerp(
                transform.localScale,
                new Vector2(3, 3),
                growSpeed * Time.deltaTime);
        }
    }

    // ��ų ���� �Լ�
    public void FinishSkill()
    {
        if (isExploding) // ���� ��
        {
            canGrow = true; // ��â ����
            anim.SetTrigger("Explode"); // ũ����Ż �ִϸ��̼� ����
        }
        else
        {
            DestroyCrystal(); // ũ����Ż ����
        }
    }

    // Ÿ�� ���� �Լ�
    public void ChoiceTarget()
    {
        // Ÿ�� ���� ����
        float radius = SkillManager.instance.blackhole.GetSize() / 2;

        // �ݶ��̴� ���� = Ÿ�� ���� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, enemy);

        if (colliders.Length > 0) // Ÿ�� ����
        {
            target = colliders[Random.Range(0, colliders.Length)].transform; // ���� Ÿ��
        }
    }

    // ũ����Ż ���� �Լ�
    //public void SetCrystal(float _crystalDuration)
    //public void SetCrystal(float _crystalDuration, bool _isExploding)
    public void SetCrystal(float _crystalDuration, bool _isExploding, 
        float _moveSpeed, bool _isMoving, Transform _target)
    {
        crystalTimer = _crystalDuration; // ũ����Ż Ÿ�̸� �ʱ�ȭ = ũ����Ż ���ӽð�

        isExploding = _isExploding;

        moveSpeed = _moveSpeed;
        isMoving = _isMoving;
        target = _target;
    }

    // ũ����Ż ���� �Լ�
    public void DestroyCrystal() => Destroy(gameObject);

    // ���� Ʈ���� �Լ� �̺�Ʈ
    private void AttackTrigger()
    {
        // �ݶ��̴� ���� = ���� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, col.radius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null) // ���� ����
            {
                // �� ������ ����Ʈ
                hit.GetComponent<Enemy>().DamageEffect();
            }
        }
    }
}