using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    // ������Ʈ
    private SpriteRenderer sr;
    private Animator anim;

    [SerializeField] private float disappearSpeed; // ������� �ӵ� = ����ȭ �ӵ�
    private float cloneTimer; // Ŭ�� Ÿ�̸�

    [SerializeField] private Transform attackCheck; // ���� ����
    [SerializeField] private float attackRadius = 0.8f; // ���� ���� ����
    private Transform target; // Ÿ��

    [Header("���� ����")]
    private int direction = 1; // ����
    private bool canDuplicate; // ���� ���� ����
    private float duplicateChance; // ���� Ȯ��

    private void Awake()
    {
        // ������Ʈ ��������
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime; // Ŭ�� Ÿ�̸� ����

        if (cloneTimer < 0) // Ŭ�� ���ӽð� ����
        {
            // Ŭ�� ���� ����ȭ
            sr.color = new Color(1, 1, 1, sr.color.a - disappearSpeed * Time.deltaTime);

            if (sr.color.a <= 0) // Ŭ�� ���� ����ȭ
            {
                Destroy(gameObject); // Ŭ�� ����
            }
        }
    }
    
    // Ŭ�� ���� �Լ�
    //public void SetClone(bool _canAttack, Transform _transform, float _cloneDuration)
    //public void SetClone(bool _canAttack, Transform _transform, float _cloneDuration, Vector3 _offest)
    public void SetClone(bool _canAttack, Transform _transform, float _cloneDuration,
        //Vector3 _offest, Transform _target)
        Vector3 _offest, Transform _target, bool _canDuplicate, float _duplicateChance)
    {
        if (_canAttack) // ���� ����
        {
            anim.SetInteger("Attack", Random.Range(1, 4)); // Ŭ�� �ִϸ��̼� ���� (����)
        }

        //transform.position = _transform.position; // Ŭ�� ��ġ
        transform.position = _transform.position + _offest; // Ŭ�� ��ġ + ������ ����
        cloneTimer = _cloneDuration; // Ŭ�� Ÿ�̸� �ʱ�ȭ = Ŭ�� ���ӽð�

        target = _target; // ���� Ÿ��
        FaceTarget(); // Ÿ�� ����

        canDuplicate = _canDuplicate;
        duplicateChance = _duplicateChance;
    }

    // Ÿ�� ���� �Լ� : ���� ����� Ÿ�� ã�� + Ÿ������ ���� ��ȯ
    private void FaceTarget()
    {
        // Ÿ������ ���� ��ȯ
        if (target != null)
        {
            if (transform.position.x > target.position.x) // Ŭ���� Ÿ���� ���ʿ� ����
            {
                // Ŭ�� ���� ��ȯ
                direction = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }

    // �ִϸ��̼� Ʈ���� �Լ� �̺�Ʈ
    private void AnimationTrigger()
    {
        cloneTimer = -0.1f; // Ŭ�� Ÿ�̸� ����
    }

    // ���� Ʈ���� �Լ� �̺�Ʈ
    private void AttackTrigger()
    {
        // �ݶ��̴� ���� = �÷��̾� ���� ����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null) // ���� ����
            {
                // �� ������ ����Ʈ
                hit.GetComponent<Enemy>().DamageEffect();
            }

            if (canDuplicate) // ���� ����
            {
                if (Random.Range(0, 100) < duplicateChance) // ���� Ȯ��
                {
                    // Ŭ�� ����
                    SkillManager.instance.clone.CreateClone(hit.transform, new Vector3(0.5f * direction, 0));
                }
            }
        }
    }
}