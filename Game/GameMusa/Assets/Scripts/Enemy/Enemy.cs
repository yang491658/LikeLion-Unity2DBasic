using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask playerLayer; // �÷��̾� ���̾�
    public string animName { get; private set; } // �ִϸ��̼�

    #region ����
    public EnemyStateMachine stateMachine { get; private set; } // �� ���¸ӽ�
    #endregion

    [Header("�̵� ����")]
    public float idleTime; // ��� �ð�
    public float moveSpeed; // �̵� �ӵ�
    private float moveSpeedSave; // �̵� �ӵ� ����

    [Header("���� ����")]
    public float battleTime; // ���� �ð�
    public float battleDistance; // ���� �Ÿ�
    public float attackCoolDown; // ���� ��ٿ�
    [HideInInspector] public float lastAttack; // ������ ����

    [Header("���� ����")]
    public float stunDuration; // ���� ���ӽð�
    public Vector2 stunDirection; // ���� ����

    [Header("�ݰ� ����")]
    [SerializeField] protected GameObject counterTime; // �ݰ� �ð� (�̹���)
    protected bool canCounter; // �ݰ� ���� ����

    protected override void Awake()
    {
        base.Awake();

        // ���¸ӽ� �ν��Ͻ� ����
        stateMachine = new EnemyStateMachine();

        moveSpeedSave = moveSpeed; // �̵� �ӵ� ����
    }

    // �ִϸ��̼� �Ҵ� �Լ�
    public virtual void AssignAnim(string _animName)
    {
        animName = _animName;
    }

    // �ð� ���� �Լ�
    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen) // �ð� ����
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else // �ð� ���� ����
        {
            moveSpeed = moveSpeedSave;
            anim.speed = 1;
        }
    }

    // �ð� ���� �ڷ�ƾ
    protected virtual IEnumerator FreezeTimerFor(float _seconds)
    {
        FreezeTime(true); // �ð� ����
        yield return new WaitForSeconds(_seconds);
        FreezeTime(false); // �ð� ���� ����
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    // �ݰ� Ÿ�̹� Ȱ��ȭ �Լ�
    public virtual void OpenCounterTime()
    {
        canCounter = true; // �ݰ� ����
        counterTime.SetActive(true); // �ݰ� �ð� Ȱ��ȭ
    }

    // �ݰ� Ÿ�̹� ��Ȱ��ȭ �Լ�
    public virtual void CloseCounterTime()
    {
        canCounter = false; // �ݰ� �Ұ���
        counterTime.SetActive(false); // �ݰ� �ð� ��Ȱ��ȭ
    }

    // �ݰ� ���� �Լ�
    public virtual bool CanCounter()
    {
        // �ݰ� ����
        if (canCounter)
        {
            CloseCounterTime(); // �ݰ� Ÿ�̹� ��Ȱ��ȭ
            return true;
        }

        // �ݰ� �Ұ���
        return false;
    }

    // ����� �׸��� �Լ�
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // �÷��̾�
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,
            new Vector3(transform.position.x + direction * battleDistance, transform.position.y));
    }

    // �÷��̾� ���� �Լ�
    public virtual RaycastHit2D IsPlayer()
        => Physics2D.Raycast(wallCheck.position, Vector2.right * direction, 50, playerLayer);

    // �ִϸ��̼� Ʈ���� �Լ�
    public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
}