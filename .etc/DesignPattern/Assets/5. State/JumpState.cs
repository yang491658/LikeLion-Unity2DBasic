using UnityEngine;

public class JumpState : IState
{
    public void Enter() { Debug.Log("JUMP ���� ����"); }
    public void Update() { Debug.Log("JUMP ���� ������"); }
    public void Exit() { Debug.Log("JUMP ���� ����"); }
}
