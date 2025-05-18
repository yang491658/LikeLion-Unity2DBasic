using UnityEngine;

public class EnemySetup : MonoBehaviour
{
    public Transform[] patrolWaypoints;

    private void Start()
    {
        // 적 컨트롤러 설정
        EnemyStateController controller = GetComponent<EnemyStateController>();

        // 웨이포인트 설정
        controller.waypoints = patrolWaypoints;

        // 플레이어 찾기
        controller.player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }
}
