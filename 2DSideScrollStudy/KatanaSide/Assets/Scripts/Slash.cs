using UnityEngine;

public class Slash : MonoBehaviour
{
    float angle; // 각도

    private GameObject player; // 플레이어

    public Vector3 direction = Vector3.right; // 방향(이동)
    Vector3 dirRotation; // 방향(회전)
    Vector2 MousePos; // 마우스 위치

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Transform tr = player.GetComponent<Transform>();
        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);
        Vector3 Pos = new Vector3(MousePos.x, MousePos.y, 0);

        dirRotation = Pos - tr.position; // 플레이어에서 마우스를 향하는 벡터

        // 각도 계산
        angle = Mathf.Atan2(dirRotation.y, dirRotation.x) * Mathf.Rad2Deg;
    }

    void Update()
    {
        transform.position = player.transform.position; // 슬래쉬 위치
        transform.rotation = Quaternion.Euler(0f, 0f, angle); // 슬래쉬 회전
    }

    public void Destr() // 이펙트 제거 함수
    {
        Destroy(gameObject);
    }
}