using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine; // �� ���¸ӽ�
    protected Enemy enemyBase; // ��
    protected Rigidbody2D rb; // ������ٵ�

    private string animName; // �ִϸ��̼� �̸�
    protected float stateTimer; // ���� Ÿ�̸�
    protected bool trigger; // Ʈ���� �ߵ� ����

    // ������
    public EnemyState(EnemyStateMachine _stateMachine, Enemy _enemyBase, string _animName)
    {
        this.stateMachine = _stateMachine;
        this.enemyBase = _enemyBase;
        this.animName = _animName;
    }

    public virtual void Enter()
    {
        rb = enemyBase.rb; // ���� ������ٵ�

        enemyBase.anim.SetBool(animName, true); // �� �ִϸ��̼� ����

        trigger = false; // Ʈ���� �ʱ�ȭ
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime; // ���� Ÿ�̸� ����
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animName, false); // �� �ִϸ��̼� ����

        enemyBase.AssignAnim(animName); // �� �ִϸ��̼� �Ҵ�
    }

    // �ִϸ��̼� Ʈ���� �Լ�
    public virtual void AnimationTrigger()
    {
        trigger = true; // Ʈ���� �ߵ�
    }
}