using UnityEngine;

public class RunState : IState
{
    public void Enter() { Debug.Log("RUN ���� ����"); }
    public void Update() { Debug.Log("RUN ���� ������"); }
    public void Exit() { Debug.Log("RUN ���� ����"); }
}