using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5.0f; // 속도

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // 좌우 이동
        float moveZ = Input.GetAxis("Vertical"); // 앞뒤 이동
        Vector3 move = new Vector3(moveX, 0, moveZ);
        transform.Translate(move * speed * Time.deltaTime);
    }
}
