using UnityEngine;

public class PlayerCatchState : PlayerState
{
    private Transform sword; // �ҵ�

    public PlayerCatchState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // �ҵ� ��ġ�� �÷��̾� ���� ȸ��
        sword = player.sword.transform;
        if (player.transform.position.x > sword.position.x && player.direction == 1)
            player.Flip();
        else if (player.transform.position.x < sword.position.x && player.direction == -1)
            player.Flip();

        // �÷��̾� �ҵ� ��� �ݵ�
        rb.linearVelocity = new Vector2(-player.direction * player.swordReturnImpact, rb.linearVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (trigger) // Ʈ���� �ߵ� ��
        {
            stateMachine.Change(player.idleState); // �÷��̾� ��� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();

        // �÷��̾� �ൿ �ڷ�ƾ
        player.StartCoroutine("Act", 0.1f);
    }
}