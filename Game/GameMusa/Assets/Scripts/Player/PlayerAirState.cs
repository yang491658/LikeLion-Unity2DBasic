public class PlayerAirState : PlayerState
{
    // ������ - ���
    public PlayerAirState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        //if (rb.linearVelocityY == 0) // �÷��̾ ���� ����
        if (player.IsGround()) // �÷��̾ �ٴ� ����
        {
            stateMachine.Change(player.idleState); // �÷��̾� ��� ���·� ����
        }

        if (player.IsWall()) // �÷��̾ �� ����
        {
            stateMachine.Change(player.wallSlideState); // �÷��̾� ��Ÿ�� ���·� ����
        }

        if (xInput != 0) // �¿� ����Ű �Է�
        {
            // �÷��̾� õõ�� �̵�
            player.SetVelocity(xInput * player.moveSpeed * 0.8f, rb.linearVelocityY);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}