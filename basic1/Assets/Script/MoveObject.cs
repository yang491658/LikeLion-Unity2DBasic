using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed = 5.0f; // 이동 속도

    void Update()
    {
        //// 키 입력에 따라 이동
        //float move = Input.GetAxis("Horizontal"); // 수평조작 감지 (AD 또는 방향키)
        //// 방향 * 스피드 * Time.deltaTime
        //transform.Translate(Vector3.right * move * speed * Time.deltaTime);

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //transform.position += move * speed * Time.deltaTime;
        transform.Translate(move * speed * Time.deltaTime);
    }
}
