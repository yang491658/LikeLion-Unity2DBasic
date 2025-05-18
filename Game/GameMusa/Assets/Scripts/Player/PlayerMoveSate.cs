using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    GameObject enemy; // ��

    // ������ - ���
    public PlayerMoveState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // �� ã��
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public override void Update()
    {
        base.Update();

        if (player.transform.position.x < enemy.transform.position.x) // �÷��̾ ���� ���ʿ� ����
        {
            xInput = 1; // �÷��̾� ���� = ������
        }
        else if (player.transform.position.x > enemy.transform.position.x) // �÷��̾ ���� �����ʿ� ����
        {
            xInput = -1; // �÷��̾� ���� = ����
        }

        // �÷��̾� �̵�
        player.SetVelocity(xInput * player.moveSpeed, player.rb.linearVelocityY);

        if (xInput == 0 || // �¿� ����Ű ���Է�
            player.IsWall()) // �÷��̾ �� ����
        {
            player.stateMachine.Change(player.idleState); // �÷��̾� ��� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}