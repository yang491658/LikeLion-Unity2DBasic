using UnityEngine;

public class EnemySetup : MonoBehaviour
{
    public Transform[] patrolWaypoints;

    private void Start()
    {
        // �� ��Ʈ�ѷ� ����
        EnemyStateController controller = GetComponent<EnemyStateController>();

        // ��������Ʈ ����
        controller.waypoints = patrolWaypoints;

        // �÷��̾� ã��
        controller.player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
}
