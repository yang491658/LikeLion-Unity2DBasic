public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; } // ���� ����

    // ���� �ʱ�ȭ �Լ�
    public void Initialize(EnemyState _startState)
    {
        currentState = _startState; // ���� �ʱ�ȭ
        currentState.Enter(); // ���� ����
    }

    // ���� ���� �Լ�
    public void Change(EnemyState _newState)
    {
        currentState.Exit(); // ���� ����
        currentState = _newState; // ���ο� ����
        currentState.Enter(); // ���� ����
    }
}