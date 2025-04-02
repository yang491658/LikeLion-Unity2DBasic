using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance; // �̱���
    private CinemachineImpulseSource impulseSource; // ���޽� �ҽ� ��ü

    void Awake()
    {
        instance = this;
        impulseSource = GetComponent<CinemachineImpulseSource>(); // ���޽� �ҽ� ������Ʈ
    }

    // ī�޶� ����
    public void Shake()
    {
        if (impulseSource != null)
        {
            // �⺻ �������� ���޽� ����
            impulseSource.GenerateImpulse();
        }
    }
}