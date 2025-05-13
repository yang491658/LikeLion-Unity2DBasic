public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; } // ���� ����

    // ���� �ʱ�ȭ �Լ�
    public void Initialize(PlayerState _startState)
    {
        currentState = _startState; // ���� �ʱ�ȭ
        currentState.Enter(); // ���� ����
    }

    // ���� ���� �Լ�
    public void Change(PlayerState _newState)
    {
        currentState.Exit(); // ���� ����
        currentState = _newState; // ���ο� ����
        currentState.Enter(); // ���� ����
    }
}