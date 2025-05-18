public class PlayerDashState : PlayerState
{
    // ������ - ���
    public PlayerDashState(PlayerStateMachine _stateMachine, Player _player, string _animName)
        : base(_stateMachine, _player, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration; // ���� Ÿ�̸� �ʱ�ȭ = �뽬 ���ӽð�

        // Ŭ�� ����
        //player.skill.clone.CreateClone(player.transform);
        //player.skill.clone.CreateClone(player.transform, Vector3.zero);
        player.skill.clone.CreateCloneOnDash();
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGround() && player.IsWall()) // �÷��̾ �ٴ� �̰��� + �� ����
        {
            player.stateMachine.Change(player.wallSlideState); // �÷��̾� ��Ÿ�� ���·� ����
        }

        // �÷��̾� �뽬
        //player.SetVelocity(player.direction * player.dashSpeed, player.rb.linearVelocityY);
        player.SetVelocity(player.dashDirection * player.dashSpeed, 0);

        if (stateTimer < 0) // �뽬 ���ӽð� ����
        {
            player.stateMachine.Change(player.idleState); // �÷��̾� ��� ���·� ����
        }
    }

    public override void Exit()
    {
        base.Exit();

        // Ŭ�� ����
        player.skill.clone.CreateCloneOnDashOver();

        // �÷��̾� ����
        player.SetVelocity(0, rb.linearVelocityY);
    }
}