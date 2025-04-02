public class StateMachine 
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit(); // ���� ������ Exit ����
        currentState = newState; // ���ο� ���·� ����
        currentState.Enter(); // ���ο� ������ Enter ����
    }

    public void Update()
    {
        currentState?.Update(); // ���� ������ Update ����
    }
}