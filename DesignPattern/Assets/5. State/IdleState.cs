using UnityEngine;

public class IdleState : IState
{
    public void Enter() { Debug.Log("IDLE ���� ����"); }
    public void Update() { Debug.Log("IDLE ���� ������"); }
    public void Exit() { Debug.Log("IDLE ���� ����"); }
}